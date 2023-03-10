namespace VOD.Admin.UI.Pages.SimilarFilms
{
	public partial class SimilarFilmsIndex
	{
		[Parameter] public List<FilmDTO> Model { get; set; } = new();
		[Parameter] public EventCallback<ClickModel> OnClick { get; set; }
	}
}
