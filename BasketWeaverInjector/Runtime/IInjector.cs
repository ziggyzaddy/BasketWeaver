using Mono.Cecil;

namespace BasketWeaverInjector
{
    public interface IInjector
    {
        // IAssemblyResolver interface for ModTek
        void Inject(IAssemblyResolver resolver);
    }
}
