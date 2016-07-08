using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.IDAL
{
    public interface IDBSeesion
    {
        IUserInfoDal UserInforDal { get; set; }

        bool SaveChanges();
    }
}
