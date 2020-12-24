using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormSoft.classe
{
    class ClassClient
    {
        string mat, nom, postnom, prenom, adresse, etatcivil, tel, affiliation, reseaux, sexe;
        string idcarte;
        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public int Ib { get; set; }

        public string Idcarte
        {
            get { return idcarte; }
            set { idcarte = value; }
        }

        public string Sexe
        {
            get { return sexe; }
            set { sexe = value; }
        }

        public string Reseaux
        {
            get { return reseaux; }
            set { reseaux = value; }
        }

        public string Affiliation
        {
            get { return affiliation; }
            set { affiliation = value; }
        }

        public string Tel
        {
            get { return tel; }
            set { tel = value; }
        }

        public string Etatcivil
        {
            get { return etatcivil; }
            set { etatcivil = value; }
        }

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public string Postnom
        {
            get { return postnom; }
            set { postnom = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Mat
        {
            get { return mat; }
            set { mat = value; }
        }
        int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        byte[] photo;

        public byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
        }
        byte[] qr;
        string refcat;

        public string Refcat
        {
            get { return refcat; }
            set { refcat = value; }
        }

        public byte[] Qr
        {
            get { return qr; }
            set { qr = value; }
        }



    }
}
