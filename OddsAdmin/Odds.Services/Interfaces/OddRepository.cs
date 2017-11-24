using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Odds.Core.Entity;
using Odds.Data.Context;

namespace Odds.Services.Interfaces
{
    public class OddRepository : IOddRepository
    {
        private readonly IRepository<Odd> _odds;
        public OddRepository(IRepository<Odd> odds)            
        {
            _odds = odds;
        }

        /// <summary>
        /// Get Odds in Data Table
        /// </summary>
        /// <param name="search"></param>
        /// <param name="descsearch"></param>
        /// <param name="Odd_1search"></param>
        /// <param name="Odd_Xsearch"></param>
        /// <param name="Odd_2search"></param>
        /// <returns>Odds List </returns>
        public IQueryable<Odd> GetOdds(string search, string descsearch, string odd_1search, string odd_xsearch,string odd_2search)

        {
            var qry = _odds.Table;

            if (qry != null)
            {
                if (!string.IsNullOrWhiteSpace(search))

                {
                    qry = qry.Where(m => m.Description.Contains(search) || m.Odd_1.ToString().Contains(search)|| m.Odd_X.ToString().Contains(search)|| m.Odd_2.ToString().Contains(search));
                }

                if (!string.IsNullOrWhiteSpace(descsearch) && descsearch != "NA")

                {
                    qry = qry.Where(m => m.Description.Contains(descsearch));
                }
                if (!string.IsNullOrWhiteSpace(odd_1search) && odd_1search != "NA")

                {
                    qry = qry.Where(m => m.Odd_1.ToString().Contains(odd_1search));
                }
                if (!string.IsNullOrWhiteSpace(odd_xsearch) && odd_xsearch != "NA")

                {
                    qry = qry.Where(m => m.Odd_X.ToString().Contains(odd_xsearch));
                }
                if (!string.IsNullOrWhiteSpace(odd_2search) && odd_2search != "NA")

                {
                    qry = qry.Where(m => m.Odd_2.ToString().Contains(odd_2search));
                }
            }

            qry = qry.OrderByDescending(m => m.Id);
            return qry;
        }

        /// <summary>
        /// Save & Update Odds Data
        /// </summary>
        /// <param name="odd"></param>
        /// <returns>Integer for Successfull Operation</returns>
        public int SaveUpdateOdd(Odd odd)
        {
            if (odd.Id > 0)
            {
                var qry = _odds.Table.Where(m => m.Id == odd.Id).FirstOrDefault();

                if (qry !=null)
                {
                    qry.Description = odd.Description;
                    qry.Odd_1 = odd.Odd_1;
                    qry.Odd_X = odd.Odd_X;
                    qry.Odd_2 = odd.Odd_2;
                    _odds.Update(qry);
                    return 2;
                }
            }
            else
            {
                Odd odds = new Odd();
                odds.Description = odd.Description;
                odd.Odd_1 = odd.Odd_1;
                odd.Odd_X = odd.Odd_X;
                odd.Odd_2 = odd.Odd_2;
                _odds.Insert(odd);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// Get Odd by Odd ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Odds Object containing Odds Data</returns>
        public Odd GetbyId(int id)
        {
            return _odds.Table.Where(m => m.Id == id).FirstOrDefault();
        }


        /// <summary>
        /// Delete Odd by Odd ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Integer for Successfull Operation</returns>
        public int DeleteOdd(int id)
        {
            var qry = _odds.Table.Where(m => m.Id == id).FirstOrDefault();
            if (qry != null)
            {
                _odds.Delete(qry);
                return 1;
            }
            return 0;
        }

    }
}
