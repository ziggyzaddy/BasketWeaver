## BattleTech 2018 Modded Optimization Notes - 2025 JAN
     
This document highlights AI optimization efforts, starting with initial profiling of CleverGirl. Certain fixes are generally applicable, however modpacks using ModTek and the following mods will be more impacted:
- [StrategicOperations](https://github.com/BattletechModders/StrategicOperations)
- [TisButAScratch](https://github.com/BattletechModders/TisButAScratch)
- [LowVisibility](https://github.com/BattletechModders/LowVisibility)
- [IRBTModUtils](https://github.com/BattletechModders/IRBTModUtils)
- [MechEngineer](https://github.com/BattletechModders/MechEngineer)
- [CustomAmmoCategories](https://github.com/BattletechModders/CustomAmmoCategories)
- [CustomComponents](https://github.com/BattletechModders/CustomComponents)

### Assumptions
- Mathematical complexity of influence map calculations contributed to AI think time
- Time was lost in Line of Sight (LOS) and Line of Fire (LOF) checking
- Bresenham was a major contributor to think time.

### Initial Steps
- CleverGirl instrumented manually and > `100 ms` slowdowns printed in logs.
- Found `*_PositionalFactor` evaluations took > `1 sec` when units had large movement ranges, more ammo modes, or multi-shot weaponry.
- Manual profiling of log functions showed > ~`1 sec` per turn accumulated
- Past profiling data and flame-graphs were reviewed
- .NET Standard 2.0 was considered for GPU AI optimizations and python scripting for development.
- GPU attempts halted when data acquisition began spiking manual instrumentation. 
- Profiler access enabled DotTrace visualization/flamegraphs.

## Common Notes
- .NET Framework 4.7.x string processing and formatting frequently allocates new strings, take up time & memory, and increases Garbage Collector runtimes through underlying `mallocs` and `callocs`.
- Allocating memory done frequently with strings where integers, bools, or enums can be used.
- General memoization needed where performance concerns arise, often due to string comparisons over lists/loops.
- Encoding is expensive, and Mono has UTF8 encoding overhead and string allocations for Reflection calls and naively implemented Managed <-> Native marshalling.
- Due to extensive logging - formatting, buffer writing, encoding, and flush overhead can block main. Performance worse on HDD and older SATA/M.2 SSD.
- Mono `InternalCall` overhead can range from 5 to 16ns, and more when marshalling data.
- Call stack overhead is non-negligible, inclusive `InternalCall` overhead. This may remove performance benefits when JIT is faster than native implementation.

## Optimization - Tooling:


### .NET Standard 2.0 Facades
Notes:
- Initial attempts at speeding and improving AI had rationale for using either a compute shader language or scripting language to ease development.
- Attempts were made to use .NET Standard 2.0 nuget packages in development.
- Discussion of facade use kicked off collaborative efforts to improve runtime performance and tooling to handle performance improvements across ModTek and other areas of the game.

Process:
- Adding `Auto-generate binding redirects` to `.csproj` properties enables .NET Standard 2.0 dependencies to be resolved against .NET Framework 4.7.2.
- The `netstandard.dll` provided in the built version of Mono bundled with Unity Editor shall be referenced. If a different version of Mono BGE (Bleeding Edge) is used, use the `netstandard.dll` compiled alongside the Mono version.
- Ensure all facade `.dlls` are referenced as needed when using libraries dependent on core features. These are also provided alongside editor releases, and can be compiled alongside new Unity Mono versions.

### ModTek .NET Standard 2.0 Injector Support
Notes:
- To solve a circular dependency, injectors were converted to support MSBuild of injector `.dlls` and run them at build time.
- This supports referencing injected variables without running the game and requiring ModTek's preloader.

### BasketWeaver
Notes:
- As injectors require manually writing C# Intermediate Language (IL) for injecting fields, types, or modification/replacement of methods, tooling for weaving instructions through helper libraries was created
- In addition to weaving capabilities, automated inlining using backports of salient .NET9 heuristics was added to improve performance for smaller helper functions and avoid call stack overhead.

### Mono
Notes:
- To experiment under the hood, a Mono BGE (Bleeding Edge) version compatible with Unity 2018.4f2 was recompiled and replaced. `2018.4.15` built with debugging capabilities for `dnSpy` was used.

## Optimization - Startup:
### Asynchronous Logging - `ModTek.Features.Logging.MTLoggerAsyncQueue`
Identification:
- ModTek asynchronous processing was improved with better optimized code
- Faster dispatching and moving formatting off-main reduced was required
- Increased logging warranted ModTek to also be asynchronous for performance concerns and future redirect of logging from IRBTModUtils to ModTek.

Fix:
- [Commit](https://github.com/BattletechModders/ModTek/commit/d7ebcb92212927c341923367f6eaade0e7535939): Start of dispatch optimization and beginning of asynchronous optimization efforts following profiling of IRBTModUtils logger. ModTek carrying HBS Logging message load provided opportunity to further offload logging from main, and eventually redirect all logging through ModTek.
- [Commit](https://github.com/BattletechModders/ModTek/commit/6559df4d7dfe247621caed6a090323e394394eca): Improve thread safety of asynchronous logging.
- [Commit](https://github.com/BattletechModders/ModTek/commit/4d43fcb97d76054b5bbf0dc30485af37e9e77740): Remove more timestamp processing from main thread.
- [Commit](https://github.com/BattletechModders/ModTek/commit/a1ee1cc6904fe7111716e4b10f5146a16d6208db): Backport of random sampling ideas from newer .NET versions
- [Commit](https://github.com/BattletechModders/ModTek/commit/b86c619ddde369e6240797ccdd3bf2a99cb88958): Batch writes to OS Buffers for improved performance.

## Optimization - In Mission:

### Asynchronous Logging - `IRBTModUtils.DeferredLogger`
Identification:
- Manual instrumentation through CleverGirl (profiler initially unavailable)
- Enabling LowVis trace increased logging times to > ~`2 sec` per turn.
- `String.Concat`, `DateTime.ToString`, `StreamWriter.WriteLine()`, `StreamWriter.Flush()` found to contribute to main thread execution times
- Kicked off performance optimizations using .NET ConcurrentQueue, which was quickly replaced with circular buffer and started remaining optimization efforts ~ 2024 NOV to DEC.

Fix:
- [PR](https://github.com/BattletechModders/IRBTModUtils/pull/16)
- Offload string processing and flush overhead from main to an asynchronous logging thread, and reduce allocations/CPU time
- `String.Concat` replaced by allocating async thread local buffer and directly formatting DateTime through a zero-alloc function inclusive concatenation spaces. Reduced runtime allocations to zero.
- Reduced redundant formatting, queued writes, and flushed messages contiguously by specified file
- Marshal message data through a fast MPMC (Multiple Producer Multiple Consumer) concurrent circular buffer, prototypical of the modern .NET9 ConcurrentQueue implementation.


### Removing StartsWith -  `StartsWith(DebilitatedPrefix)`

Identification:
- `Pilot.IsIncapacitated.Getter.Postfix` is frequently called by CleverGirl through Strategic Operations.
- `GetAllFriendlies` frequently calls `GetIsDead`.

Fix:
- [PR](https://github.com/BattletechModders/TisButAScratch/pull/5)
- `StatCollection` memoized the tags and removed iteration of ParentActor tags.
- `StartsWith` is an expensive call due to string processing and allocations, and was called ~12.5M times on a turn when profiled.

### Reducing TagSet Use - `HBS.TagSet`
Identification:
- `HBS.Collections.TagSet.AddRange()`.
- In AI under `CustomAmmoCategoriesPatches.ToHit_GetAllModifiers()`.

Fix:
- [PR](https://github.com/BattletechModders/CustomAmmoCategories/pull/33)
- Convert instances using TagSet.AddRange() to StatCollections
- Target hot-spot uses first through StatCollections, eventually add injected fields.
- Trades string allocations for dictionary lookup overhead, which is significantly faster

### Evasion Reflection - `CustomAmmoCategoriesPatches.ToHit_GetEvasivePipsModifier`
Identification:
- `System.RuntimeType.GetField()` took > `500msec`in `CustAmmoCategories.ToHitModifiersHelper`
- Used when getting the `CombatGameState`

Fix:
- Use `Krafs.Publicizer` to directly access the field and eliminate reflection overhead due to Mono.
- Similar optimizations for to `CustomTranslation.Interpolator_Interpolate` and other instances using Reflection/Traverse


### Placeholder Interpolation - `MechEngineer.InterpolateText`

Identification:
- `MechEngineers.AccuracyEffectsFeature.AccuracyForLocation` frequently called `System.Enum.ToString()` for returning `LocationId`.
- Causes reflection overhead through `System.RuntimeType.IsDefined` and alloc overhead through `ToString` 


Fix:
- [Commit](https://github.com/BattletechModders/MechEngineer/commit/7521fe2eae83d70dc0cbb0f83d542562ac4da9c7): Locations are currently fixed, return string directly through switch - case statement.
- [Commit](https://github.com/BattletechModders/MechEngineer/commit/b8a5a8508d4dce1de82c4ca13d8e75c287af04c8): Further improvements to `AccuracyFeature` through adding `StatCollection` in `AccuracyForKey` and `AccuracyForLocation`


### MonsterMashup - `HasCollisionAt`
Identification: 
- Found in  `MonsterMashup.Patch.PathNode_HasCollisionAt`
- Overlapping actors checked using Unique ID string equality 

Fix:
- [PR](https://github.com/BattletechModders/MonsterMashup/pull/3): Map Unique ID to integers and compare through dictionary lookups to avoid string comparison overhead

### Off Screen Rendering - `SphericalHarmonics.Evaluate()`
Identification:
- Found when rendering OffScreen cameras
- Mono `InternalCall` return of `[Out] Color` caused marshalling overhead due to type reflection

Fix:
- Reimplement Normal -> Color calculation for Spherical Harmonics based on reference shader `.cginc` on CPU
- Uses `Stupid Spherical Harmonics` - GDC 2008 as implemented in Unity.
- Differences in Native call to Mono JIT negligible, as marshalling takes up bulk of profile time.

### Unity Core Module - Mathematics
Identification:
- `Vector3` operations frequently used the following pattern after calculations, adding ~`10ns` to function overhead
```csharp
return new Vector3(x,y,z);
```
- Math operations that operate without crossing ordinates can reuse the left-hand-side (LHS) as storage since struct is passed by value.
- `Vector3.ToString()` returns a new object formatter.
- Unity wraps `System.Mathf` operations with helpers to hide casting from caller. Adds function call overhead as inlining not adequately applied by runtime

Fix:
- Reuse LHS parameter as storage when returning results to avoid new allocation
- Replace `ToString` with `StringBuilder`. This is faster than original implementation, but may require further optimization since UTF16 -> UTF8 encoding will not expand in practice
- Declare temporary storage when ordinates require temporary calculations (i.e. Cross Product). Do not call constructor
- Directly call `System.Mathf` operations and cast.
- Overall impact of `~30%` to `~80%` for all `VectorX`, `VectorXInt`, `Color` and relevant `Matrix4x4` methods where replaced in weaver.

### BasketWeaver - Automated Inlining
Identification:
- Older .NET version and bundled Unity runtime does not insert AggressiveInlining. Codebase contains many helper functions that can be heuristically inlined as per newer `dotnet/runtime` versions.

Fix:
- Implement automated inlining through backported heuristics at injection time. 
- To avoid unpatching HarmonyX, conflict detection must be ran prior to inserting inlines

## Optimization - SimGame:


## References
- [ModTek Development Guide](https://github.com/BattletechModders/ModTek/blob/master/doc/DEVELOPMENT_GUIDE.md): Profiling information used to connect [dotTrace](https://www.jetbrains.com/profiler/). Main profiler used during optimization efforts after initial manual attempts.
- [Current Mono Version](https://github.com/Unity-Technologies/mono/tree/unity-2018.4-mbe): Provides insight into runtime specifics. recompiled and ran for exploration and understanding
- [.NET9 Performance Improvements](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-9/): Previous performance improvements included in links, and some techniques were backported or adapted as needed
- [Unity Editor Archive](https://unity.com/releases/editor/archive): Used for `.dll` experimentation against .NET Standard 2.0. Bundled shader `.cginc` files were used for L2 Spherical Harmonics optimization
- [Stupid Spherical Harmonics](https://www.gdcvault.com/play/273/Stupid-Spherical-Harmonics-(SH)): Implementation/inspiration for spherical harmonics math as referencing in light probe documentation for Unity. Used to reimplement mathematics without function call overhead
- [Frame Rate Booster](https://github.com/tool-buddy/FrameRateBooster): Prior discovery of constructor and LHS reuse optimizations for mathematics found in current profiling efforts.
