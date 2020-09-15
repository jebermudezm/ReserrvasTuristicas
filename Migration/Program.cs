using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;


namespace Migration
{
    class Program
    {
        static string oracleConexion;
        static string sqlConexion;
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();
            oracleConexion = configuration.GetConnectionString("ConexionOracle");
            sqlConexion = configuration.GetConnectionString("ConexionSLQ");

            
            //System.Console.WriteLin   e(conString);
            Console.Write("Presione una tecla para Inicia.");
            Console.ReadKey();
            Console.WriteLine("Inicio proceso.");
            var fechaInicio = DateTime.Now;
            Console.WriteLine($"Inicio ==> {fechaInicio}.");
            var file = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ListaTablasColumnasMigrar.xml");
            var xmlTables = XDocument.Load(file);
            var columnasTabla = xmlTables.Descendants("Item");
            MigrarInformacion(columnasTabla);
            var fechaFin = DateTime.Now;
            var duracion = fechaFin.Subtract(fechaInicio);
            Console.WriteLine($"Fin ==> {fechaFin}.");
            Console.WriteLine($"Duracion en minutos => {duracion.Minutes}.");
            Console.Write("Presione una tecla para finalizar.");
            Console.ReadKey();
        }

        private static void MigrarInformacion(System.Collections.Generic.IEnumerable<XElement> columnasTabla)
        {
            var tablas = new List<Tablas>();
            foreach (var item in columnasTabla)
            {
                tablas.Add(new Tablas
                {
                    TableName = item.Attribute("TableName").Value,
                    Orden = int.Parse(item.Attribute("Orden").Value)
                });
            }
            foreach (var item in tablas.OrderBy(x => x.Orden))
            {
                try
                {
                    Console.WriteLine($"Migrando tabla ==> {item.TableName}.");
                    var sqlOrl = $"SELECT COLUMN_NAME||';'||DATA_TYPE VALOR FROM ALL_TAB_COLUMNS WHERE OWNER = 'OPERADOR' AND TABLE_NAME = '{item.TableName}'  ORDER BY COLUMN_ID";
                    var listaColumnas = AccesoDatosOracle.ObtenerLista(sqlOrl, oracleConexion);
                    List<MetaData> metaDataTable = new List<MetaData>();
                    foreach (var col in listaColumnas)
                    {
                        var arrRegistro = col.Split(';');
                        metaDataTable.Add(new MetaData
                        {
                            TableName = item.TableName,
                            ColumnName = arrRegistro[0],
                            DataTypeValor = arrRegistro[1]
                        });
                    }
                    HecerLaMagia(metaDataTable);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }

            }
        }

        private static void HecerLaMagia(List<MetaData> metaDataTable)
        {
            try
            {
                var xCantidad = 1;
                var tabla = metaDataTable.FirstOrDefault().TableName;
                var sentenciaOracle = $"SELECT ' SELECT {"{0}'"} VALOR FROM {tabla}";
                var campos = string.Empty;
                foreach (var item in metaDataTable)
                {
                    var columna = $"{ObtenerCampoTOSelect(item.ColumnName, item.DataTypeValor)}";
                    campos += columna;
                }
                campos = campos.Substring(0, campos.Length - 1);
                var sentenciaOracle2 = string.Format(sentenciaOracle, campos);
                var listaSentenciasSQL = AccesoDatosOracle.ObtenerLista(sentenciaOracle2, sqlConexion);
                var sentenciaSQL = string.Empty;
                var numeroRegistrosParaInsert = 0;
                foreach (var item in listaSentenciasSQL)
                {
                    try
                    {
                        numeroRegistrosParaInsert++;
                        if (sentenciaSQL == string.Empty)
                        {
                            sentenciaSQL += $"{item}";
                        }
                        else
                        {
                            sentenciaSQL += $"UNION ALL {item}";
                        }

                        if (numeroRegistrosParaInsert == xCantidad)
                        {
                            AccesoDatosSql.EjecutarSQL($"INSERT INTO {tabla} {sentenciaSQL}", sqlConexion);
                            numeroRegistrosParaInsert = 0;
                            sentenciaSQL = string.Empty;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error ejecutando la sentencia, {sentenciaSQL}: {e.Message}");
                    }
                }
                if (numeroRegistrosParaInsert != 0)
                {
                    AccesoDatosSql.EjecutarSQL($"INSERT INTO {tabla} {sentenciaSQL}", sqlConexion);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }

        private static string ObtenerCampoTOSelect(string columnName, string dataTypeValor)
        {
            var value = "";
            if (dataTypeValor.Equals("TIMESTAMP(0)") || dataTypeValor.Equals("DATE"))
            {
                value = $"CONVERT(DATETIME,'''||TO_CHAR({columnName}, 'YYYY-MM-DD HH24:MI:SS')||''',120),";
            }
            else if (dataTypeValor.Equals("FLOAT") || dataTypeValor.Equals("NUMBER"))
            {
                value = $"'||REPLACE(NVL({columnName},0),',','.')||',";
            }
            else if (dataTypeValor.Equals("VARCHAR2"))
            {
                value = $"'''||{columnName}||''',";
            }
            return value;
        }
    }
}
