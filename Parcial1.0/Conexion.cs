using System;
using System.Data.SqlClient;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial1._0
{
    class Conexion
    {
        public static SqlConnection conexion = new SqlConnection("workstation id=Jonathan3249.mssql.somee.com;packet size=4096;user id=Jona2020_SQLLogin_1;pwd=t5fvbjqhdv;data source=Jonathan3249.mssql.somee.com;persist security info=False;initial catalog=Jonathan3249");

    }
}
