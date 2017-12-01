using OddsBusiness.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OddsBusiness.Repository.Interfaces
{
    public interface IOddRepository
    {
        /// <summary>
        /// Get Odds in Data Table
        /// </summary>
        /// <param name="search"></param>
        /// <param name="descsearch"></param>
        /// <param name="Odd_1search"></param>
        /// <param name="Odd_Xsearch"></param>
        /// <param name="Odd_2search"></param>
        /// <returns>Odds List </returns>
        IQueryable<Odd> GetOdds(string search, string descsearch, string odd_1search, string odd_xsearch, string odd_2search);

        /// <summary>
        /// Save & Update Odds Data
        /// </summary>
        /// <param name="odd"></param>
        /// <returns>Integer for Successfull Operation</returns>
        int SaveUpdateOdd(Odd odd);

        /// <summary>
        /// Get Odd by Odd ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Odds Object containing Odds Data</returns>
        Odd GetbyId(int id);


        /// <summary>
        /// Delete Odd by Odd ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Integer for Successfull Operation</returns>
        int DeleteOdd(int id);
    }
}
