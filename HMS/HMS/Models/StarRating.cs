using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class StarRating
    {
        public virtual int Id_rate { get; set; }
        public virtual int Rate { get; set; }
        public virtual int Doctor_rate { get; set; }
        public virtual DateTime DateComment { get; set; }
        public virtual string Comment { get; set; }
    }
    public class StarRatingMap : ClassMap<StarRating>
    {
        public StarRatingMap()
        {
            Table("star_rating");
            Id(x => x.Id_rate);
            Map(x => x.Rate);
            Map(x => x.Doctor_rate);
            Map(x => x.DateComment);
            Map(x => x.Comment);


        }
    }
}