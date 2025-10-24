using Microsoft.AspNetCore.Identity;

namespace InvoiceAnalyzer.Domain.Entities;

public class Role : IdentityRole<Guid>
{
    public Role() : base() { }

    public Role(string roleName) : base(roleName) { }
}