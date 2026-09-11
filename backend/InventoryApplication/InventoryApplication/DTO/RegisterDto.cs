namespace InventoryApplication.DTO
{
    public class RegisterDto
    {

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;
    }
}
