using Mono.Cecil;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BasketWeaver
{
    // Inspired by ILspy, dnspy, and DotPeek
    public class Formatter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrintMethod(MethodDefinition methodDef)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("```csharp");

            PrintMethodBlock(methodDef, sb);

            sb.AppendLine("\n{");
            if (methodDef.Body.MaxStackSize > 0)
            {
                sb.AppendLine($"  .maxstack {methodDef.Body.MaxStackSize}");
            }
            PrintVariableBlock(methodDef, sb);
            PrintInstructionBlock(methodDef, sb);
            sb.AppendLine("}");
            sb.AppendLine("```");
            Console.Write(sb.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrintInstructionBlock(MethodDefinition methodDef, StringBuilder sb)
        {
            string ilFormat = "  IL_{0,-4} {1,-10}";
            if (methodDef.Body.Instructions.Count > 0)
            {
                sb.Append("\n");
                foreach (var instruction in methodDef.Body.Instructions)
                {
                    sb.AppendFormat(
                        ilFormat,
                        instruction.Offset.ToString("x").PadLeft(4, '0') + ":",
                        instruction.OpCode
                    );

                    if (instruction.Operand != null)
                    {
                        if (instruction.Operand.GetType().Name == "MethodDefinition")
                        {
                            var def = (MethodDefinition)instruction.Operand;

                            sb.Append($"[{def.Module.Name.Replace(".dll", "")}] ");
                        }

                        if (instruction.Operand.GetType().Name == "String")
                        {
                            sb.Append($"\"{instruction.Operand.ToString()}\"");
                        }
                        else
                        {
                            sb.Append(instruction.Operand.ToString());
                        }
                    }
                    sb.Append("\n");
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrintVariableBlock(MethodDefinition methodDef, StringBuilder sb)
        {
            string varFormat = "    {0,-4} {1,-10}";
            if (methodDef.Body.Variables.Count > 0)
            {
                string hasLocal = methodDef.Body.InitLocals ? " init" : "";

                sb.AppendLine($"  .locals{hasLocal} (");
                foreach (var variable in methodDef.Body.Variables)
                {
                    sb.AppendFormat(varFormat, $"[{variable.Index}]", variable.VariableType);

                    if (variable != methodDef.Body.Variables.Last())
                    {
                        sb.Append(",\n");
                    }
                    else
                    {
                        sb.Append("\n");
                    }
                }
                sb.Append("  )\n");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PrintMethodBlock(MethodDefinition methodDef, StringBuilder sb)
        {
            sb.AppendLine(
                $".method "
                    +
                    // Attribute Flags
                    $"{(methodDef.IsPrivate ? "private " : "")}"
                    + $"{(methodDef.IsFamilyAndAssembly ? "famandassem " : "")}"
                    + $"{(methodDef.IsAssembly ? "assembly " : "")}"
                    + $"{(methodDef.IsFamily ? "family " : "")}"
                    + $"{(methodDef.IsFamilyOrAssembly ? "famorassem " : "")}"
                    + $"{(methodDef.IsPublic ? "public " : "")}"
                    +
                    // Visibility
                    $"{(methodDef.IsFinal ? "final " : "")}"
                    + $"{(methodDef.IsHideBySig ? "hidebysig " : "")}"
                    + $"{(methodDef.IsSpecialName ? "specialname " : "")}"
                    + $"{(methodDef.IsUnmanagedExport ? "export " : "")}"
                    + $"{(methodDef.IsRuntimeSpecialName ? "rtspecialname " : "")}"
                    + $"{(methodDef.IsNewSlot ? "newslot " : "")}"
                    + $"{(methodDef.IsCheckAccessOnOverride ? "strict " : "")}"
                    + $"{(methodDef.IsAbstract ? "abstract " : "")}"
                    + $"{(methodDef.IsVirtual ? "virtual " : "")}"
                    + $"{(methodDef.IsStatic ? "static " : "")}"
            );
            sb.Append(
                $"  {methodDef.ReturnType.FullName} {methodDef.DeclaringType.FullName}{"::"}{methodDef.Name} ("
            );

            if (methodDef.Parameters.Count > 0)
            {
                sb.Append('\n');
                foreach (var param in methodDef.Parameters)
                {
                    if (param != methodDef.Parameters.Last())
                    {
                        sb.AppendLine($"    {param.ParameterType.ToString()} {param.Name},");
                    }
                    else
                    {
                        sb.AppendLine($"    {param.ParameterType.ToString()} {param.Name}");
                    }
                }
            }

            sb.Append(
                $") "
                    +
                    // Code Type
                    $"{(methodDef.IsIL ? "cil " : "")}"
                    + $"{(methodDef.IsNative ? "native " : "")}"
                    + $"{(methodDef.IsRuntime ? "runtime " : "")}"
                    + $"{(methodDef.IsManaged ? "managed " : "")}"
                    + $"{(methodDef.IsUnmanaged ? "unmanaged" : "")}"
                    +
                    // Impl
                    $"{(methodDef.IsSynchronized ? "synchronized " : "")}"
                    + $"{(methodDef.NoInlining ? "noinlining " : "")}"
                    + $"{(methodDef.NoOptimization ? "nooptimization " : "")}"
                    + $"{(methodDef.IsPreserveSig ? "preservesig " : "")}"
                    + $"{(methodDef.IsInternalCall ? "internalcall " : "")}"
                    + $"{(methodDef.IsForwardRef ? "forwardref " : "")}"
                    + $"{(methodDef.AggressiveInlining ? "aggressiveinlining " : "")}"
            );
        }
    }
}
