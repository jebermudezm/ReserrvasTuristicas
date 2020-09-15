namespace RSI.Modelo.Modelo.Migrations
{
    using RSI.Modelo.Entidades.Maestros;
    using RSI.Modelo.Entidades.Seguridad;
    using RSI.Modelo.RepositorioImpl;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RSIModelContextDB>
    {
        private const string creadoPor = "JoaquinBM";
        private DateTime fechaCreacion = new DateTime(2019, 1, 1, 8, 0, 0);
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }


        protected override void Seed(RSIModelContextDB context)
        {
            //CreateRoles(context);
            //(context);
            //CreateCliente(context);
            //CreateTipoLista(context);
            //CreateLista(context);
            //CreateProveedor(context);
            //AuthorizeRSI.Modelo(context);
        }

        private void CreateTipoLista(RSIModelContextDB context)
        {
            context.TipoListas.AddOrUpdate(t => t.Codigo,
               new TipoLista[]
               {
                    new TipoLista { Id = 1, Codigo  = "IMPUESTOS", Descripcion = "Tabla 11 - Impuestos registrados en la Factura Electrónica", Observacion = "", CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new TipoLista { Id = 1, Codigo  = "TIPOPERSONA", Descripcion = "Tabla 20  Tipo de identificación - Tipos de Persona", Observacion = "", CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new TipoLista { Id = 1, Codigo  = "REGIMENFI", Descripcion = "Tabla 2 -  Regimen Fiscal", Observacion = "", CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new TipoLista { Id = 1, Codigo  = "TIPODOCID", Descripcion = "Tabla 3 - Tipos de documentos de identidad", Observacion = "", CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null}
               });

            context.SaveChanges();
        }

        private void CreateLista(RSIModelContextDB context)
        {
            var tipoListaImpId = context.TipoListas.FirstOrDefault(t => t.Codigo == "IMPUESTOS").Id;
            var tipoListaTpId = context.TipoListas.FirstOrDefault(t => t.Codigo == "TIPOPERSONA").Id;
            var tipoListaRFId = context.TipoListas.FirstOrDefault(t => t.Codigo == "REGIMENFI").Id;
            var tipoListaTdIId = context.TipoListas.FirstOrDefault(t => t.Codigo == "TIPODOCID").Id;
            context.Listas.AddOrUpdate(t => t.Codigo,
               new Lista[]
               {
                    new Lista { Id = 1, TipoListaId = tipoListaImpId, Codigo  = "01", Nombre = "IVA",  Descripcion = "Impuesto al Valor Agregado", Valor = 19, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 2, TipoListaId = tipoListaImpId, Codigo  = "02", Nombre = "IC", Descripcion = "Impuesto al Consumo", Valor = 8, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 3, TipoListaId = tipoListaTpId, Codigo  = "1", Nombre = "PN", Descripcion = "Persona Natural", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 4, TipoListaId = tipoListaTpId, Codigo  = "2", Nombre = "PJ", Descripcion = "Persona Jurídica", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 5, TipoListaId = tipoListaTdIId, Codigo  = "11", Nombre = "RC", Descripcion = "Registro civil", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 6, TipoListaId = tipoListaTdIId, Codigo  = "12", Nombre = "TI", Descripcion = "Tarjeta de identidad", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 7, TipoListaId = tipoListaTdIId, Codigo  = "13", Nombre = "CC", Descripcion = "Cédula de ciudadanía", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 8, TipoListaId = tipoListaTdIId, Codigo  = "21", Nombre = "TE", Descripcion = "Tarjeta de extranjería", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 9, TipoListaId = tipoListaTdIId, Codigo  = "22", Nombre = "CE", Descripcion = "Cédula de extranjería", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 10, TipoListaId = tipoListaTdIId, Codigo  = "31", Nombre = "NI", Descripcion = "NIT", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 11, TipoListaId = tipoListaTdIId, Codigo  = "41", Nombre = "PP", Descripcion = "Pasaporte", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 12, TipoListaId = tipoListaTdIId, Codigo  = "42", Nombre = "DE", Descripcion = "Documento de identificación extranjero", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 13, TipoListaId = tipoListaTdIId, Codigo  = "50", Nombre = "NE", Descripcion = "Nit de otro país", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 14, TipoListaId = tipoListaTdIId, Codigo  = "91", Nombre = "NP", Descripcion = "NIUP", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 15, TipoListaId = tipoListaRFId, Codigo  = "48", Nombre = "IV", Descripcion = "Impuestos sobre la venta del IVA", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Lista { Id = 16, TipoListaId = tipoListaRFId, Codigo  = "49", Nombre = "NR", Descripcion = "No responsables del IVA", Valor = 0, FechaVigencia = new DateTime(2019, 1, 1), CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},

               });

            context.SaveChanges();
        }

        private void CreateRoles(RSIModelContextDB context)
        {
            context.Roles.AddOrUpdate(t => t.Nombre,
               new Rol[]
               {
                    new Rol { Id = 1,  Nombre = "Administrador", CreadoPor = creadoPor, FechaCreacion = fechaCreacion }
               });

            context.SaveChanges();
        }

        

        private void CreateUsuarios(RSIModelContextDB context)
        {
            var rolId = context.Roles.FirstOrDefault(t => t.Nombre == "Administrador").Id;
            context.Usuarios.AddOrUpdate(t => t.Cedula,
               new Usuario[]
               {
                    new Usuario { Id = 1, Cedula  = "7924785", Nombre = "Joaquin Bermúdez", UserName = "JoaquinBM", Contrasena = "MTIzNDU2", Telefono = "3128823506", RolId = rolId, CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, UltimoIngreso = fechaCreacion, ModificadoPor = null, CambiarContrasena = "Si", Estado = "Activo" },
                    new Usuario { Id = 2, Cedula  = "8888888", Nombre = "Edward Vargas", UserName = "EduardVC", Contrasena = "MTIzNDU2", Telefono = "3012602865", RolId = rolId, CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, UltimoIngreso = fechaCreacion, ModificadoPor = null, CambiarContrasena = "Si", Estado = "Activo" }
               });

            context.SaveChanges();
        }

        private void CreateProveedor(RSIModelContextDB context)
        {
            var tipoListaId = context.TipoListas.FirstOrDefault(t => t.Codigo == "TIPODOCID").Id;
            var documentoId = context.Listas.FirstOrDefault(t => t.TipoListaId == tipoListaId && t.Codigo == "11").Id;
            var tipoListaTPId = context.TipoListas.FirstOrDefault(t => t.Codigo == "TIPOPERSONA").Id;
            var tipoPersonaId = context.Listas.FirstOrDefault(t => t.TipoListaId == tipoListaTPId && t.Codigo == "1").Id;
            context.Proveedores.AddOrUpdate(t => t.NumeroDocumentoIdentidad,
               new Proveedor[]
               {
                    new Proveedor { Id = 1, DocumentoIdentidadId  = documentoId, TipoPersonaId = tipoPersonaId, NumeroDocumentoIdentidad = "0", NombreORazonSocial = "NA", Contacto = "NA", Direccion = " NA", Telefono = "NA", Correo = "NA", Observacion = null,  CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null}
               });

            context.SaveChanges();
        }
        private void CreateCliente(RSIModelContextDB context)
        {
            var tipoListaId = context.TipoListas.FirstOrDefault(t => t.Codigo == "TIPODOCID").Id;
            var documentoId = context.Listas.FirstOrDefault(t => t.TipoListaId == tipoListaId && t.Codigo == "13").Id;
            context.Clientes.AddOrUpdate(t => t.NumeroDocumentoIdentidad,
               new Cliente[]
               {
                    new Cliente { Id = 1, DocumentoIdentidadId  = documentoId, NumeroDocumentoIdentidad = "7924785", FechaNacimiento = new DateTime(1977,5,19),Apodo = "Joaco", Direccion = "Cra 44 26-51 Bello", Telefono = "6528855", Correo = "jobeme@gmail.com", Observacion = "Cliente de prueba", NombreORazonSocial = "Joaquin Bermúdez", CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null},
                    new Cliente { Id = 2, DocumentoIdentidadId  = documentoId, NumeroDocumentoIdentidad = "1234567", FechaNacimiento = new DateTime(1977,5,19),Apodo = "Joaco", Direccion = "Cra 44 26-51 Bello", Telefono = "6528855", Correo = "jobeme@gmail.com", Observacion = "Cliente de prueba", NombreORazonSocial = "Prueba", CreadoPor = creadoPor, FechaCreacion = fechaCreacion, FechaModificacion = null, ModificadoPor = null}
               });

            context.SaveChanges();
        }
    }
}
