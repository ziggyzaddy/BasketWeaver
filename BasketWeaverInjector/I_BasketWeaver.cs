using BasketWeaverInjector;
using Mono.Cecil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BasketWeaverInjector
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
        private string SettingsFile = "BasketWeaverInjector.json";


        ConflictDetect Conflicts;
        InjectableDefinitions Definitions;

        // Finds the configuration setting for the injectors. Otherwise, use defaults
        public ModConfig GetModConfig()
        {
            string curDir = Directory.GetCurrentDirectory();
            string searchPath = Path.Combine(curDir + InjectorPath);
            ModConfig config = new ModConfig();

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
                        config = JsonConvert.DeserializeObject<ModConfig>(jsonText);
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
            return new ModConfig();
        }

        // Routine called by the injector.
        public void Inject(IAssemblyResolver resolver)
        {

            var config = GetModConfig();
            
            Definitions = new InjectableDefinitions(resolver, config);
            Conflicts = new ConflictDetect();

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
            ModConfig config,
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
                }
            }
        }



        // Run the autoinliner to add the AggressiveInlining attribute to certain functions
        private static void RunAutoInline(
            ModConfig config,
            InjectableDefinitions definitions,
            ConflictDetect conflict
        )
        {
            foreach (var toInline in config.AutoInline)
            {
                if(definitions.GetModuleByName(toInline, out var assembly))
                {
                    Console.WriteLine("Got Module\n");
                    AutoInline.Run(
                        conflict,
                        assembly,
                        16
                    );
                }

            }

        }
    }
}
