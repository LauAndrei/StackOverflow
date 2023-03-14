namespace API.Dtos;

public class LoggedInUserDto
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public int Score { get; set; }
    public string Token { get; set; }
}