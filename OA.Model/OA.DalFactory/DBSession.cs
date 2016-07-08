using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.IDAL;
using OA.DAL;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace OA.DalFactory
{
    /// <summary>
    /// Class Description : 1. Factory class (instance UserInforDal)
    /// </summary>
    public class DBSession : IDBSeesion
    {
        public DbContext context = ContextFactory.GetContext();
        private IUserInfoDal _UserInfoDal;

        public IUserInfoDal UserInforDal
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    _UserInfoDal = new UserInfoDal();
                    // _UserInfoDal = DALAbstractFactory.GetUserInfoDal(); // Abstract
                }
                return _UserInfoDal;
            }

            set
            {
                _UserInfoDal = value;
            }
        }

        /// <summary>
        /// This function is used to complete transaction(DBContext). (reduce connect times)
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
