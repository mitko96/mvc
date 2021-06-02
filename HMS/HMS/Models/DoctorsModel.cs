using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using System.ComponentModel.DataAnnotations;

namespace HMS.Models
{
    public class DoctorsModel
    {
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual string ApplicationUserId { get; set; }
        public virtual int Id_d { get; set; }
        [Display (Name ="Име: ")]
        public virtual string D_first_name { get; set; }
        [Display(Name = "Презиме: ")]
        public virtual string D_mid_name { get; set; }
        [Display(Name = "Фамилия: ")]
        public virtual string D_family_name { get; set; }
        [Display(Name = "Специалност: ")]
        public virtual string D_specialty { get; set; }
        [Display(Name = "Пол: ")]
        public virtual string D_gender { get; set; }
        [Display(Name = "Тел.номер: ")]
        public virtual string D_phone { get; set; }
        [Display(Name = "Email: ")]
        public virtual string D_email { get; set; }
        [Display(Name = "ЕГН: ")]
        public virtual string D_egn { get; set; }
        [Display(Name = "Град: ")]
        public virtual string D_city { get; set; }
        [Display(Name = "Снимка: ")]
        public virtual byte[] D_picture { get; set; }
        [StringLength(5)]
        public virtual string D_rating { get; set; }
        public virtual string D_content { get; set; }
        [Display(Name = "Стая:" )]
        public virtual string D_room { get; set; }
        [Display(Name = "Етаж: ")]
        public virtual string D_floor { get; set; }
        [Display(Name = "Болница:")]
        public virtual string D_spec { get; set; }
        
        
    }
   
    public class DoctorsMap : ClassMap<DoctorsModel>
    {
        public DoctorsMap()
            {
            Table("doctors");
            Id(x => x.Id_d);
            Map(x => x.D_first_name);
            Map(x => x.D_mid_name);
            Map(x => x.D_family_name);
            Map(x => x.D_specialty);
            Map(x => x.D_gender);
            Map(x => x.D_phone);
            Map(x => x.D_email);
            Map(x => x.D_egn);
            Map(x => x.D_city);
            Map(x => x.D_picture);
            Map(x => x.D_rating);
            Map(x => x.D_content);
            Map(x => x.D_room);
            Map(x => x.D_floor);
            Map(x => x.D_spec);

        }
    }

}