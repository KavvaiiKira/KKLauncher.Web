namespace KKLauncher.Web.Client.Models
{
    public class DynamicComponentData
    {
        public Type Type { get; private set; }

        public Dictionary<string, object> Parameters { get; private set; }

        public string Title { get; private set; }

        public DynamicComponentData(Type type, string title, Dictionary<string, object>? parameters = null)
        {
            Type = type;
            Title = title;
            Parameters = parameters ?? new Dictionary<string, object>();
        }
    }
}
