using System.Collections.Generic;

namespace BasketWeaverInjector
{

    // Must be placed alongside BasketWeaverInjector.dll
    public class ModConfig
    {
        public string HelperNamespace = "BasketWeaver";
        public bool PrintReplacedIL = true;
        public bool PrintDiffIL = false;

        public bool RunHelpers = true;
        public bool RunAutoInline = false;

        public List<string> AutoInline { get; set; }
        public List<string> InjectableDefinitions { get; set; }



        public ModConfig()
        {
            AutoInline = new List<string>();
        }

    }
}
