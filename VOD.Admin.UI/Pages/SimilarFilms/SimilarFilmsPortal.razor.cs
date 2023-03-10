namespace VOD.Admin.UI.Pages.SimilarFilms
{
	public partial class SimilarFilmsPortal
	{
		public string Alert { get; set; } = string.Empty;
		public string Navigation { get; set; } = PageType.Index;
		public List<FilmDTO> AllFilms { get; set; } = new();
		public List<FilmDTO> SimilarFilms { get; set; } = new();
		public FilmDTO CurrentFilm { get; set; } = new();

		public void ChangePageType(string pageType)
		{
			Navigation = pageType;
		}

		public void CloseAlert()
		{
			Alert = string.Empty;
		}

		async Task OnClick(ClickModel model)
		{
			if (model.pageType.Equals(PageType.Edit) || model.pageType.Equals(PageType.Delete))
			{
				CurrentFilm = await AdminService.SingleAsync<FilmDTO>($"Films/{model.id}") ?? new();
				SimilarFilms = await AdminService.GetAsync<FilmDTO>($"SimilarFilms/{model.id}") ?? new();
			}
			ChangePageType(model.pageType);
		}

		async Task OnChange(string alert)
		{
			try
			{
				Alert = alert;
				AllFilms = await AdminService.GetAsync<FilmDTO>("Films");
				ChangePageType(PageType.Index);
			}
			catch
			{
				Alert = "error";
			}
		}

		protected override async Task OnInitializedAsync()
		{
			Alert = string.Empty;
			AllFilms = await AdminService.GetAsync<FilmDTO>("Films");
		}
	}
}
