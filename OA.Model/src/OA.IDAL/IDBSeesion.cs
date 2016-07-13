using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.IDAL
{
    public interface IDBSeesion
    {
        IActionInfoDal ActionInfoDal { get; set; }

        IDepartmentDal DepartmentDal { get; set; }

        IRUserInfoActionInfoDal RUserInfoActionInfoDal { get; set; }

        IRoleInfoDal RoleInfoDal { get; set; }

        IUserInfoDal UserInfoDal { get; set; }

        bool SaveChanges();
    }
}
