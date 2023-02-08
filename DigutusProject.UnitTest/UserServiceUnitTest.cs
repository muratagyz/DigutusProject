using DigutusProject.Core.Enums;
using DigutusProject.Core.Models;
using DigutusProject.Core.Repositories;
using DigutusProject.Core.Services;
using DigutusProject.Core.UnitOfWorks;
using DigutusProject.Core.Utilities.Security.Hashing;
using DigutusProject.Repository.DbContext;
using DigutusProject.Repository.Repositories;
using DigutusProject.Repository.UnitOfWork;
using DigutusProject.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigutusProject.UnitTest;

public class UserServiceUnitTest
{
    private IUserService _userService;
    ServiceCollection services = new ServiceCollection();

    public UserServiceUnitTest()
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));

        services.AddDbContext<DigutusProjectDbContext>(x =>
        {
            x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DigutusProjectDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        });

        var serviceProvider = services.BuildServiceProvider();
        _userService = serviceProvider.GetService<IUserService>();
    }

    [Test]
    public async Task AddAsync()
    {
        // Arrange
        var password = "password";
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

        var user = new User()
        {
            Id = new Guid(),
            Role = Role.User,
            CreateDate = DateTime.Now,
            Email = "user@user",
            FirstName = "user",
            LastName = "user",
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        // Action
        var result = await _userService.AddAsync(user);

        // Assert
        Assert.IsNotNull(result);
    }
}