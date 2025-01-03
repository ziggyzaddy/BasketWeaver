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


namespace BasketWeaverInjector
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
        Dictionary<string, ConflictData> ConflictDict = new Dictionary<string, ConflictData>();

        DefaultAssemblyResolver modResolver = new DefaultAssemblyResolver();

        List<string> AvoidDetect = new List<string>
        {
            "Assembly-CSharp.dll",
            "System.",
            "bass",
            "winhttp",
            "UnityEngine",
            "mono",
            "sqlite"

        };


        // Returns true if MethodDefinition conflicts
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MethodDefConflictCheck(MethodDefinition methodDef)
        {
            string conflictCheck = methodDef.DeclaringType.FullName + "::" + methodDef.Name;

            if (ConflictDict.TryGetValue(conflictCheck, out var conflict))
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

            if (ConflictDict.TryGetValue(conflictCheck, out var conflict))
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
            modResolver.AddSearchDirectory(Path.Combine(curDir, "/BattleTech_Data/Managed"));
            Console.WriteLine($"Running Conflict Detection for Harmony");

            foreach (var modDll in modDlls)
            {
                Console.WriteLine($"Adding Path {Path.GetDirectoryName(modDll)}");
                modResolver.AddSearchDirectory(Path.GetDirectoryName(modDll));
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

                if(modDll.Contains("Managed"))
                {
                    continue;
                }

                var assembly = modResolver.Resolve(new AssemblyNameReference(Path.GetFileNameWithoutExtension(modDll), null));

                // Do not add the injector to conflict detect.
                if (assembly.MainModule == null)
                {
                    continue;
                }
                foreach (var type in assembly.MainModule.Types)
                {
                    if (type == null) { continue; }
                    if (!type.HasCustomAttributes) { continue; }

                    //// Very simple state machine
                    //bool gotType = false;
                    //bool gotMethod = false;

                    var typeStr = "";
                    var methodStr = "";


                    foreach (var custAttr in type.CustomAttributes)
                    {

                        if (!custAttr.AttributeType.FullName.Contains("Harmony")) { continue; }

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
                        ConflictDict[str.Replace("/", ".")] = new ConflictData(typeStr.ToString(), methodStr.ToString());

                    }
                }
            }
                
            modResolver.Dispose();
        }
    }
}

