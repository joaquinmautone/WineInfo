﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WineInfo.Services.Security
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool PasswordMatches(string providedPassword, string passwordHash);
    }
}
