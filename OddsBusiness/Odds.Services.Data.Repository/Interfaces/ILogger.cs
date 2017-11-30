using OddsBusiness.Core.Entity;
using System;
using System.Collections.Generic;
namespace OddsBusiness.Repository.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Check Login for Admin
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns>True or False</returns>
        void Log(string message, string innerexception);

    }
}
