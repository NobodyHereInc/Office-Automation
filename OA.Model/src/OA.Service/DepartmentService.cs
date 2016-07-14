using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class DepartmentService : BaseService<Department>, IDepartmentService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }
}
