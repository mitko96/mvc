using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class CalendarModel
    {
        public virtual int EventID { get; set; }
        public virtual DateTime Start_date { get; set; }
        public virtual DateTime End_date { get; set; }
        public virtual string Description { get; set; }
        public virtual int? Fk_doc { get; set; }
        public virtual int? Fk_pat { get; set; }
        public virtual string Subject { get; set; }


    }

    public class CalendarMap : ClassMap<CalendarModel>
    {
        public CalendarMap()
        {
            Table("calendar");
            Id(x => x.EventID);
            Map(x => x.Start_date);
            Map(x => x.End_date);
            Map(x => x.Description);
            Map(x => x.Subject);
            Map(x => x.Fk_doc);
            Map(x => x.Fk_pat);

        }
    }
}