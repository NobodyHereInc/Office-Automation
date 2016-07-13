using OA.IDAL;
using OA.DAL;
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
        private IActionInfoDal _ActionInfoDal;
        private IDepartmentDal _DepartmentDal;
        private IRoleInfoDal _RoleInfoDal;
        private IRUserInfoActionInfoDal _RUserInfoActionInfoDal;

        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if (_ActionInfoDal == null)
                {
                    _ActionInfoDal = new ActionInfoDal();
                    // _UserInfoDal = DALAbstractFactory.GetUserInfoDal(); // Abstract
                }
                return _ActionInfoDal;
            }

            set
            {
                _ActionInfoDal = value;
            }
        }

        public IDepartmentDal DepartmentDal
        {
            get
            {
                if (_DepartmentDal == null)
                {
                    _DepartmentDal = new DepartmentDal();
                    // _UserInfoDal = DALAbstractFactory.GetUserInfoDal(); // Abstract
                }
                return _DepartmentDal;
            }

            set
            {
                _DepartmentDal = value;
            }
        }

        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if (_RoleInfoDal == null)
                {
                    _RoleInfoDal = new RoleInfoDal();
                    // _UserInfoDal = DALAbstractFactory.GetUserInfoDal(); // Abstract
                }
                return _RoleInfoDal;
            }

            set
            {
                _RoleInfoDal = value;
            }
        }

        public IRUserInfoActionInfoDal RUserInfoActionInfoDal
        {
            get
            {
                if (_RUserInfoActionInfoDal == null)
                {
                    _RUserInfoActionInfoDal = new RUserInfoActionInfoDal();
                    // _UserInfoDal = DALAbstractFactory.GetUserInfoDal(); // Abstract
                }
                return _RUserInfoActionInfoDal;
            }

            set
            {
                _RUserInfoActionInfoDal = value;
            }
        }

        public IUserInfoDal UserInfoDal
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
