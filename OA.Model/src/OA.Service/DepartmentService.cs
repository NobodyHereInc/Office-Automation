using OA.Model;
using OA.IService;

namespace OA.Service
{
    public class DepartmentService : BaseService<Department>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }
}
