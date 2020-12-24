using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormSoft.classe
{
    class ClassMessage
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; } 
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private string numUtilisateur;

        public string NumUtilisateur
        {
            get { return numUtilisateur; }
            set { numUtilisateur = value; }
        }
    }
}
