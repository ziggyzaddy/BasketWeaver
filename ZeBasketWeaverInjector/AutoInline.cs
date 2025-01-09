using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace BasketWeaver
{
    public class AutoInline
    {
        public static void Run(
            Settings config,
            ConflictDetect conflict,
            AssemblyDefinition assembly,
            int maxInstrCount = 16)
        {
            Console.WriteLine($"### Adding Inlines {assembly.Name}:");
            foreach (var type in assembly.MainModule.GetAllTypes())
            {
                if (type == null) { continue; }

                Console.WriteLine($"### {type.FullName}");

                if (!type.HasMethods) { continue; }

                //Console.WriteLine($"```csharp");
                foreach (var method in type.Methods)
                {
                    if (method == null) { continue; }
                    // Check body exists, if not unable to inline
                    if (!method.HasBody) { continue; }

                    // Inlining restrictions, partly from dotnet/runtime
                    if (method.IsInternalCall) { continue; }
                    if (method.NoInlining) { continue; }
                    if (method.IsSynchronized) { continue; }
                    if (method.IsNative) { continue; }
                    if (method.IsPInvokeImpl) { continue; }
                    if (method.IsUnmanaged) { continue; }
                    if (method.IsAbstract) { continue; }
                    if (method.IsVirtual) { continue; }
                    if (method.IsUnmanaged) { continue; }
                    if (method.IsUnmanagedExport) { continue; }
                    if (method.IsCompilerControlled) { continue; }
                    if (method.IsForwardRef) { continue; }

                    if (method.Body.Instructions.Count >= maxInstrCount) { continue; }

                    // Check if harmony is patching this method, avoids inlining it into Callers and using vanilla implementation
                    if (conflict.MethodDefConflictCheck(method))
                    {

                        // Safe Mode
                        if (config.AutoInlineSafeMode)
                        {
                            method.AggressiveInlining = false;
                            method.NoInlining = true;
                        }

                        // Method conflict check == true if found
                        Console.WriteLine($"     [SKIP - HARMONY] {method.DeclaringType.FullName}::{method.Name}");
                        continue;
                    }

                    // Iterate through calls and check if anything patched is referenced. Avoid inlining so callee is correct
                    if (!conflict.FindPatchedCalls(method))
                    {
                        Console.WriteLine($"  {method.DeclaringType.FullName}::{method.Name}");
                        // Patched calls not found, inline as all conditions have passed
                        method.AggressiveInlining = true;
                        continue;
                    }
                }
            }
        }

    }
}
