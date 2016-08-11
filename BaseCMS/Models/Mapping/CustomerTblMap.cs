using System; 
using System.Collections.Generic; 
using System.Text;
using BaseCMS.Models.Domain;
using FluentNHibernate.Mapping;

namespace BaseCMS.Models.Mapping {
    
    
    public class CustomerTblMap : ClassMap<UserInfo> {
        
        public CustomerTblMap() {
			Table("Customer_Tbl");
			LazyLoad();
			Id(x => x.ID).GeneratedBy.Identity().Column("ID");
			Map(x => x.Username).Column("Username").Not.Nullable();
			Map(x => x.Password).Column("Password").Not.Nullable();
			Map(x => x.Email).Column("Email").Not.Nullable();
			Map(x => x.Active).Column("Active").Not.Nullable();
			Map(x => x.Role).Column("Role");
        }
    }
}
