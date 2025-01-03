using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    Definitions.Add(assem.FullName, assem);
                }
            }
        }

        public void Close()
        {
        }

        // Looks up type from loaded dependencies to avoid requiring manual configuration of .dlls to inject into.
        public bool FindType(
            string fullName,
            out TypeDefinition typeDef
        )
        {
            foreach (var key in Definitions.Keys)
            {
                var def = Definitions[key].MainModule.GetType(fullName);
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
