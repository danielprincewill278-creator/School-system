namespace student_system.Dto
{
    public class TokenResponseDto
    {
        public UserDto UserDetails { get; set; }
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string Role { get; set; }
    }
}
