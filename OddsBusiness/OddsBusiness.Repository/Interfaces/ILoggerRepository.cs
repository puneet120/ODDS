using OddsBusiness.Core.Entity;
using System;
using System.Collections.Generic;
namespace OddsBusiness.Repository.Interfaces
{
    public interface ILoggerRepository
    {
        /// <summary>
        /// Check Login for Admin
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns>True or False</returns>
        void LogFileWrite(string message, string innerexception);

    }
}
