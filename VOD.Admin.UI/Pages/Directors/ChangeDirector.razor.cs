namespace VOD.Admin.UI.Pages.Directors;

public partial class ChangeDirector
{
	[Parameter] public DirectorDTO Model { get; set; } = new();
	[Parameter] public string Page { get; set; } = string.Empty;
	[Parameter] public EventCallback<string> OnChange { get; set; }

	async Task OnFormSubmit()
	{
		try
		{
			if (Page.Equals(PageType.Create))
			{
				await AdminService.CreateAsync<CreateDirectorDTO>("Directors", new CreateDirectorDTO
				{
					Name = Model.Name
				});
			}
			else if (Page.Equals(PageType.Edit))
			{
				await AdminService.EditAsync<DirectorDTO>($"Directors/{Model.Id}", Model);

			}
			else if (Page.Equals(PageType.Delete))
				await AdminService.DeleteAsync<DirectorDTO>($"Directors/{Model.Id}");

			await OnChange.InvokeAsync("");
		}
		catch
		{
			await OnChange.InvokeAsync("error");
		}
	}
}
