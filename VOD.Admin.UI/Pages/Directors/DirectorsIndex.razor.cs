namespace VOD.Admin.UI.Pages.Directors;

public partial class DirectorsIndex
{
	[Parameter] public List<DirectorDTO> Model { get; set; } = new();
	[Parameter] public EventCallback<ClickModel> OnClick { get; set; }
}
