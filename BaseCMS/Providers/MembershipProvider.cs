using System;
using System.Web.Security;
using BaseCMS.Models.Domain;
using BaseCMS.Models.Repositories;

namespace BaseCMS.Providers
{
    public class BaseCMSMembershipProvider : MembershipProvider
    {
        private readonly IUserInfoRepo _userInfoRepo;

        public BaseCMSMembershipProvider()
        {
        }

        public BaseCMSMembershipProvider(IUserInfoRepo userInfoRepo)
        {
            _userInfoRepo = userInfoRepo;
        }
        public MembershipUser CreateUser(UserInfo u, out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(u.Username, UserInfo.GetMd5Hash(u.Password), true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(u.Email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            var user = GetUser(u.Username, true);

            if (user == null && args.IsNewUser)
            {
                u.Password = UserInfo.GetMd5Hash(u.Password);
                _userInfoRepo.Add(u);

                status = MembershipCreateStatus.Success;

                return GetUser(u.Username, true);
            }
            status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = _userInfoRepo.SingleOrDefault(username);

            if (user != null)
            {
                if (user.Password == UserInfo.GetMd5Hash(oldPassword))
                {
                    user.Password = UserInfo.GetMd5Hash(newPassword);
                    return _userInfoRepo.UpdateUser(user);
                }
            }
            return false;
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            var requiredUser = _userInfoRepo.SingleOrDefault(username, UserInfo.GetMd5Hash(password));


            return requiredUser != null;
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _userInfoRepo.SingleOrDefault(username);

            if (user == null) return null;
            var memUser = new BaseCMSMembershipUser(user);
            return memUser;
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}