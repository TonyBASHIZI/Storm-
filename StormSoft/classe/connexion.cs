using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StormSoft.classe
{
    class connexion
    {
        public string chemin = " ";

        public void Connect()
        {
            //chemin = "datasource=91.216.107.164;port=3306;username=synap881639_3fbe;database=synap881639_3fbe;password=SynapseDb010101;SslMode=none; ";
            chemin = "datasource=localhost;port=3306;username=root;database=station_db;password=";
        }



    }
}
