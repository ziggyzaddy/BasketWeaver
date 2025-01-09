using System.Collections.Generic;

namespace BasketWeaver
{

    // Must be placed alongside ZeBasketWeaverInjector.dll
    public class Settings
    {
        public string HelperNamespace = "Inject";
        public bool PrintReplacedIL = true;
        public bool PrintDiffIL = false;

        public bool RunHelpers = true;
        public bool RunAutoInline = false;
        public bool HackBool = true;

        // Remove existing AggressiveInlines and inject NoInlining to avoid Harmony mispatch. Theorized due to JIT
        public bool AutoInlineSafeMode = false;

        // List of AssemblyDefinitions to AutoInline. Generally located in Managed, but can pull Harmony
        public List<string> AutoInline { get; set; }

        // Override to force inlining a method. Disable by Safe Mode
        public List<string> AutoInlineMethods { get; set; 
        
        }
        // Override to force inlining a method. Disable by Safe Mode
        public List<string> AutoInlineTypes { get; set; }

        // List of all definitions that can be injected into. Provides filtering in case inlining breaks
        public List<string> InjectableDefinitions { get; set; }



        // Initialize all the lists
        public Settings()
        {
            AutoInline = new List<string>();
            AutoInlineMethods = new List<string>();
            AutoInlineTypes = new List<string>();
            InjectableDefinitions = new List<string>();
        }


    }
}
