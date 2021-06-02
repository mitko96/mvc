using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class UsersModel
    {

        public virtual int Id_u { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Парола: ")]
        public virtual string U_password { get; set; }
        [Display(Name = "Ниво: ")]
        public virtual string U_user_level { get; set; }
        [Display(Name = "Потребителско име: ")]
        public virtual string U_username { get; set; }
        public virtual int Id_pat_dr { get; set; }
        public virtual bool RememberMe { get; set; }

    }

    public class UsersMap : ClassMap<UsersModel>
    {
        public UsersMap()
        {
            Table("users");
            Id(x => x.Id_u);
            Map(x => x.U_password);
            Map(x => x.U_user_level);
            Map(x => x.U_username);
            Map(x => x.Id_pat_dr);
            
        }
    }
}