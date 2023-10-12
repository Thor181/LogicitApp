namespace LogicitApp.Shared
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class LocalizedNameAttribute : Attribute
    {
        public string LocalizedName { get; set; }

        public LocalizedNameAttribute(string localizedName)
        {
            LocalizedName = localizedName;
        }
    }
}
