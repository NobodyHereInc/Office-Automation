using OA.Model;
using System.Collections.Generic;

namespace OA.IService
{
    public partial interface IActionInfoService : IBaseService<ActionInfo>
    {
        bool DeleteEntities(List<int> list);
        bool SetActionRole(int actionId, List<int> roleIds);
    }
}
