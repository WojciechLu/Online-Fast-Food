﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Exceptions.User;

public class InvalidEmailException : Exception
{
    public InvalidEmailException() : base("Invalid email") { }
}
