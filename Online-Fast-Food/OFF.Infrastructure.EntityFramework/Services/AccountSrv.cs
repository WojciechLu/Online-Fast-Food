using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OFF.Domain.Common.Exceptions.User;
using OFF.Domain.Common.Models.User;
using OFF.Domain.Common.Utils;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework.Entities;
using System.Security.Claims;

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
        var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);
        var userAuth = new UserAuthorizeDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PasswordHash = user.PasswordHash,
            Role = user.Role.Name
        };
        return userAuth;
    }

    public UserDTO Login(LoginDTO loginDTO)
    {
        var user = _dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == loginDTO.Email);
        if (user == null) throw new InvalidEmailException();

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDTO.Password);
        if (result == PasswordVerificationResult.Failed) throw new WrongPasswordException();

        var userAuth = new UserAuthorizeDTO
        {
            Id=user.Id,
            Email=user.Email,
            FirstName=user.FirstName,
            LastName=user.LastName,
            PasswordHash=user.PasswordHash,
            Role = user.Role.Name
        };
        var token = _jwtUtils.GenerateJWT(userAuth);

        var userDTO = new UserDTO
        {
            Id = userAuth.Id,
            Email = userAuth.Email,
            FirstName=userAuth.FirstName,
            LastName = userAuth.LastName,
            Token=token
        };
        return userDTO;
    }

    public UserDTO Register(RegisterDTO registerDTO)
    {
        var emailInUse = _dbContext.Users.FirstOrDefault(u => u.Email == registerDTO.Email);
        if (emailInUse != null) throw new EmailTakenException();

        var role = _dbContext.Roles.FirstOrDefault(r => r.Id == registerDTO.RoleId);

        var newUser = new User
        {
            Email = registerDTO.Email,
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            RoleId = registerDTO.RoleId,
            Role = role
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
            PasswordHash = newUser.PasswordHash,
            Role = newUser.Role.Name
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
