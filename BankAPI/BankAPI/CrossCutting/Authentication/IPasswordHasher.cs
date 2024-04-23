﻿namespace BankAPI.CrossCutting.Authentication
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string passwordHash, string password);
    }
}
