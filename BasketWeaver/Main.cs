using HarmonyLib;
using HBS.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;

namespace BasketWeaver;

public static class Main
{
    private static readonly ILog Console = Logger.GetLogger(nameof(BasketWeaver));


    // Use this mod for visibility into weaving process. For future decompiler capabilities
    public static void Init(string directory, string settingsJSON)
    {

        Settings config;
        Console.Log($"INIT: Loading Settings {settingsJSON}. Directory {directory}");
        try
        {
            config = JsonConvert.DeserializeObject<Settings>(settingsJSON);
        }
        catch (Exception e)
        {
            Console.Log($"INIT: Loading Settings (FAIL) -> Initialize Defaults.");
            config = new Settings();
            Console.LogException(e);
        }
        Console.Log($"RUN: Patching");

        // Patch [HarmonyPatch] annotations
        Harmony.CreateAndPatchAll(typeof(Main).Assembly);
        // Patch others
        Harmony.CreateAndPatchAll(typeof(Main));

        Console.Log($"RUN: Started");

        Console.Log($"RUN: Benchmarking");
        UnityCoreMathTest unityCoreMathTest = new UnityCoreMathTest();
        unityCoreMathTest.RunBenchmark(Console);
    }



    // Development use only
    private static void PKillUnity()
    {
        Console.Log($"RUN: Killing Unity PID: {Process.GetCurrentProcess().Id}");
        Process.GetCurrentProcess().Kill();
    }
}
