namespace VOD.Admin.UI.Pages.Directors;

public partial class DirectorsPortal
{
	public List<DirectorDTO> Model { get; set; } = new();
	public DirectorDTO Director { get; set; } = new();
	public string Alert { get; set; } = string.Empty;
	public string Navigation { get; set; } = PageType.Index;

	protected override async Task OnInitializedAsync()
	{
		Alert = string.Empty;
		Model = await AdminService.GetAsync<DirectorDTO>("Directors");
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
			Director = await AdminService.SingleAsync<DirectorDTO>($"Directors/{model.id}") ?? new();
		}
		ChangePageType(model.pageType);
	}

	async Task OnChange(string alert)
	{
		try
		{
			Alert = alert;
			Model = await AdminService.GetAsync<DirectorDTO>("Directors");
			ChangePageType(PageType.Index);
		}
		catch
		{
			Alert = "error";
		}
	}
}
