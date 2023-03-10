namespace VOD.Admin.UI.Pages.FIlmGenres;

public partial class ChangeFilmGenres
{
	[Parameter] public FilmGenreDTO Model { get; set; } = new();
	[Parameter] public string Page { get; set; } = string.Empty;
	[Parameter] public EventCallback<string> OnChange { get; set; }

	private async Task OnFormSubmit()
	{
		try
		{
			if (Page.Equals(PageType.Create))
			{
				await AdminService.CreateRefAsync<FilmGenreDTO>("FilmGenres", new FilmGenreDTO
				{
					FilmId = Model.FilmId,
					GenreId = Model.GenreId
				});
			}
			else if (Page.Equals(PageType.Delete))
				await AdminService.DeleteRefAsync<FilmGenreDTO>($"FilmGenres", Model);

			await OnChange.InvokeAsync("");
		}
		catch
		{
			await OnChange.InvokeAsync("error");
		}
	}
}
