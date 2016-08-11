using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using BaseCMS.Models.Repositories;

namespace BaseCMS.Providers
{
    public class UserRoleProvider : RoleProvider
    {

        private IUserInfoRepo UserInfoRepo
        {
            get
            {
                return DependencyResolver.Current.GetService<IUserInfoRepo>();
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var lastUser = UserInfoRepo.SingleOrDefault(username);

            //UserInfo lastUser = UserInfo.GetUser(username);
            return
                lastUser.Role.ToString(CultureInfo.InvariantCulture)
                        .ToLower()
                        .Split(new[] { ',', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}