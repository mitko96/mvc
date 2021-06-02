using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Models
{
    public class CC
    {
        public virtual PatientsModel PatientModel { get; set; }
        public virtual UsersModel UserModel { get; set; }

        public virtual DoctorsModel DocModel { get; set; }
        public virtual PrescriptionModel PresModel { get; set; }

        public virtual add_patients addModel { get; set; }
        public virtual ViewModel viewmodel { get; set; }
        public virtual RecipteModel reciptemodel { get; set; }


    }
}