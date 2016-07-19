using OA.Model;
using OA.IService;
using System;
using System.Linq;

namespace OA.Service
{
    public class RUserInfoActionInfoService : BaseService<RUserInfoActionInfo>, IRUserInfoActionInfoService
    {
        #region Set User Action
        /// <summary>
        /// This function is used to set user action.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="actionId"></param>
        /// <param name="isPass"></param>
        /// <returns></returns>
        public bool SetUserAction(int userId, int actionId, bool isPass)
        {
            // get this record.
            var actionInfo = this.DbSession.RUserInfoActionInfoDal.GetList(r => r.UserInfoId == userId && r.ActionInfoId == actionId).FirstOrDefault();

            // get whether this record is exist.
            if (actionInfo == null)
            {
                // create new RUserInfoActionInfo.
                RUserInfoActionInfo ruai = new RUserInfoActionInfo()
                {
                    UserInfoId = userId,
                    ActionInfoId = actionId,
                    IsPass = isPass
                };

                // add new record into database.
                this.DbSession.RUserInfoActionInfoDal.Add(ruai);             
            }
            else // otherwise
            {
                if (actionInfo.IsPass != isPass)
                {
                    actionInfo.IsPass = isPass;
                }
            }

            // return .
            return this.DbSession.SaveChanges();
        }
        #endregion

        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RUserInfoActionInfoDal;
        }


    }
}
