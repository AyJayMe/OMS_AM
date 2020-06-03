using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Entity
    {
        protected string connectionString = ConfigurationManager.ConnectionStrings["OMSConnectionString"].ConnectionString;
    }
}
