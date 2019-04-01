using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormSoft.classe
{
    class ClassBonus
    {
        public static string table = "tbonus";

        private string ref_cl, reseaux, reseaux0;
        private double prix;
        private string tel;
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }
        private string tel0;

        public string Tel0
        {
            get { return tel0; }
            set { tel0 = value; }
        }

        public double Prix
        {
            get { return prix; }
            set { prix = value; }
        }
        public string Reseaux
        {
            get { return reseaux; }
            set { reseaux = value; }
        }

        public string Reseaux0
        {
            get { return reseaux0; }
            set { reseaux0 = value; }
        }

        public string Ref_cl
        {
            get { return ref_cl; }
            set { ref_cl = value; }
        }
        private double qte;

        public double Qte
        {
            get { return qte; }
            set { qte = value; }
        }
        private double pourcent, pourcent0;

        public double Pourcent
        {
            get { return pourcent; }
            set { pourcent = value; }
        }

        public double Pourcent0
        {
            get { return pourcent0; }
            set { pourcent0 = value; }
        }
        private double montantdo, montantfc;

        public double Montantdo
        {
            get { return montantdo; }
            set { montantdo = value; }
        }

        public double Montantfc
        {
            get { return montantfc; }
            set { montantfc = value; }
        }


    }
}
