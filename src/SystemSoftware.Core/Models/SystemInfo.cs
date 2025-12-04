namespace SystemSoftware.Core.Models
{
    public class SystemInfo
    {
        public int Id { get; set; }
        public string OSName { get; set; } = string.Empty;
        public string OSVersion { get; set; } = string.Empty;
        public DateTime LastUpdated { get; set; }
        public bool IsActive { get; set; }
    }
}
