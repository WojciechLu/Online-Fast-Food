using Microsoft.AspNetCore.Identity;
using OFF.Domain.Common.Exceptions.User;
using OFF.Domain.Common.Models.User;
using OFF.Domain.Common.Utils;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework.Entities;

namespace OFF.Infrastructure.EntityFramework.Services;

public class AccountSrv : IAccountSrv
{
    private readonly OFFDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtUtils _jwtUtils;
    public AccountSrv(OFFDbContext dbContext, IPasswordHasher<User> passwordHasher, IJwtUtils jwtUtils)
    {
        _dbContext=dbContext;
        _passwordHasher=passwordHasher;
        _jwtUtils=jwtUtils;
    }

    public UserAuthorizeDTO GetById(int? id)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
        var userAuth = new UserAuthorizeDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash
        };
        return userAuth;
    }

    public UserDTO Register(RegisterDTO registerDTO)
    {
        var emailInUse = _dbContext.Users.FirstOrDefault(u => u.Email == registerDTO.Email);
        if (emailInUse != null) throw new EmailTakenException();

        var newUser = new User
        {
            Email = registerDTO.Email,
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            RoleId = registerDTO.RoleId
        };
        var hashedPassword = _passwordHasher.HashPassword(newUser, registerDTO.Password);
        newUser.PasswordHash = hashedPassword;

        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();

        var userAuth = new UserAuthorizeDTO
        {
            Id = newUser.Id,
            Email = newUser.Email,
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            PasswordHash = newUser.PasswordHash
        };
        var token = _jwtUtils.GenerateJWT(userAuth);

        var newUserDTO = new UserDTO
        {
            Id = newUser.Id,
            FirstName=newUser.FirstName,
            LastName=newUser.LastName,
            Email = newUser.Email,
            Token = token
        };
        return newUserDTO;
    }
}
