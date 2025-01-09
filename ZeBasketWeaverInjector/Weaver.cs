using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// BasketWeaver namespace reserved for Helpers and Mod types
namespace BasketWeaver
{
    public class Weaver
    {
        // Hack for redirecting to a log, disabled for now
        //static StreamWriter Console = new StreamWriter("BasketWeaverLog.md");

        // Add new fields to a class. If a non-trivial constructor is required, you must define a constructor alongside the field to be added
        public static void AddFields(AssemblyDefinition srcAssembly, AssemblyDefinition tgtAssembly, string srcNamespace, TypeDefinition srcType, TypeDefinition tgtType)
        {
            Dictionary<string, FieldDefinition> srcFields = new Dictionary<string, FieldDefinition>();
            Dictionary<string, FieldDefinition> tgtFields = new Dictionary<string, FieldDefinition>();

            Console.WriteLine("\n### FIELDS:");
            //Console.WriteLine("```csharp");
            foreach (var srcField in srcType.Fields)
            {
                string srcSig = Utils.FixName(srcNamespace, srcField.FullName);
                //Console.WriteLine($" {srcSig}");
                srcFields.Add(srcSig, srcField);
            }

            foreach (var tgtField in tgtType.Fields)
            {
                //Console.WriteLine($" {tgtField.FullName}");
                tgtFields.Add(tgtField.FullName, tgtField);
            }

            foreach (var srcFieldData in srcFields)
            {
                var srcField = srcFieldData.Value;

                if (tgtFields.TryGetValue(srcFieldData.Key, out FieldDefinition tgtField))
                {
                    Console.WriteLine($"{tgtField.FullName}");
                }
                else
                {
                    if (srcField.FieldType.FullName.Contains(srcNamespace))
                    {
                        if (srcFieldData.Key == tgtType.FullName)
                        {
                            tgtType.Fields.Add(new FieldDefinition(srcField.Name, srcField.Attributes, tgtType));
                        }
                        else
                        {
                            string newTypeStr = Utils.FixName(srcNamespace, srcField.FieldType.FullName);
                            var newType = tgtAssembly.MainModule.GetType(newTypeStr);
                            tgtType.Fields.Add(new FieldDefinition(srcField.Name, srcField.Attributes, tgtAssembly.MainModule.ImportReference(newType)));
                        }
                        tgtType.Fields.Last().HasConstant = srcField.HasConstant;
                        tgtType.Fields.Last().Constant = srcField.Constant;
                        tgtType.Fields.Last().InitialValue = srcField.InitialValue;
                        string fullName = tgtType.Fields.Last().FullName;
                        Console.WriteLine($"{fullName} (+)");
                    }
                    else
                    {
                        tgtType.Fields.Add(new FieldDefinition(srcField.Name, srcField.Attributes, srcField.FieldType));
                        tgtType.Fields.Last().HasConstant = srcField.HasConstant;
                        tgtType.Fields.Last().Constant = srcField.Constant;
                        tgtType.Fields.Last().InitialValue = srcField.InitialValue;
                        string fullName = tgtType.Fields.Last().FullName;
                        Console.WriteLine($"{fullName} (+)");
                    }
                }
            }
            //Console.WriteLine("```");
        }


        // The heart of the injector, takes a target type from a source assembly and injects it into a target.
        public static void Run(
            AssemblyDefinition srcAssembly,
            AssemblyDefinition tgtAssembly,
            string srcNamespace,
            string tgtTypeStr,
            bool printIL = false,
            bool printILDiff = false)
        {
            string srcTypeFullName = $"{srcNamespace}.{tgtTypeStr}";

            var tgtType = tgtAssembly.MainModule.GetType(tgtTypeStr) ?? throw new Exception($"Can't find target type: {tgtTypeStr}");
            var srcType = srcAssembly.MainModule.GetType(srcTypeFullName) ?? throw new Exception($"Can't find target type: {srcTypeFullName}");

            Console.WriteLine($"\n## WEAVING: {srcType.FullName} -> {tgtType.FullName}");

            AddFields(srcAssembly, tgtAssembly, srcNamespace, srcType, tgtType);

            if (Utils.FixName(srcNamespace, srcType.FullName) == tgtType.FullName)
            {
                Dictionary<string, MethodDefinition> srcMethods = new Dictionary<string, MethodDefinition>();
                Dictionary<string, MethodDefinition> tgtMethods = new Dictionary<string, MethodDefinition>();

                foreach (var srcMethod in srcType.Methods)
                {
                    string srcSig = Utils.ExtractSig(srcMethod);
                    srcMethods.Add(srcSig, srcMethod);
                }

                foreach (var tgtMethod in tgtType.Methods)
                {
                    string tgtSig = Utils.ExtractSig(tgtMethod);
                    tgtMethods.Add(tgtSig, tgtMethod);
                }

                foreach (var srcMethodData in srcMethods)
                {
                    
                    var srcMethod = srcMethodData.Value;

                    if (!tgtMethods.TryGetValue(srcMethodData.Key, out MethodDefinition tgtMethod))
                    {
                        Console.WriteLine("\n### NEW METHOD:");

                        var newMethod = new MethodDefinition(srcMethod.Name, srcMethod.Attributes, srcMethod.ReturnType);
                        tgtType.Methods.Add(newMethod);
                        var editMethod = tgtType.Methods.Last();
                        editMethod.DeclaringType = tgtType;
                        editMethod.Body.Variables.Clear();
                        editMethod.IsAbstract = srcMethod.IsAbstract;
                        editMethod.IsStatic = srcMethod.IsStatic;
                        editMethod.IsUnmanagedExport = srcMethod.IsUnmanagedExport;
                        foreach (var variable in srcMethod.Body.Variables)
                        {
                            var newVariable = new VariableDefinition(tgtType.Module.ImportReference(variable.VariableType));
                            editMethod.Body.Variables.Add(newVariable);
                        }
                        
                        foreach (var param in srcMethod.Parameters)
                        {
                            editMethod.Parameters.Add(param);
                        }
                            
                        editMethod.Body.MaxStackSize = srcMethod.Body.MaxStackSize;
                        editMethod.Body = new MethodBody(srcMethod);
                        var proc = editMethod.Body.GetILProcessor();
                        
                        string tgtSig = Utils.ExtractSig(editMethod);
                        tgtMethods.Add(tgtSig, editMethod);
                        Formatter.PrintMethod(editMethod);

                    }
                }


                foreach (var srcMethodData in srcMethods)
                {
                    var srcMethod = srcMethodData.Value;
                    if (tgtMethods.TryGetValue(srcMethodData.Key, out MethodDefinition tgtMethod))
                    {
                        Console.WriteLine($"\n### METHOD: {tgtMethod.FullName}");

                        // May be dangerous to add methods to a body. TODO: Handle edges
                        if (!tgtMethod.HasBody) { continue; }

                        // Prints difference of IL to log. 
                        if (printILDiff)
                        {
                            Console.WriteLine("\n### IL SOURCE:");
                            Formatter.PrintMethod(srcMethod);

                            Console.WriteLine("\n### IL TARGET:");
                            Formatter.PrintMethod(tgtMethod);
                        }


                        // Resolve references in target assembly scope
                        SyncMethod(tgtMethod, srcMethod);
                        SyncVariables(tgtAssembly, srcNamespace, tgtType, tgtMethod, srcMethod);
                        SyncInstructions(tgtAssembly, srcNamespace, tgtType, tgtMethod, srcMethod);


                        // Prints replaced IL. TODO: Decompile later
                        if (printIL)
                        {
                            Console.WriteLine("\n### IL REPLACED:");
                            Formatter.PrintMethod(tgtMethod);
                        }
                    }
                }
            }
            //Console.Flush();
        }

        // Synchronizes the method attributes. Generally prefer preserving the attributes of the target method, as helpers can be partially defined
        private static void SyncMethod(MethodDefinition tgtMethod, MethodDefinition srcMethod)
        {
            // Only set inlining to true, never undo it
            if (srcMethod.AggressiveInlining == true)
            {
                tgtMethod.AggressiveInlining = true;
            }
            tgtMethod.HasSecurity = false;
        }

        // Variable definition in a method need to be synchronized in case new variables are required
        private static void SyncVariables(AssemblyDefinition tgtAssembly, string srcNamespace, TypeDefinition tgtType, MethodDefinition tgtMethod, MethodDefinition srcMethod)
        {
            //Console.WriteLine("```csharp");
            tgtMethod.Body.Variables.Clear();
            foreach (var srcVariable in srcMethod.Body.Variables)
            {
                if (Utils.FixName(srcNamespace, srcVariable.VariableType.FullName) == tgtType.FullName)
                {
                    var varDef = new VariableDefinition(tgtType);
                    tgtMethod.Body.Variables.Add(varDef);
                    //Console.WriteLine(String.Format(varFormat, varDef.Index.ToString().PadLeft(2, '0'), tgtMethod.Body.Variables.Last().VariableType.FullName));
                }
                else if (srcVariable.VariableType.FullName.Contains(srcNamespace))
                {
                    var type = tgtAssembly.MainModule.GetType(Utils.FixName(srcNamespace, srcVariable.VariableType.FullName));
                    var typeRef = tgtAssembly.MainModule.ImportReference(type.Resolve());
                    var varDef = new VariableDefinition(typeRef);
                    tgtMethod.Body.Variables.Add(varDef);
                    //Console.WriteLine(String.Format(varFormat, varDef.Index.ToString().PadLeft(2, '0'), tgtMethod.Body.Variables.Last().VariableType.FullName));
                }
                else
                {
                    var type = tgtAssembly.MainModule.ImportReference(srcVariable.VariableType.Resolve());
                    var varDef = new VariableDefinition(type);
                    tgtMethod.Body.Variables.Add(varDef);
                    //Console.WriteLine(String.Format(varFormat, varDef.Index.ToString().PadLeft(2, '0'), tgtMethod.Body.Variables.Last().VariableType.FullName));
                }
            }
            //Console.WriteLine("```");
        }

        private static void SyncInstructions(AssemblyDefinition tgtAssembly, string srcNamespace, TypeDefinition tgtType, MethodDefinition tgtMethod, MethodDefinition srcMethod)
        {
            // Remove the target method instructions first. Operands need retargeting
            tgtMethod.Body.Instructions.Clear();
            var processor = tgtMethod.Body.GetILProcessor();

            // TODO: I don't think this does anything. Might need to be called on srcMethod instead, and optimize on target method
            tgtMethod.Body.SimplifyMacros();

            // Iterate through all operands and retarget them within the scope of the target method. Remove wrapper namespace as needed to get FullNames
            foreach (Instruction instruction in srcMethod.Body.Instructions)
            {
                if (instruction.Operand == null)
                {
                    processor.Emit(instruction.OpCode);
                    continue;
                }
                switch (instruction.Operand.GetType().Name)
                {
                    case "FieldDefinition":
                        {
                            FieldDefinition field = (FieldDefinition)instruction.Operand;

                            if (field.DeclaringType.Name == tgtType.Name)
                            {
                                var newField = tgtType.Fields.FirstOrDefault(f => f.Name == field.Name);
                                var fieldRef = tgtAssembly.MainModule.ImportReference(newField);
                                newField.Attributes = field.Attributes;
                                processor.Emit(instruction.OpCode, newField);
                            }
                            else
                            {
                                var type = tgtAssembly.MainModule.GetType(Utils.FixName(srcNamespace, field.DeclaringType.FullName));
                                var newField = type.Fields.FirstOrDefault(f => f.Name == field.Name);
                                processor.Emit(instruction.OpCode, newField);
                            }
                            break;
                        }
                    case "ParameterDefinition":
                        {
                            ParameterDefinition param = (ParameterDefinition)instruction.Operand;
                            var newParam = tgtMethod.Parameters.FirstOrDefault(f => f.Name == param.Name);
                            processor.Emit(instruction.OpCode, newParam);
                            break;
                        }
                    case "MethodDefinition":
                        {
                            MethodDefinition method = (MethodDefinition)instruction.Operand;
                            if (method.DeclaringType.Name == tgtType.Name)
                            {
                                string sig = Utils.ExtractSig(method);
                                var newMethod = tgtType.Methods.First(f => Utils.ExtractSig(f) == sig);
                                processor.Emit(instruction.OpCode, newMethod);
                            }
                            else if (method.DeclaringType.FullName.Contains(srcNamespace))
                            {
                                string sig = Utils.ExtractSig(method);
                                var type = tgtAssembly.MainModule.GetType(Utils.FixName(srcNamespace, method.DeclaringType.FullName));
                                var newMethod = type.Methods.First(f => Utils.ExtractSig(f) == sig);

                                //var refMethod = tgtAssembly.MainModule.ImportReference(method.Resolve());
                                processor.Emit(instruction.OpCode, newMethod);
                            }
                            else
                            {
                                Console.WriteLine($"{method.FullName}");
                                processor.Emit(instruction.OpCode, method);
                            }
                            break;
                        }
                    case "MethodReference":
                        {
                            MethodReference method = (MethodReference)instruction.Operand;
                            var newMethod = tgtAssembly.MainModule.ImportReference(method);
                            processor.Emit(instruction.OpCode, newMethod);
                            break;
                        }
                    case "FieldReference":
                        {
                            FieldReference field = (FieldReference)instruction.Operand;
                            var fieldReference = tgtAssembly.MainModule.ImportReference(field.Resolve());
                            processor.Emit(instruction.OpCode, fieldReference);
                            break;
                        }
                    case "TypeReference":
                        {
                            TypeReference type = (TypeReference)instruction.Operand;
                            var typeRef = tgtAssembly.MainModule.ImportReference(type.Resolve());
                            processor.Emit(instruction.OpCode, typeRef);
                            break;
                        }
                    case "VariableDefinition":
                        {
                            VariableDefinition variable = (VariableDefinition)instruction.Operand;
                            processor.Emit(instruction.OpCode, tgtMethod.Body.Variables[variable.Index]);
                            break;
                        }

                    case "TypeDefinition":
                        {
                            TypeDefinition type = (TypeDefinition)instruction.Operand;

                            if (type.Name == tgtType.Name)
                            {
                                var typeRef = tgtAssembly.MainModule.GetType(Utils.FixName(srcNamespace, tgtType.FullName));
                                processor.Emit(instruction.OpCode, typeRef);
                            }
                            else if (type.FullName.Contains(srcNamespace))
                            {
                                var typeRef = tgtAssembly.MainModule.GetType(Utils.FixName(srcNamespace, tgtType.FullName));
                                processor.Emit(instruction.OpCode, typeRef);
                            }
                            else
                            {
                                processor.Emit(instruction.OpCode, type);
                            }
                            break;
                        }
                    case "Single":
                        {
                            System.Single single = (System.Single)instruction.Operand;
                            processor.Append(instruction);
                            break;
                        }
                    case "Mono.Cecil.Cil.Instruction":
                        {
                            Instruction inst = (Mono.Cecil.Cil.Instruction)instruction.Operand;
                            processor.Append(inst);
                            break;
                        }
                    case "String":
                        {
                            String str = (string)instruction.Operand;
                            processor.Append(instruction);
                            break;
                        }
                    default:
                        {
                            processor.Append(instruction);
                            break;
                        }
                }
            }

            // When netstandard2.0 becomes available, separate library can provide ICSharpCode decompiler to visualize replaces methods for comparison
            //Decompile(resolver, tgtAssembly, tgtMethod);
            tgtMethod.Body.Optimize();
        }


    }
}