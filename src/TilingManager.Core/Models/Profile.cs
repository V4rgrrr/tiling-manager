namespace TilingManager.Core.Models;

public class Profile
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Hotkey { get; set; } = string.Empty;
    public Layout SelectedLayout { get; set; } = new Layout();
    public List<ProfileApp> Apps { get; set; } = new List<ProfileApp>();
}