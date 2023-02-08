using DigutusProject.Core.DTOs;
using DigutusProject.Core.Repositories;
using DigutusProject.Core.Services;
using DigutusProject.Core.UnitOfWorks;
using DigutusProject.Repository.DbContext;
using DigutusProject.Repository.Repositories;
using DigutusProject.Repository.UnitOfWork;
using DigutusProject.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DigutusProject.UnitTest;

public class AuthServiceUnitTest
{
    private IAuthService _authService;
    ServiceCollection services = new ServiceCollection();

    public AuthServiceUnitTest()
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient(typeof(IGenericService<>), typeof(GenericService<>));

        services.AddDbContext<DigutusProjectDbContext>(x =>
        {
            x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DigutusProjectDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        });

        var serviceProvider = services.BuildServiceProvider();
        _authService = serviceProvider.GetService<IAuthService>();
    }

    [Test]
    public async Task LoginAsync()
    {
        // Arrange
        var userLoginDto = new UserLoginDto()
        {
            Email = "test@test",
            Password = "password"
        };

        // Action
        var result = await _authService.LoginAsync(userLoginDto);

        // Assert
        Assert.AreEqual(true, result);
    }
}