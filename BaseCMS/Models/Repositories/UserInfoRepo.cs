using System;
using System.Collections.Generic;
using System.Linq;
using BaseCMS.Models.Domain;
using NHibernate;
using NHibernate.Linq;

namespace BaseCMS.Models.Repositories
{
    public interface IUserInfoRepo
    {
        void Add(UserInfo u);
        UserInfo Get(long id);
        void Remove(UserInfo userInfo);
        IEnumerable<UserInfo> GetAll();
        UserInfo SingleOrDefault(string username);
        bool UpdateUser(UserInfo user);
        UserInfo SingleOrDefault(string username, string password);
    }

    public class UserInfoRepo : IUserInfoRepo
    {
        private readonly ISession _session;

        public UserInfoRepo(ISession session)
        {
            _session = session;
        }

        public void Add(UserInfo u)
        {
            _session.Save(u);
        }

        public UserInfo Get(long id)
        {
            var u = _session.Get<UserInfo>(id);
            return u;
        }

        public void Remove(UserInfo u)
        {
            _session.Delete(u);
            _session.Flush();
        }
        public IEnumerable<UserInfo> GetAll()
        {
            return _session.Query<UserInfo>();
        }

        public UserInfo SingleOrDefault(string username)
        {
            var userInfos = _session.QueryOver<UserInfo>().Where(info => info.Username == username).SingleOrDefault();
                return userInfos;
        }


        public bool UpdateUser(UserInfo user)
        {
            try
            {
                _session.Update(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserInfo SingleOrDefault(string username, string password)
        {
            var userInfos = _session.QueryOver<UserInfo>().Where(u => u.Username == username && u.Password == password).SingleOrDefault();

            return userInfos;
        }

    }

}