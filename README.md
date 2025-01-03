# BasketWeaver [![License: LGPL v3](https://img.shields.io/badge/License-LGPL_v3-blue.svg)](https://www.gnu.org/licenses/lgpl-3.0) 

### [CHANGELOG](CHANGELOG.md)

BasketWeaver is a Weaver/Injector for HBS's [BattleTech](https://harebrained-schemes.com/battletech/). It provides users the capability to inject C# code into BattleTech without requiring manual specification of [CIL](https://learn.microsoft.com/en-us/dotnet/standard/managed-code). 

When [ModTek](https://github.com/BattletechModders/ModTek) is installed and BattleTech starts, it uses UnityDoorstop to load early and avoid loading .NET assemblies. During this phase, ModTek can inject code into .NET assemblies through Injectors, inclusive itself. BasketWeaver enables the user to write C# code instead of CIL through helper libraries that wrap user specified fields and methods to override existing classes. These injected methods can then be called by BattleTech and optionally Harmony patches later in the loading process.

## Quick Start

1. In `CHANGEME.Directory.Build.Props`, set `BattleTechGameDir` to the location of the `BATTLETECH` install folder: ```<BattleTechGameDir>CHANGEME_TO_FULL_PATH_TO_BTG_DIR</BattleTechGameDir>```
2. Open IDE of choice and compile. `Build/*` will contain a mirror of of what is copied to your `BATTLETECH/Mods/` folder if built succeeds.

## Components

By default, BasketWeaver uses three components:

1. **BasketWeaver**: `/Mods/BasketWeaver`
2. **BasketWeaverInjector**: `/Mods/ModTek/Injectors` (mirrored in `/Mods/BasketWeaver/Injectors`)
3. **Helper Libraries**: `/Mods/BasketWeaver/Helpers`

## BasketWeaver

BasketWeaver will be installed by default into `BattleTech/Mods/BasketWeaver`. This parent mod has minimal functionality and is used to provide a container for Helper Libraries that BasketWeaverInjector will load during the ModTek's injection phase.

## BasketWeaverInjector

BasketWeaver will be installed by default into `BattleTech/Mods/ModTek/Injectors/BasketWeaverInjector.dll`. It provides the following default capabilities:
* Injects helpers from `/Mods/BasketWeaver/Helpers/` into the game
* Basic heuristics for automated inlining of specified `Modules`/`.DLLs`
* Conflict detection to avoid injecting or inlining methods that may bypass Harmony patches. This could occur due to inlining relocating instructions to caller methods and bypassing the patched methods.

A configuration file in `BattleTech/Mods/ModTek/Injectors/BasketWeaver/.json` is provided to toggle `Helper Injection` and `Automated Inlining` routines. It can also specific a desired `Helper Namespace`, set to `BasketWeaver` to start.

Automated inlining attempts to speed up Unity Engine and game performance by detecting methods that can be inlined. It naively uses method instructions and method attributes to identify wrapper-like functions that can be safely inlined at minimal cost. Due to the nature of possible Harmony bypasses, this should be specified carefully and may break runtime behavior.

## Helper Libraries

Helper libraries are .NET Class Libraries that provide methods to replace existing code at injection time. This library can reference BattleTech's injectable `.DLLs` and insert user specified code to replace original methods. The reference IL can be extracted through tools like [ILSpy](https://github.com/icsharpcode/ILSpy) or [dnSpy](https://github.com/dnSpy/dnSpy), modified, and compiled against BattleTech's `.DLLs` and automatically injected by `BasketWeaverInjector`

### PerfLibHelpers

This helper library is provided by default and loaded by BasketWeaverInjector. It attempts to improve performance of the `UnityEngine.CoreModule`. The following optimizations are considered:
1. Remove `return new Constructor();` calls in core mathematical results.
2. Reuse of Left-Hand-Side (LHS) valuetypes instead of allocation where applicable when result assignments do not cross axis boundaries or require temporary variables. 
3. Improvement of `ToString` through `StringBuilder.Append` for formatting
4. Precalculating divisions in vectorized divides to avoid floating point overhead. May or may not be optimized by the JIT.
5. Removing wrapped `UnityEngine.Mathf` calls and passing equivalently casted `System.Math` calls directly to reduce call overhead. 
6. Attaching the `[AggressiveInlining]` attribute to math and helper functions similar to `Unity.Mathematics` to further reduce call overhead.

Many of these optimizations were found optimizing logging overhead for the purposes of speeding up BattleTech. Some of them can be found in future versions of Unity.Mathematics and further discussions on these techniques can be founds in the below references:

### References:
* [Unity.Mathematics](https://github.com/Unity-Technologies/Unity.Mathematics) - Uses many of the above techniques but not included as default math by the Unity 2018.4.2f1 build as used by BattleTech.
* [Frame Rate Booster](https://github.com/tool-buddy/FrameRateBooster) - A plugin that attempts to apply some of these optimizations to existing Unity builds. 
* [FRB Discussion](https://discussions.unity.com/t/vector3-and-other-structs-optimization-of-operators/668199) - Discussion of the frame rate implications and JIT/IL2CPP behavior of above optimizations

## Getting Started

Following along the `PerfLibHelper`, these steps provide a reference on replacing a method:

### 1. Find method to replace:

In this example, we replace the `+` operator to optimize addition. First `return new Color();` is removed, and `Color a` reused as a variable to pass the result. As structures are passed by value, this results in no side effects in the calling method and simultaneously avoids construction overhead.

From ILSpy inspection of `UnityEngine.CoreModule.dll`:
```csharp
namespace UnityEngine
{
public struct Color

    public static Color operator +(Color a, Color b)
    {
        return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
    }
}
```


### 2. Create a helper library:

See the PerfLibHelpers directory for examples on project setup. Otherwise, in IDE of choice, create a new Class Library targeting `.NET Framework 4.7.2` and reference the `UnityEngine.CoreModule.dll`. Referencing `.DLLs` may be required for `Mono.Cecil` to resolve references during injection. Configure PostBuild actions as necessary.

### 3. Wrap and modify the method

The following snippet replaces the `+` operator with an optimized method. Member variables called by replaced methods shall include all preceding variables in the order they were defined. This may ease resolving references where field indexes are provided in CIL operands. The `BasketWeaver` namespace will be removed during injector and method operands resolved against the BattleTech loaded `.DLL`. The wrapper namespace can be configured using the `HelperNamespace` option in the`BasketWeaverInjector.json` settings file.

**NOTE:** `.NET` can ambiguously resolve the namespaces against the helper namespace. In these instances, manually inserting the references may be required. Types are generally resolved by full-name in the injector. 

`Color.cs` snippet from PerfLibHelpers:
```csharp
using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

// Helper Namespace, referenced and removed by BasketWeaver during Injetion
namespace BasketWeaver
{
    namespace UnityEngine
    {
        public struct Color : IEquatable<Color>
        {
            public float r;
            public float g;
            public float b;
            public float a;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Color operator +(Color a, Color b)
            {
                a.r = a.r + b.r;
                a.g = a.g + b.g;
                a.b = a.b + b.b;
                a.a = a.a + b.a;
                return a;
            }
        }
    }
}
```
### 4. Install your helper into BasketWeaver

After build, copy the output `.DLL` helper library into the `BasketWeaver/Helpers/*` directory. Upon runtime, all `.DLLs` in this folder will be searched for the specified wrapper namespace and injected. 

**NOTE:** The copy may be done automatically depending on post-build options. See PerfLibHelpers for examples. PerfLibHelpers provides post-build options to copy the helpers to both the chosen `BattleTechGameDir` and a local `Build` directory that can be used for distribution.

