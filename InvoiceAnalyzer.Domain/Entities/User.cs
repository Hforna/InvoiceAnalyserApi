using Microsoft.AspNetCore.Identity;

namespace InvoiceAnalyzer.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public DateTime RefreshTokenExpires { get; set; }
    public string RefreshToken { get; set; }
}