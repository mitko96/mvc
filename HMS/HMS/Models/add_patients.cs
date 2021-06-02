using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class add_patients
    {
        public virtual int id_add { get; set; }
        public virtual int id_patient { get; set; }
        public virtual int id_doctor { get; set; }
        
    }

    public class AddMap : ClassMap<add_patients>
    {
        public AddMap()
        {
            Table("add_patients");
            Id(x => x.id_add);
            Map(x => x.id_patient);
            Map(x => x.id_doctor);

        }
    }
}