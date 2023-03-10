namespace VOD.Admin.UI.Pages.Films;

public partial class FilmsIndex
{
	[Parameter] public List<FilmDTO> Model { get; set; } = new();
	[Parameter] public EventCallback<ClickModel> OnClick { get; set; }
	public List<GenreDTO> AllGenres { get; set; } = new();
}
