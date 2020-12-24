using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormSoft.classe
{
    class ClassPourcentage
    {
        private int niveau0, niveau1;
        public string Categorie { get; set; }
        public int Id { get; set; }

        public int Niveau0
        {
            get { return niveau0; }
            set { niveau0 = value; }
        }

        public int Niveau1
        {
            get { return niveau1; }
            set { niveau1 = value; }
        }
        private DateTime date_fin;

        public DateTime Date_fin
        {
            get { return date_fin; }
            set { date_fin = value; }
        }
    }
}
