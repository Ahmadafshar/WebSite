using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BaseCMS.Models.Domain;
using BaseCMS.Models;
using Ninject;

namespace BaseCMS.Providers
{
    public interface IMembershipService
    {
        bool IsLoggedIn { get; }
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        MembershipCreateStatus CreateUser(UserInfo u);
    }
    public class AccountMembershipService : IMembershipService
    {
        public bool IsLoggedIn
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }
        public BaseCMSMembershipProvider Provider
        {
            get
            {
                return DependencyResolver.Current.GetService<BaseCMSMembershipProvider>();
            }
        }

        public int MinPasswordLength
        {
            get
            {
                return Provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Value cannot be null or empty.", "password");

            return Provider.ValidateUser(userName, password);
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                return Provider.ChangePassword(userName, oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        public MembershipCreateStatus CreateUser(UserInfo u)
        {
            if (String.IsNullOrEmpty(u.Username)) return MembershipCreateStatus.InvalidUserName;
            if (String.IsNullOrEmpty(u.Password)) return MembershipCreateStatus.InvalidPassword;
            if (String.IsNullOrEmpty(u.Role)) return MembershipCreateStatus.InvalidEmail;



            MembershipCreateStatus status;
            Provider.CreateUser(u, out status);
            return status;
        }
    }

}