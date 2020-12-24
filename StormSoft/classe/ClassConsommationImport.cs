using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormSoft.classe
{
    class ClassConsommationImport
    {
        private string mat;
        private string prix;

        public string Prix
        {
            get { return prix; }
            set { prix = value; }
        }
        public string Mat
        {
            get { return mat; }
            set { mat = value; }
        }
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private string qte;

        public string Qte
        {
            get { return qte; }
            set { qte = value; }
        }
        private string user;

        public string User
        {
            get { return user; }
            set { user = value; }
        }
    }
}
