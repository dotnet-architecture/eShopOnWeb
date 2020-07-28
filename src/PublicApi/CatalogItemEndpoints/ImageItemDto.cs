namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints
{
    public class FileItemDto
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string Ext { get; set; }
        public string Type { get; set; }
        public string DataBase64 { get; set; }
    }
}