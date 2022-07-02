using Microsoft.AspNetCore.Identity;
using OFF.Domain.Common.Exceptions.User;
using OFF.Domain.Common.Models.User;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework.Entities;

namespace OFF.Infrastructure.EntityFramework.Services;

public class AccountSrv : IAccountSrv
{
    private readonly OFFDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;
    public AccountSrv(OFFDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        _dbContext=dbContext;
        _passwordHasher=passwordHasher;
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

        //var userAuth = _userMapper.Map(newUser);
        //var token = _jwtUtils.GenerateJWT(userAuth);

        var newUserDTO = new UserDTO
        {
            Id = newUser.Id,
            FirstName=newUser.FirstName,
            LastName=newUser.LastName,
            Email = newUser.Email,
            //Token = token
        };
        return newUserDTO;
    }
}
