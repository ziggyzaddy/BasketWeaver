using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;


namespace BasketWeaverInjector
{

    public class Utils
    {
        // Remove the Wrapper Namespace when injecting
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string FixName(string srcNamespace, string srcFullName)
        {
            return srcFullName.Replace(srcNamespace + ".", "");
        }

        // Extracts a signature from a method definition that is used as a unique handle. Avoids overload confusion
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ExtractSig(MethodDefinition method)
        {
            string args = "";
            foreach (var srcParam in method.Parameters)
            {
                args += srcParam.ParameterType.Name;
            }
            string sigStr = $"{method.ReturnType.Name}{method.Name}{args}";
            return sigStr;
        }

        // Extracts a signature from a method reference that is used as a unique handle.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ExtractSig(MethodReference method)
        {
            string args = "";
            foreach (var srcParam in method.Parameters)
            {
                args += srcParam.ParameterType.Name;
            }
            string sigStr = $"{method.ReturnType.Name}{method.Name}{args}";
            return sigStr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string PrettyPrintSig(MethodDefinition method)
        {
            string args = "";
            foreach (var srcParam in method.Parameters)
            {
                args += srcParam.ParameterType.Name + " ";
            }
            string sigStr = $"{method.ReturnType.Name} {method.Name}({args})";
            return sigStr;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string PrettyPrintSig(MethodReference method)
        {
            string args = "";
            foreach (var srcParam in method.Parameters)
            {
                args += srcParam.ParameterType.Name + " ";
            }
            string sigStr = $"{method.ReturnType.Name} {method.Name}({args})";
            return sigStr;
        }


    }
}
