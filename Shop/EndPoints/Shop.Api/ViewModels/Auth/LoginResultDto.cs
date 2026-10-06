namespace Shop.Api.ViewModels.Auth
{
	public class LoginResultDto
	{
		public string Token { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
	}
}