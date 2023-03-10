namespace VOD.Admin.UI.Pages.Films;

public partial class FilmsPortal
{
	public string Alert { get; set; } = string.Empty;
	public string Navigation { get; set; } = PageType.Index;
	public List<FilmDTO> Model { get; set; } = new();
	public FilmDTO Film { get; set; } = new();

	public void ChangePageType(string pageType)
	{
		Navigation = pageType;
	}

	public void CloseAlert()
	{
		Alert = "";
	}

	async Task OnClick(ClickModel model)
	{
		if (model.pageType.Equals(PageType.Edit) || model.pageType.Equals(PageType.Delete))
		{
			Film = await AdminService.SingleAsync<FilmDTO>($"Films/{model.id}") ?? new();
		}
		ChangePageType(model.pageType);
	}

	async Task OnChange(string alert)
	{
		try
		{
			Alert = alert;
			Model = await AdminService.GetAsync<FilmDTO>("Films");
			ChangePageType(PageType.Index);
		}
		catch
		{
			Alert = "Error";
		}
	}

	protected override async Task OnInitializedAsync()
	{
		Alert = "";
		Model = await AdminService.GetAsync<FilmDTO>("Films");
	}
}
