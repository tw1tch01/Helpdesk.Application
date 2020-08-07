namespace Helpdesk.WebAPI.Configuration
{
    public class AccessControl
    {
        public const string ConfigurationSectionName = nameof(AccessControl);

        public string Url { get; set; }
        public string Scope { get; set; }
    }
}