using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }
}
