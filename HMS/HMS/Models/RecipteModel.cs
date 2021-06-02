using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class RecipteModel
    {
        public virtual int Id_r { get; set; }

        [Display(Name = "Тип рецепта: ")]
        public virtual string Recipe { get; set; }
        [Display(Name = "Вид: ")]
        public virtual string Recipe_type_ { get; set; }
        [Display(Name = "Начална дата: ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime Start_date_recipe { get; set; }
        [Display(Name = "Крайна дата: ")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime End_date_recipe { get; set; }
        [Display(Name = "Под вид: ")]
        public virtual string Recipe_typeof { get; set; }
        [Display(Name = "Номер на протокол: ")]
        public virtual string Number_protocol { get; set; }

        
        public virtual int? Fk_pats { get; set; }

        [Display(Name = "Тема: ")]
        public virtual string Name_recipe { get; set; }
        [Display(Name = "Лекарства: ")]
        public virtual string Description_recipe { get; set; }
    }
        public class RecipteMap : ClassMap<RecipteModel>
        {
            public RecipteMap()
            {
                Table("recipe_type");
                Id(x => x.Id_r);
            Map(x => x.Recipe);
            Map(x => x.Recipe_type_);
                Map(x => x.Start_date_recipe);
                Map(x => x.End_date_recipe);
                Map(x => x.Recipe_typeof);
                Map(x => x.Number_protocol);
                Map(x => x.Fk_pats);
                Map(x => x.Name_recipe);
                Map(x => x.Description_recipe);
            }
        }
    }