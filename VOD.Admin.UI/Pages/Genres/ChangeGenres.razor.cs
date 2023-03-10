namespace VOD.Admin.UI.Pages.Genres;

public partial class ChangeGenres
{
	[Parameter] public GenreDTO Model { get; set; } = new();
	[Parameter] public string Page { get; set; } = string.Empty;
	[Parameter] public EventCallback<string> OnChange { get; set; }

	async Task OnFormSubmit()
	{
		try
		{
			if (Page.Equals(PageType.Create))
			{
				await AdminService.CreateAsync<CreateGenreDTO>("Genres", new CreateGenreDTO
				{
					Name = Model.Name
				});
			}
			else if (Page.Equals(PageType.Edit))
			{
				await AdminService.EditAsync<GenreDTO>($"Genres/{Model.Id}", Model);

			}
			else if (Page.Equals(PageType.Delete))
				await AdminService.DeleteAsync<GenreDTO>($"Genres/{Model.Id}");

			await OnChange.InvokeAsync("");
		}
		catch
		{
			await OnChange.InvokeAsync("Something went wrong.");
		}
	}
}
