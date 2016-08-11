using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace BaseCMS.Models.Domain {
    
    public class UserInfo {
        public UserInfo() {
        }
        public virtual long ID { get; set; }
        [NotNullNotEmpty]
        public virtual string Username { get; set; }
        [NotNullNotEmpty]
        public virtual string Password { get; set; }
        [NotNullNotEmpty]
        public virtual string Email { get; set; }
        [NotNullNotEmpty]
        public virtual bool Active { get; set; }
        public virtual string Role { get; set; }
        public static string GetMd5Hash(string oldPassword)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(oldPassword));
            var sBuilder = new StringBuilder();
            foreach (byte t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
