namespace VOD.Admin.UI.Pages.Films;

public partial class ChangeFilms
{
	[Parameter] public FilmDTO Model { get; set; } = new();
	[Parameter] public string Page { get; set; } = "";
	[Parameter] public EventCallback<string> OnChange { get; set; }
	public List<DirectorDTO> Directors { get; set; } = new();
	public List<GenreDTO> AllGenres { get; set; } = new();
	public int[] GenreIds { get; set; } = new int[] { };


	private async Task OnFormSubmit()
	{
		try
		{
			var selectedGenres = AllGenres.Where(g => GenreIds.Contains(g.Id)).ToList();
			if (Page.Equals(PageType.Create))
			{
				var response = await AdminService.CreateAsync<CreateFilmDTO>("Films", new CreateFilmDTO
				{
					Title = Model.Title,
					FilmUrl = Model.FilmUrl,
					Description = Model.Description,
					Released = Model.Released,
					DirectorId = Model.DirectorId
				});
				var tempDTO = await response.Content.ReadFromJsonAsync<EditFilmDTO>();
				response.Dispose();
				tempDTO.Genres = selectedGenres;
				await AdminService.EditAsync($"Films/{tempDTO.Id}", tempDTO);
			}
			else if (Page.Equals(PageType.Edit))
			{
				var tempDTO = new EditFilmDTO
				{
					Id = Model.Id,
					Title = Model.Title,
					FilmUrl = Model.FilmUrl,
					Description = Model.Description,
					Released = Model.Released,
					DirectorId = Model.DirectorId,
					Genres = selectedGenres
				};
				await AdminService.EditAsync<EditFilmDTO>($"Films/{Model.Id}", tempDTO);
			}
			else if (Page.Equals(PageType.Delete))
				await AdminService.DeleteAsync<FilmDTO>($"Films/{Model.Id}");

			await OnChange.InvokeAsync("");
			UpdateSelectedGenres();
		}
		catch
		{
			await OnChange.InvokeAsync("Something went wrong.");
		}
	}

	protected override async Task OnInitializedAsync()
	{
		Directors = await AdminService.GetAsync<DirectorDTO>("Directors");
		AllGenres = await AdminService.GetAsync<GenreDTO>("Genres");
		UpdateSelectedGenres();
	}

	protected void UpdateSelectedGenres()
	{
		GenreIds = Model.Genres.Select(m => m.Id).ToArray();
	}
}
