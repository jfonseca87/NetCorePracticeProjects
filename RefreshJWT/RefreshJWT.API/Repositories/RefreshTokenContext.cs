using Microsoft.EntityFrameworkCore;
using RefreshJWT.API.Models;

namespace RefreshJWT.API.Repositories;

public class RefreshTokenContext : DbContext
{
    public RefreshTokenContext(DbContextOptions<RefreshTokenContext> options)
        : base(options)
    {}

    public DbSet<User> User { get; set; }
}