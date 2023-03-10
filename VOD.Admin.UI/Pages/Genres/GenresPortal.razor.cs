namespace VOD.Admin.UI.Pages.Genres
{
	public partial class GenresPortal
	{
		public List<GenreDTO> Model { get; set; } = new();
		public string Alert { get; set; } = string.Empty;
		public string Navigation { get; set; } = PageType.Index;
		public GenreDTO Genre { get; set; } = new();

		protected override async Task OnInitializedAsync()
		{
			Alert = string.Empty;
			Model = await AdminService.GetAsync<GenreDTO>("Genres");
		}

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
				Genre = await AdminService.SingleAsync<GenreDTO>($"Genres/{model.id}") ?? new();
			}
			ChangePageType(model.pageType);
		}

		async Task OnChange(string alert)
		{
			try
			{
				Alert = alert;
				Model = await AdminService.GetAsync<GenreDTO>("Genres");
				ChangePageType(PageType.Index);
			}
			catch
			{
				Alert = "error";
			}
		}
	}
}
