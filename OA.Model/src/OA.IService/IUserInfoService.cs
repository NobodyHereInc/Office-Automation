using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OA.Model;

namespace OA.IService
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {
        bool DeleteEntities(List<int> list);
    }
}
