using OFF.Domain.Common.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Interfaces.Infrastructure;

public interface IAccountSrv
{
    UserDTO Register(RegisterDTO registerDTO);
    UserAuthorizeDTO GetById(int? id);
    UserDTO Login(LoginDTO loginDTO);
}
