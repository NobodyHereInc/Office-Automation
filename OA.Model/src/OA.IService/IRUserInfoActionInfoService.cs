using OA.Model;

namespace OA.IService
{
    public interface IRUserInfoActionInfoService : IBaseService<RUserInfoActionInfo>
    {
        /// <summary>
        /// This function is used to set action for user.
        /// </summary>
        /// <param name="userId"> user id.</param>
        /// <param name="actionId"> action id.</param>
        /// <param name="isPass"> bool </param>
        /// <returns></returns>
        bool SetUserAction(int userId, int actionId, bool isPass);
    }
}
