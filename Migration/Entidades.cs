using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migration
{
    public class Entidades
    {
    }

    public class MetaData
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string DataTypeValor { get; set; }
    }

    public class Tablas
    {
        public string TableName { get; set; }
        public int Orden { get; set; }
    }
}
