using Mono.Cecil;

namespace BasketWeaver
{
    public interface IInjector
    {
        // IAssemblyResolver interface for ModTek
        void Inject(IAssemblyResolver resolver);
    }
}
