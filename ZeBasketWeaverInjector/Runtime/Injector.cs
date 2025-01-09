using Mono.Cecil;
using System.Collections.Generic;

namespace BasketWeaver
{
    public static class Injector
    {
        // Does not provide ability to add search paths. Only for pushing edits for ModTek injection
        public static void Inject(IAssemblyResolver resolver)
        {
            foreach (var injector in GetInjectors())
            {
                injector.Inject(resolver);
            }
        }

        // Following the references
        private static IEnumerable<IInjector> GetInjectors()
        {
            yield return new I_BasketWeaver();
        }
    }
}
