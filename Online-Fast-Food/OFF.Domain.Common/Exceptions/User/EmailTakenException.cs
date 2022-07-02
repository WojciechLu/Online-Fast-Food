using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Exceptions.User;

public class EmailTakenException: Exception
{
    public EmailTakenException() : base("Email is already in use") { }
}
