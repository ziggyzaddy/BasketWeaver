
using Mono.Cecil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BasketWeaver
{
    // Contains the Injector runtime and initialization logic
    public class I_BasketWeaver : IInjector
    {
        // Mods/* and Mods/Core/* are the only valid locations for the Mod.
        private List<string> helperPaths = new List<string>()
        {
            "Mods/BasketWeaver/Helpers/",
            "Mods/Core/BasketWeaver/Helpers/",
        };

        private string InjectorPath = "/Mods/ModTek/Injectors";

        // Must be local to the injector
        private string SettingsFile = "ZeBasketWeaverInjector.json";


        ConflictDetect Conflicts;
        InjectableDefinitions Definitions;

        // Finds the configuration setting for the injectors. Otherwise, use defaults
        public Settings GetModConfig()
        {
            string curDir = Directory.GetCurrentDirectory();
            string searchPath = Path.Combine(curDir + InjectorPath);
            Settings config = new Settings();

            bool foundSettings = false;
            if (Directory.Exists(searchPath))
            {
                string settingsLocation = Path.GetFullPath(Path.Combine(searchPath, SettingsFile));
                if (File.Exists(settingsLocation))
                {
                    Console.WriteLine($"Found Setting: {settingsLocation}");
                    try
                    {
                        string jsonText = File.ReadAllText(settingsLocation);
                        config = JsonConvert.DeserializeObject<Settings>(jsonText);
                        foundSettings = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"FAIL - Could not deserialize settings");
                        Console.WriteLine(e.ToString());
                        foundSettings = false;
                    }
                }
            }

            if (foundSettings)
            {
                return config;
            }
            Console.WriteLine($"Settings not found, using defaults");
            return new Settings();
        }

        // Routine called by the injector.
        public void Inject(IAssemblyResolver resolver)
        {

            var config = GetModConfig();
            
            Definitions = new InjectableDefinitions(resolver, config);
            Conflicts = new ConflictDetect();
            Conflicts.CheckDefinition(Definitions);

            System.Diagnostics.Stopwatch helperSW = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch inlinerSW = new System.Diagnostics.Stopwatch();

            if (config.RunAutoInline)
            {
                Console.WriteLine($"\nAutoInline Injection Started");
                inlinerSW.Start();
                RunAutoInline(config, Definitions, Conflicts);
                inlinerSW.Stop();
                Console.WriteLine(
                    $"AutoInline Injection Took {inlinerSW.Elapsed.TotalMilliseconds} ms\n"
                );
            }

            if (config.RunHelpers)
            {
                Console.WriteLine($"\nHelper Injection Started");
                helperSW.Start();
                RunHelperInjection(config, Definitions);
                helperSW.Stop();
                Console.WriteLine($"Helper Injection Took {helperSW.Elapsed.TotalMilliseconds} ms");
            }
        }

        // Run the helper injection and iterate all types in the configured namespace and match them to their ingame equivalents
        private void RunHelperInjection(
            Settings config,
            InjectableDefinitions definitions
        )
        {
            string curDir = Directory.GetCurrentDirectory();
            string helperLocation = "";

            foreach (var searchPath in helperPaths)
            {
                // Use System.Path to clean up path strings and handle combination
                string path = Path.GetFullPath(searchPath);
                helperLocation = Path.GetFullPath(Path.Combine(curDir, path));

                if (Directory.Exists(helperLocation))
                {
                    Console.WriteLine($"Found Helpers: {helperLocation}");
                    break;
                }
            }
            
            var helperResolver = new Mono.Cecil.DefaultAssemblyResolver();

            List<AssemblyDefinition> helpers = new List<AssemblyDefinition>();
            if (Directory.Exists(helperLocation))
            {
                helperResolver.AddSearchDirectory(helperLocation);
                string[] helperDlls = Directory.GetFiles(
                    helperLocation,
                    "*.dll",
                    SearchOption.TopDirectoryOnly
                );

                foreach (var helperDll in helperDlls)
                {
                    Console.WriteLine($"Found Helper: {helperDll}");
                    helpers.Add(
                        helperResolver.Resolve(
                            new Mono.Cecil.AssemblyNameReference(
                                helperDll.Replace(".dll", ""),
                                null
                            )
                        )
                    );
                    


                    foreach (var type in helpers.Last().MainModule.Types)
                    {
                        Definitions.
                        FindType(
                            type.FullName.Replace(config.HelperNamespace + ".", ""),
                            out var typeDefinition
                        );
                        if (typeDefinition != null)
                        {
                            if (typeDefinition.FullName != "<Module>")
                            {
                                Weaver.Run(
                                    helpers.Last(),
                                    typeDefinition.Module.Assembly,
                                    config.HelperNamespace,
                                    typeDefinition.FullName,
                                    config.PrintReplacedIL,
                                    config.PrintDiffIL
                                );
                            }
                        }
                    }
                    
                    //// Type Prepass, add new types and retarget Namespace
                    //foreach (var type in helpers.Last().MainModule.Types)
                    //{
                    //    Definitions.
                    //    FindType(
                    //        type.FullName.Replace(config.HelperNamespace + ".", ""),
                    //        out var typeDefinition
                    //    );
                    //    if (typeDefinition == null)
                    //    {
                    //        try
                    //        {
                    //            Console.WriteLine($"### New Type: {type.FullName}");
                    //            var st_Hello_0 = new TypeDefinition("", "Hello", TypeAttributes.Sealed | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit | TypeAttributes.SequentialLayout | TypeAttributes.Public, helpers.Last().MainModule.ImportReference(typeof(System.ValueType)));
                    //            helpers.Last().MainModule.Types.Add(st_Hello_0);
                    //        }
                    //        catch (Exception e)
                    //        {
                    //            Console.WriteLine(e);
                    //        }
                    //    }
                    //}

                }
            }
        }



        // Run the autoinliner to add the AggressiveInlining attribute to certain functions
        private static void RunAutoInline(
            Settings config,
            InjectableDefinitions definitions,
            ConflictDetect conflict
        )
        {
            foreach (var toInline in config.AutoInline)
            {
                if(definitions.GetModuleByName(toInline, out var assembly))
                {
                    AutoInline.Run(
                        config,
                        conflict,
                        assembly,
                        16
                    );
                }

            }

        }
    }
}
