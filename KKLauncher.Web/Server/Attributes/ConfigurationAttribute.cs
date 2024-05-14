namespace KKLauncher.Web.Server.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ConfigurationAttribute : Attribute
    {
        public readonly string FileName;

        public ConfigurationAttribute(string fileName)
        {
            FileName = fileName + ".json";
        }
    }
}
