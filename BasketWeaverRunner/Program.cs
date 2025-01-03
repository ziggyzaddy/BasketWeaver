using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Mono.Cecil;

namespace BasketWeaverInjector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // See https://aka.ms/new-console-template for more information



            string BattleTechGameDir = "E:/SteamLibrary/steamapps/common/BATTLETECH/";


            FileStream filestream = new FileStream("Runner.log", FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            Console.WriteLine($"BasketWeaverRunner: Started");

            DefaultAssemblyResolver resolver = new DefaultAssemblyResolver();
            
            resolver.AddSearchDirectory(Path.Combine(BattleTechGameDir, "BattleTech_Data/Managed/"));
            resolver.AddSearchDirectory(Path.Combine(BattleTechGameDir, "Mods/ModTek/"));

            Directory.SetCurrentDirectory(BattleTechGameDir);
            Assembly a = Assembly.LoadFile(Path.Combine(BattleTechGameDir, "Mods/Modtek/Injectors/BasketWeaverInjector.dll"));
            foreach(var type in a.GetTypes())
                {
                Console.WriteLine(type.FullName);
            }
            Type t = a.GetType("BasketWeaverInjector.Injector");
            Console.WriteLine(t.ToString());
            MethodInfo m = t.GetMethod("Inject");
            Console.Write(m.Name);

            try
            {
            
                m.Invoke(null, new object[] {resolver});

                //BasketWeaverInjector.Injector.Inject(resolver);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
