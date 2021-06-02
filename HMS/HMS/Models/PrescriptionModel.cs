using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class PrescriptionModel
    {
        public virtual int Id_prescription { get; set; }

        [Display(Name = "Лекарства: ")]
        public virtual string Medicine { get; set; }
        [Display(Name = "Файл: ")]
        public virtual byte[] File { get; set; }
        [Display(Name = "Дата: ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime Date_pre { get; set; }
        [Display(Name = "Продължителност: ")]
        public virtual string Duration { get; set; }
        public virtual int? Id_patient { get; set; }
        public virtual int? Id_doctors { get; set; }
        [Display(Name = "Медицински тест: ")]
        public virtual string medicial_test { get; set; }
        [Display(Name = "Резултат от теста: ")]
        public virtual string result { get; set; }
        public virtual string file_name { get; set; }

    }

    public class PrescriptionsMap : ClassMap<PrescriptionModel>
    {
        public PrescriptionsMap()
        {
            Table("prescription");
            Id(x => x.Id_prescription);
            Map(x => x.Medicine);
            Map(x => x.File);
            Map(x => x.Date_pre);
            Map(x => x.Duration);
            Map(x => x.Id_patient);
            Map(x => x.Id_doctors);
            Map(x => x.medicial_test);
            Map(x => x.result);
            Map(x => x.file_name);

        }
    }
}