using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OA.DAL;
using OA.IDAL;
using System.Data.Entity;

namespace OA.DalFactory
{
    /// <summary>
    /// Class Description: This class is used to as middle layout for dal and service.
    /// </summary>
    public partial class DbSession : IDbSession
    {
        private IUserInfoDal _UserInfoDal;

        public DbContext Db
        {
            get { return DBContextFactory.CreateDbContext(); }
        }

        public IUserInfoDal UserInfoDal
        {
            get
            {
                if (_UserInfoDal == null)
                {
                    _UserInfoDal = DALAbstractFactory.CreateUserInfoDal();
                }
                return _UserInfoDal;
            }

            set
            {
                _UserInfoDal = value;
            }
        }

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
