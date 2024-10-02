namespace ShareMarket.Core.Models.Configuration;
public class StaticFileConfiguration
{
    public List<string> AllowedExtention    { get; set; } = [];
    public string       ProfileImageName    { get; set; } = string.Empty;
    public int          MaxFileSize         { get; set; }
    public string       RootFolder          { get; set; } = string.Empty;
    public string       SubFolder           { get; set; } = string.Empty;
    public string       FamilyTree          { get; set; } = string.Empty;
    public string       Finance             { get; set; } = string.Empty;
    public List<string> FinanceExtention    { get; set; } = [];
}