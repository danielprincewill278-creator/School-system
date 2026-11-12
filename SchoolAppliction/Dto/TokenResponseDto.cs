namespace SchoolAppliction.Dto
{
    public class TokenResponseDto
    {
        public LoginUser UserDetails { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string Role { get; set; }
    }
}
