using KKLauncher.Web.Client.Models;

namespace KKLauncher.Web.Client.Factories
{
    public interface IDynamicComponentFactory
    {
        DynamicComponentData Create(string componentKey);
    }
}
