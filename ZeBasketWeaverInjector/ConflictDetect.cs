using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Mono.Cecil.Rocks;


namespace BasketWeaver
{
    public struct ConflictData
    {
        public TypeDefinition Patcher;
        public MethodDefinition MethodDef;
        public MethodReference MethodRef;
        public string TypeStr = "";
        public string MethodStr = "";

        public ConflictData(string typeStr, string methodStr)
        {
            TypeStr = typeStr;
            MethodStr = methodStr;

        }
    }

    public class ConflictDetect
    {
        // Uses ExtractSig for type
        Dictionary<string, ConflictData> _methodConflicts = new Dictionary<string, ConflictData>();
        Dictionary<string, ConflictData> _typeConflicts = new Dictionary<string, ConflictData>();
        DefaultAssemblyResolver _modResolver = new DefaultAssemblyResolver();

        List<string> AvoidDetect = new List<string>
        {
            "Assembly-CSharp.dll",
            "System.",
            "bass",
            "winhttp",
            "UnityEngine",
            "mono",
            "sqlite",
            "SimdJsonNative"
        };


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TypeDefConflictCheck(TypeDefinition typeDef)
        {
            if (_typeConflicts.TryGetValue(typeDef.FullName, out var conflict))
            {
                Console.WriteLine($"     [SKIP - TYPE] {typeDef.FullName}");
                return true;
            }
            return false;
        }


        // Returns true if MethodDefinition conflicts
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MethodDefConflictCheck(MethodDefinition methodDef)
        {
            string conflictCheck = methodDef.DeclaringType.FullName + "::" + methodDef.Name;

            if (_methodConflicts.TryGetValue(conflictCheck, out var conflict))
            {
                Console.WriteLine($"     [SKIP - DEF] {methodDef.DeclaringType.FullName}::{methodDef.Name}");
                return true;
            }

            return false;
        }

        // Returns true if MethodReference conflicts
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MethodRefConflictCheck(MethodReference methodRef)
        {
            string conflictCheck = methodRef.DeclaringType.FullName + "::" + methodRef.Name;

            if (_methodConflicts.TryGetValue(conflictCheck, out var conflict))
            {
                Console.WriteLine($"     [SKIP - REF] {methodRef.DeclaringType.FullName}::{methodRef.Name}");
                return true;
            }
            return false;
        }

        // Returns true if a method calls a patched function. If accidentally inlined, a caller to this method can override the patch
        public bool FindPatchedCalls(MethodDefinition method)
        {
            foreach (Instruction instr in method.Body.Instructions)
            {
                if (instr == null) { continue; }
                if (instr.Operand == null) { continue; }
                switch (instr.Operand.GetType().Name)
                {
                    case "MethodDefinition":
                        {
                            MethodDefinition methodDef = (MethodDefinition)instr.Operand;

                            if (MethodDefConflictCheck(methodDef))
                            {
                                return true;
                            }

                            break;
                        }
                    case "MethodReference":
                        {
                            MethodReference methodRef = (MethodReference)instr.Operand;

                            if (MethodRefConflictCheck(methodRef))
                            {
                                return true;
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            return false;
        }

        public static void PrintCustomAttribute(CustomAttribute customAttribute)
        {
            foreach (var param in customAttribute.Constructor.GenericParameters)
            {
                Console.WriteLine("Has Params: ");
                Console.WriteLine(param.FullName);
            }

            if (customAttribute.HasConstructorArguments)
            {
                Console.WriteLine("Has Constructor: ");
                foreach (var arg in customAttribute?.ConstructorArguments)
                {

                    if (arg.Value != null)
                    {
                        Console.Write($"{arg.Type.FullName} {arg.Value.ToString()} ");

                    }
                }
            }

            if (customAttribute.HasProperties)
            {
                Console.WriteLine("Has Props: ");
                foreach (var prop in customAttribute.Properties)
                {
                    Console.Write($"{prop.Argument.Type} {prop.Argument.Value} ");
                }
            }

            if (customAttribute.HasFields)
            {
                Console.WriteLine("Has Fields: ");
                foreach (var field in customAttribute.Fields)
                {
                    Console.Write($"{field.Argument.ToString()} {field.Name?.ToString()} ");
                }
            }
            Console.WriteLine(customAttribute.AttributeType.FullName);
        }


        public string GetMethodTypeString(Harmony.MethodType methodType, string methodStr = "")
        {

            switch (methodType)
            {
                case Harmony.MethodType.Normal:
                    {
                        return methodStr;
                    }

                case Harmony.MethodType.Getter:
                    {
                        return "get_" + methodStr;
                    }
                case Harmony.MethodType.Setter:
                    {
                        return "set_" + methodStr;
                    }

                case Harmony.MethodType.Constructor:
                    {
                        return ".ctor_" + methodStr;
                    }
                case Harmony.MethodType.StaticConstructor:
                    {
                        return ".ctor" + methodStr;
                    }

                default:
                    {
                        return methodStr;
                    }
            }

        }

        public string GetHarmonyLibMethodTypeString(HarmonyLib.MethodType methodType, string methodStr = "")
        {
            switch (methodType)
            {
                case HarmonyLib.MethodType.Normal:
                    {
                        return methodStr;
                    }

                case HarmonyLib.MethodType.Getter:
                    {
                        return "get_" + methodStr;
                    }
                case HarmonyLib.MethodType.Setter:
                    {
                        return "set_" + methodStr;
                    }

                case HarmonyLib.MethodType.Constructor:
                    {
                        return ".ctor_" + methodStr;
                    }
                case HarmonyLib.MethodType.StaticConstructor:
                    {
                        return ".ctor" + methodStr;
                    }

                default:
                    {
                        return methodStr;
                    }
            }

        }



        public void CheckDefinition(InjectableDefinitions definitions)
        {

            foreach (var injectableDef in definitions.Definitions)
            {
                Console.WriteLine($"Diffing {injectableDef.Value.FullName}");

                Dictionary<string, MethodDefinition> injectableMethods = new Dictionary<string, MethodDefinition>();
                Dictionary<string, MethodDefinition> referenceMethods = new Dictionary<string, MethodDefinition>();

                Mono.Cecil.AssemblyDefinition referenceDef = null;
                try
                {
                    referenceDef = _modResolver.Resolve(new Mono.Cecil.AssemblyNameReference(injectableDef.Key.Replace(".dll", ""), null));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                if (referenceDef == null)
                {
                    continue;
                }
                if (injectableDef.Value == null)
                {
                    continue;
                }

                Console.WriteLine($"Found {referenceDef.FullName}");

                int injectableMethodCount = 0;
                int referenceMethodCount = 0;

                foreach (var type in injectableDef.Value.MainModule.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        injectableMethods[method.FullName] = method;
                        injectableMethodCount = injectableMethodCount + 1;
                    }


                }
                foreach (var type in referenceDef.MainModule.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        referenceMethods[method.FullName] = method;
                        referenceMethodCount = referenceMethodCount + 1;
                    }
                }

                Console.WriteLine($"Counts: Injectable: {injectableMethodCount} | Reference: {referenceMethodCount}");

                foreach (var method in injectableMethods)
                {
                    if (referenceMethods.TryGetValue(method.Value.FullName, out MethodDefinition refMethod))
                    {

                        //refMethod.FullName = method.Key;    
                        if (method.Value.HasBody != refMethod.HasBody)
                        {
                            Console.WriteLine($"Detected Diff Method [BODY]: {method.Value.FullName}");
                            continue;
                        }

                        if (method.Value.HasBody == true)
                        {

                            if ((method.Value.Body.Instructions != null) && (refMethod.Body.Instructions != null))
                            {

                                if (method.Value.Body.Instructions.Count() != refMethod.Body.Instructions.Count())
                                {

                                    Console.WriteLine($"Detected Diff Method [INSTR_CNT]: {method.Value.FullName}");

                                    //Console.WriteLine($"### Original Method:");
                                    //Formatter.PrintMethod(refMethod);
                                    //Console.WriteLine($"### New Method:");
                                    //Formatter.PrintMethod(method.Value);
                                }
                            }
                        }

                        //if(method.Value.Body.Instructions.Count != refMethod.Body.Instructions.Count)
                        //{                        
                        //    Console.WriteLine($"Detected Diff Method [INSTR_CNT]: {method.Value.FullName}");
                        //    continue;
                        //}

                    }
                    else
                    {
                        Console.WriteLine($"Detected Injected Method: {method.Value.FullName}");

                    }
                }


                // Required to not hold the reference to Assembly and cause later loader errors
                referenceDef.Dispose();
            }

        }

        bool MethodEquality(MethodDefinition methodA, MethodDefinition methodB)
        {
            if (methodA.FullName != methodB.FullName) { return false; }

            if (methodA.HasBody != methodB.HasBody) { return false; }

            if (methodB.CallingConvention != methodA.CallingConvention) { return false; }
            if (methodA.DeclaringType.FullName != methodB.DeclaringType.FullName) { return false; }


            return true;

        }


        public ConflictDetect()
        {
            string curDir = Directory.GetCurrentDirectory();
            string modDir = Path.Combine(curDir + "/Mods");
            if (!Directory.Exists(modDir))
            {
                Console.WriteLine($"Mod Dir not found");
                return;
            }
            Console.WriteLine($"Found Mod Dir: {modDir}");
            string[] modDlls = Directory.GetFiles(modDir, "*.dll", SearchOption.AllDirectories);
            _modResolver.AddSearchDirectory(Path.Combine(curDir, "/BattleTech_Data/Managed"));

            Console.WriteLine($"Running Conflict Detection for Harmony");

            foreach (var modDll in modDlls)
            {
                if (modDll.Contains("AssembliesInjected")) { continue; }
                if (modDll.Contains("AssembliesShimmed")) { continue; }
                if (modDll.Contains("ModTek") && !modDll.Contains("Injectors")) { continue; }
                Console.WriteLine($"Adding Path {Path.GetDirectoryName(modDll)}");
                _modResolver.AddSearchDirectory(Path.GetDirectoryName(modDll));
            }
            foreach (var modDll in modDlls)
            {
                // Console.WriteLine($"Parsing {Path.GetFileNameWithoutExtension(modDll)}");
                // These are System & Engine dlls and should not contain Harmony Patches. They may also trip the resolver.
                bool avoidDLL = false;
                foreach (var avoid in AvoidDetect)
                {
                    if (Path.GetFileNameWithoutExtension(modDll).StartsWith(avoid))
                    {
                        avoidDLL = true;
                        break;
                    }
                }
                if (avoidDLL) { continue; }

                if (modDll.Contains("Managed"))
                {
                    continue;
                }
                if (modDll.Contains("Simd"))
                {
                    continue;
                }

                var assembly = _modResolver.Resolve(new AssemblyNameReference(Path.GetFileNameWithoutExtension(modDll), null));

                // Do not add the injector to conflict detect.
                if (assembly.MainModule == null)
                {
                    continue;
                }
                foreach (var type in assembly.MainModule.Types)
                {
                    if (type == null) { continue; }
                    if (!type.HasCustomAttributes) { continue; }

                    var typeStr = "";
                    var methodStr = "";
                    bool isHarmony = false;

                    foreach (var custAttr in type.CustomAttributes)
                    {
                        if (!custAttr.AttributeType.FullName.Contains("Harmony"))
                        {
                            continue;
                        }
                        else
                        {
                            isHarmony = true;
                        }

                        if (custAttr.HasConstructorArguments)
                        {
                            foreach (var arg in custAttr.ConstructorArguments)
                            {


                                switch (arg.Type.FullName)
                                {
                                    case "System.Type":
                                        {
                                            typeStr = arg.Value.ToString();
                                            break;
                                        }
                                    case "System.String":
                                        {
                                            if (methodStr != "")
                                            {
                                                break;
                                            }

                                            if (typeStr != "")
                                            {
                                                methodStr = arg.Value.ToString();
                                            }
                                            break;
                                        }

                                    case "System.String[]":

                                        {
                                            if (typeStr == "") { continue; }
                                            if (methodStr != "") { continue; }

                                            if (arg.Type.IsArray)
                                            {

                                                CustomAttributeArgument[] args = arg.Value as CustomAttributeArgument[];

                                                if (args.Count() > 0)
                                                {
                                                    methodStr = args[0].ToString();
                                                }

                                            }
                                            //System.Diagnostics.Process.GetCurrentProcess().Kill();
                                            break;
                                        }

                                    case "Harmony.MethodType":
                                        {
                                            Harmony.MethodType methodType = (Harmony.MethodType)arg.Value;
                                            methodStr = GetMethodTypeString(methodType, methodStr);
                                            break;
                                        }


                                    // They don't change, but for now
                                    case "HarmonyLib.MethodType":
                                        {
                                            HarmonyLib.MethodType methodType = (HarmonyLib.MethodType)arg.Value;
                                            methodStr = GetHarmonyLibMethodTypeString(methodType, methodStr);
                                            break;
                                        }


                                    default:
                                        {
                                            //Console.WriteLine(arg.Type.FullName);
                                            break;
                                        }
                                }
                            }
                        }
                    }

                    if (methodStr != "")
                    {

                        typeStr = typeStr.Replace('/', '.');
                        Console.WriteLine(
                            $"[{type.Module.Name}] {typeStr}::{methodStr}"
                        );
                        string str = typeStr + "::" + methodStr;
                        _methodConflicts[str.Replace("/", ".")] = new ConflictData(typeStr.ToString(), methodStr.ToString());
                        _typeConflicts[typeStr] = new ConflictData(typeStr.ToString(), "");

                    }
                }
            }

            _modResolver.Dispose();
        }
    }
}

