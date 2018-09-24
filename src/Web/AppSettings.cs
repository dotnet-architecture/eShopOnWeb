namespace Microsoft.eShopWeb.Web
{
    public class AppSettings
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public string CatalogBaseUrl { get; set; }
        public Logging Logging { get; set; }
    }

    public class Connectionstrings
    {
        public string CatalogConnection { get; set; }
        public string IdentityConnection { get; set; }
    }

    public class Logging
    {
        public bool IncludeScopes { get; set; }
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }
}
