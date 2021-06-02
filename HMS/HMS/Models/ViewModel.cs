using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class ViewModel
    {
        public virtual int? Id_p { get; set; }
        [Display(Name = "Име: ")]
        public virtual string P_first_name { get; set; }
        [Display(Name = "Презиме: ")]
        public virtual string P_mid_name { get; set; }
        [Display(Name = "Фамиля: ")]
        public virtual string P_family_name { get; set; }
        [Display(Name = "ЕГН: ")]
        public virtual string P_egn { get; set; }
        [Display(Name = "Пол: ")]
        public virtual string P_gender { get; set; }
        [Display(Name = "Тел. номер: ")]
        public virtual string P_phone { get; set; }
        [Display(Name = "Адрес: ")]
        public virtual string P_address { get; set; }
        [Display(Name = "Дата: ")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public virtual DateTime P_datetime { get; set; }
        [Display(Name = "Кръвна група: ")]
        public virtual string P_blood_group { get; set; }
        public virtual int? Id_doc { get; set; }
        public virtual int? id_add { get; set; }
        public virtual int? id_patient { get; set; }
        public virtual int? id_doctor { get; set; }
        public virtual int? id_number { get; set; }
        virtual public int? Id { get; set; }
    }
    public class ViewMap : ClassMap<ViewModel>
    {
        public ViewMap()
        {
            Table("add_patients_view");
           // ReadOnly();
           
            Id(x => x.Id_p);
            Map(x => x.P_first_name);
            Map(x => x.P_mid_name); 
            Map(x => x.P_family_name); 
            Map(x => x.P_egn); 
            Map(x => x.P_gender); 
            Map(x => x.P_phone);
            Map(x => x.P_address);
            Map(x => x.P_datetime);
            Map(x => x.P_blood_group);
            Map(x => x.Id_doc);
            Map(x => x.id_add);
            Map(x => x.id_patient);
            Map(x => x.id_doctor);
            Map(x => x.id_number);
        }
    }
}