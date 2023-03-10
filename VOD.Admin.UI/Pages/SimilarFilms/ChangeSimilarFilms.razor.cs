namespace VOD.Admin.UI.Pages.SimilarFilms
{
	public partial class ChangeSimilarFilms
	{
		[Parameter] public FilmDTO CurrentFilm { get; set; } = new();
		[Parameter] public List<FilmDTO> SimilarFilms { get; set; } = new();
		[Parameter] public string Page { get; set; } = string.Empty;
		[Parameter] public EventCallback<string> OnChange { get; set; }
		public List<FilmDTO> AllFilms { get; set; } = new();
		public List<SimilarFilmDTO> AllSimilarFilms { get; set; } = new();
		public int[] SimilarFilmsIds { get; set; } = new int[] { };

		private async Task OnFormSubmit()
		{
			try
			{
				List<SimilarFilmDTO> similarFilmsToAdd = new();
				List<SimilarFilmDTO> similarFilmsToRemove = new();
				foreach (var id in SimilarFilmsIds)
				{
					if (!SimilarFilms.Select(s => s.Id).ToList().Contains(id))
					{
						similarFilmsToAdd.Add(new SimilarFilmDTO
						{
							ParentFilmId = CurrentFilm.Id,
							SimilarFilmId = id
						});
					}
				}
				foreach (var simFilm in SimilarFilms)
				{
					if (!SimilarFilmsIds.Contains(simFilm.Id))
					{
						var tempSimilarFilm = new SimilarFilmDTO
						{
							ParentFilmId = CurrentFilm.Id,
							SimilarFilmId = simFilm.Id
						};
						var reversedSimilarFilm = new SimilarFilmDTO
						{
							ParentFilmId = simFilm.Id,
							SimilarFilmId = CurrentFilm.Id
						};
						if (AllSimilarFilms.Where(s => s.ParentFilmId.Equals(reversedSimilarFilm.ParentFilmId)
														&& s.SimilarFilmId.Equals(reversedSimilarFilm.SimilarFilmId)).Any())
						{
							similarFilmsToRemove.Add(reversedSimilarFilm);
						}
						else similarFilmsToRemove.Add(tempSimilarFilm);

					}
				}
				if (Page.Equals(PageType.Edit))
				{
					foreach (var dto in similarFilmsToRemove)
					{
						await AdminService.DeleteRefAsync<SimilarFilmDTO>("SimilarFilms", dto);
					}

					foreach (var dto in similarFilmsToAdd)
					{
						await AdminService.CreateRefAsync<SimilarFilmDTO>("SimilarFilms", dto);
					}
					await OnChange.InvokeAsync("");
				}
				else if (Page.Equals(PageType.Delete))

					await OnChange.InvokeAsync("");
			}
			catch
			{
				await OnChange.InvokeAsync("Something went wrong.");
			}
		}

		protected override async Task OnInitializedAsync()
		{
			AllFilms = await AdminService.GetAsync<FilmDTO>("Films");
			SimilarFilmsIds = SimilarFilms.Select(s => s.Id).ToArray();
			AllSimilarFilms = await AdminService.GetAsync<SimilarFilmDTO>("SimilarFilms");
		}
	}
}
