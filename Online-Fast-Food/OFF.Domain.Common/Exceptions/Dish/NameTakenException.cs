using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Exceptions.Dish;

public class NameTakenException : Exception
{
    public NameTakenException() : base("Name is already in use. Change name, please") { }
}
