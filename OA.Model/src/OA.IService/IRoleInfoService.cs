using OA.Model;
using System.Collections.Generic;

namespace OA.IService
{
    public interface IRoleInfoService : IBaseService<RoleInfo>
    {
        bool DeleteEntities(List<int> list);
    }
}
