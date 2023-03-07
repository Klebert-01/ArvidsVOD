namespace VOD.Common.HttpClients;

public class VODHttpClient
{
    public readonly HttpClient Client;

	public VODHttpClient(HttpClient client)
	{
		Client = client;
	}
}
