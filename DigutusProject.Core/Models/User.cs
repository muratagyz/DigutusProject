using DigutusProject.Core.Common;
using DigutusProject.Core.Enums;

namespace DigutusProject.Core.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public Role Role { get; set; } = Role.User;
}