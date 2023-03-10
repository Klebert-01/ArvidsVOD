namespace VOD.Admin.UI.Pages.FIlmGenres
{
	public partial class FilmGenresIndex
	{
		[Parameter] public List<FilmGenreDTO> Model { get; set; } = new();
		[Parameter] public EventCallback<RefClickModel<FilmGenreDTO>> OnClick { get; set; }
	}
}
