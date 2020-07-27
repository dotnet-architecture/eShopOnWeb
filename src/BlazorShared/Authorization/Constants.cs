namespace BlazorShared.Authorization
{
    public static class Constants
    {
        public static class Roles
        {
            public const string ADMINISTRATORS = "Administrators";
        }
        public static string GetApiUrl(bool inDocker) =>
            inDocker ? DOCKER_API_URL : API_URL;

        public static string GetWebUrl(bool inDocker) =>
            inDocker ? DOCKER_WEB_URL : WEB_URL;

        public static string GetOriginWebUrl(bool inDocker) =>
            GetWebUrl(inDocker).TrimEnd('/');

        private const string API_URL = "https://localhost:5099/api/";
        private const string DOCKER_API_URL = "http://localhost:5200/api/";

        private const string WEB_URL = "https://localhost:44315/";
        private const string DOCKER_WEB_URL = "http://localhost:5106/";

    }
}
