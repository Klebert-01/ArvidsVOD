namespace VOD.Admin.UI.Pages.FIlmGenres
{
	public partial class FilmGenresPortal
	{
		public string Alert { get; set; } = string.Empty;
		public string Navigation { get; set; } = PageType.Index;
		public List<FilmGenreDTO> Model { get; set; } = new();
		public FilmGenreDTO FilmGenre { get; set; } = new();

		public void ChangePageType(string pageType)
		{
			Navigation = pageType;
		}

		public void CloseAlert()
		{
			Alert = string.Empty;
		}

		async Task OnClick(RefClickModel<FilmGenreDTO> model)
		{
			if (model.pageType.Equals(PageType.Edit) || model.pageType.Equals(PageType.Delete))
			{
				FilmGenre = model.dto ?? new();
			}
			ChangePageType(model.pageType);
		}

		async Task OnChange(string alert)
		{
			try
			{
				Alert = alert;
				ChangePageType(PageType.Index);
				Model = await AdminService.GetAsync<FilmGenreDTO>("FilmGenres");

			}
			catch
			{
				Alert = "error";
			}
		}

		protected override async Task OnInitializedAsync()
		{
			Alert = "";
			Model = await AdminService.GetAsync<FilmGenreDTO>("FilmGenres");
		}
	}
}
