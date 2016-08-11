using System;
using System.Web.Security;
using BaseCMS.Models.Domain;

namespace BaseCMS.Providers
{
    public class BaseCMSMembershipUser : MembershipUser
    {
        public UserInfo User;
        public BaseCMSMembershipUser(UserInfo u)
            : base("BaseCMSMembershipProvider", u.Username, u.ID, u.Email,
                string.Empty, string.Empty,
                true, !u.Active, DateTime.Now,
                DateTime.Now,
                DateTime.MinValue,
                DateTime.Now, DateTime.Now)
        {
            User = u;
        }
    }
}