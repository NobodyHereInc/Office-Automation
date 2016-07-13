using OA.Model;
using OA.IService;
using System;

namespace OA.Service
{
    public class ActionInfoService : BaseService<ActionInfo>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }
}
