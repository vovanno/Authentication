﻿using AuthenticationDAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AuthenticationDAL.Context
{
    /// <summary>
    /// Context for interaction with authenticate database.
    /// </summary>
    public class IdentityContext : IdentityDbContext<IdentityUser>, IIdentiyContext
    {
        private static string _connectionString = "DefaultConnection";


        public IdentityContext(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }

        public static IdentityContext Create()
        {
            return new IdentityContext(_connectionString);
        }
    }
}
