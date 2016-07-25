using OA.Model;
using System.Collections.Generic;

namespace OA.IService
{
    public partial interface IRoleInfoService : IBaseService<RoleInfo>
    {
        bool DeleteEntities(List<int> list);
    }
}
