namespace VOD.Admin.UI.Pages.Genres;

public partial class GenresIndex
{
	[Parameter] public List<GenreDTO> Model { get; set; } = new();
	[Parameter] public EventCallback<ClickModel> OnClick { get; set; }
}
