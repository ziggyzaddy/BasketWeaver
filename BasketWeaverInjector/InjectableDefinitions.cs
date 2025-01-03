using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BasketWeaverInjector
{
    public class InjectableDefinitions
    {
        public Dictionary<string, AssemblyDefinition> Definitions = new Dictionary<string, AssemblyDefinition>();
        IAssemblyResolver Resolver;
        public InjectableDefinitions(IAssemblyResolver resolver, ModConfig config)
        {
            Resolver = resolver;
            foreach (var path in config.InjectableDefinitions)
            {
                bool resolved = false;
                AssemblyDefinition assem = null;                
                try
                {
                    assem = resolver.Resolve(
                        new Mono.Cecil.AssemblyNameReference(path.Replace(".dll", ""), null)
                    );
                    resolved = true;
                }
                catch (Exception a)
                {
                    Console.WriteLine($"FAIL - Could not resolve {path}");
                    Console.WriteLine(a.ToString());
                    resolved = false;
                }
                if (resolved && (assem != null))
                {
                    // .Name contains the name as Module.dll
                    Definitions.Add(assem.MainModule.Name, assem);
                }
            }
        }

        // Use name of module (with .dll)
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool GetModuleByName(string Name, out AssemblyDefinition assembly)
        {
            if (Definitions.ContainsKey(Name + ".dll"))
            {
                assembly = Definitions[Name + ".dll"];
                return true;
            }
            else
            {
                assembly = null;
                return false;
            }
        }

        // Use name of module (with .dll)
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool GetModuleByFile(string FileName, out AssemblyDefinition assembly)
        {
            if (Definitions.ContainsKey(FileName))
            {
                assembly = Definitions[FileName];
                return true;
            }
            else
            {
                assembly = null;
                return false;
            }
        }

        // Looks up type from loaded dependencies to avoid requiring manual configuration of .dlls to inject into.
        [MethodImplAttribute(MethodImplOptions.AggressiveInlining)]
        public bool FindType(
            string fullName,
            out TypeDefinition typeDef
        )
        {
            foreach (var definition in Definitions)
            {
                var def = definition.Value.MainModule.GetType(fullName);
                if (def != null)
                {
                    Console.WriteLine($"Found [{def.Module.Name}] {def.FullName}");
                    typeDef = def;
                    return true;
                }
            }
            typeDef = null;
            return false;
        }
    }
}
