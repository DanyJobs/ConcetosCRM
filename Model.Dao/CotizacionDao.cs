﻿using Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Model.Dao
{
    public class CotizacionDao
    {
        private ConexionDB objConexinDB;
        private SqlCommand comando;
        private SqlDataReader reader;
        public CotizacionDao()
        {
            objConexinDB = ConexionDB.saberEstado();

        }

        public string create(Cotizacion objVenta)
        {
            string idVenta = "";
            string create = "insert into cotizacion(total,idCliente,idVendedor,fecha,IVA,notas,notasCompras,estatus,diasSeguimiento,estatusCotizacion,estatusSeguimiento,fechaComienzoSeguimiento) values('" + objVenta.Total + "','" + objVenta.IdCliente + "','" + objVenta.IdVendedor + "','" + objVenta.Fecha + "','" + objVenta.Iva + "','" + objVenta.notas + "','" + objVenta.notasCompras + "','" + objVenta.estatus+ "'," + objVenta.diasSeguimiento + ",'" + objVenta.estatusCotizacion + "','" + objVenta.estatusSeguimiento + "','" + objVenta.fechaComienzoSeguimiento + "') SELECT SCOPE_IDENTITY();";
            try
            {
                comando = new SqlCommand(create, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }

                //RECUPERAR EL CODIGO AUTOGENERADO
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    idVenta = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return idVenta;

        }
        public void update(Cotizacion objVenta)
        {
            string update = "update cotizacion set total='" + objVenta.Total + "',idCliente='" + objVenta.IdCliente + "',idVendedor='" + objVenta.IdVendedor + "',fecha='" + objVenta.Fecha + "',IVA='" + objVenta.Iva + "' where idVenta='" + objVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(update, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
        }
        public void delete(Cotizacion objVenta)
        {
            string delete = "delete from cotizacion where idVenta='" + objVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(delete, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }

        }
        public bool find(Cotizacion objVenta)
        {
            bool hayRegistros;
            string find = "select*from cotizacion where idVenta='" + objVenta.IdVenta + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objVenta.Total = Convert.ToDouble(reader[1].ToString());
                    objVenta.IdCliente = Convert.ToInt64(reader[2].ToString());
                    objVenta.IdVendedor = reader[3].ToString();
                    objVenta.Fecha = reader[4].ToString();
                    objVenta.Iva = Convert.ToDouble(reader[5].ToString());
                    objVenta.Estado = 99;

                }
                else
                {
                    objVenta.Estado = 1;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }
        public List<Cotizacion> findAll(string Usuario)
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();

            try
            {
                comando = new SqlCommand("SP_Buscar_Cotizacion", objConexinDB.getCon());
                comando.Parameters.AddWithValue("@idUsuario", Usuario);
                comando.CommandType = CommandType.StoredProcedure;
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    Cotizacion objVenta = new Cotizacion();
                    objVenta.IdVenta = Convert.ToInt64(reader[0].ToString());
                    objVenta.NombreCliente = reader[1].ToString();
                    objVenta.IdVendedor = reader[2].ToString();
                    objVenta.Iva = Convert.ToDouble(reader[3].ToString());
                    objVenta.Total = Convert.ToDouble(reader[4].ToString());
                    objVenta.FechaCotizacion = reader.GetDateTime(5);
                    listaVentas.Add(objVenta);
                }
            }
            catch (Exception){
                throw;
            }
            finally{
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return listaVentas;
        }
        public bool findVentaPorClienteId(Cotizacion objVenta)
        {
            bool hayRegistros;
            string find = "select*from cotizacion where idCliente='" + objVenta.IdCliente + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objVenta.Estado = 99;
                }
                else
                {
                    objVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }
        public bool findVentaPorVendedorId(Cotizacion objVenta)
        {
            bool hayRegistros;
            string find = "select*from cotizacion where idVendedor='" + objVenta.IdVendedor + "'";
            try
            {
                comando = new SqlCommand(find, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                SqlDataReader reader = comando.ExecuteReader();
                hayRegistros = reader.Read();
                if (hayRegistros)
                {
                    objVenta.Estado = 99;
                }
                else
                {
                    objVenta.Estado = 1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return hayRegistros;
        }
        public void sp_reporteVenta(Cotizacion objVenta)
        {
            string create = "sp_reporte_venta" + objVenta.IdVenta;
            try
            {
                comando = new SqlCommand(create, objConexinDB.getCon());
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                objVenta.Estado = 1;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
        }
        //Para la parte de mostrar las cotizaciones y editar
        //Trae las cotizaciones sin necesitar ningun parametro
        public List<Cotizacion> buscar()
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();


            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaCotizacion";
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("pMonth", "");
            command.Parameters.AddWithValue("pYear", "");
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            if (objConexinDB.getCon().State == ConnectionState.Closed)
            {
                objConexinDB.getCon().Open();
            }
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtCotizacion.Rows.Count; i++)
            {
                Cotizacion c = new Cotizacion();
                c.IdVenta = int.Parse(dtCotizacion.Rows[i]["idVenta"].ToString());
                c.Total = Convert.ToDouble(dtCotizacion.Rows[i]["total"].ToString());
                c.Cliente = dtCotizacion.Rows[i]["Cliente"].ToString();
                c.IdVendedor = dtCotizacion.Rows[i]["UserName"].ToString();
                c.FechaCotizacion = Convert.ToDateTime(dtCotizacion.Rows[i]["fecha"].ToString());
                c.Iva = Convert.ToDouble(dtCotizacion.Rows[i]["IVA"].ToString());
                listaVentas.Add(c);
            }
            //Se regresa el objeto            
            return listaVentas;
        }
        //Trae las cotizaciones según el año y mes
        public List<Cotizacion> buscar(string Month, string Year)
        {
            List<Cotizacion> listaVentas = new List<Cotizacion>();
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaCotizacion";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("pMonth", Month);
            command.Parameters.AddWithValue("pYear", Year);
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            if (objConexinDB.getCon().State == ConnectionState.Closed)
            {
                objConexinDB.getCon().Open();
            }
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Se llena la lista
            for (int i = 0; i < dtCotizacion.Rows.Count; i++)
            {
                Cotizacion c = new Cotizacion();
                c.IdVenta = int.Parse(dtCotizacion.Rows[i]["idVenta"].ToString());
                c.Total = Convert.ToDouble(dtCotizacion.Rows[i]["total"].ToString());
                c.Cliente = dtCotizacion.Rows[i]["Cliente"].ToString();
                c.IdVendedor = dtCotizacion.Rows[i]["UserName"].ToString();
                c.FechaCotizacion = Convert.ToDateTime(dtCotizacion.Rows[i]["fecha"].ToString());
                c.Iva = Convert.ToDouble(dtCotizacion.Rows[i]["IVA"].ToString());                
                listaVentas.Add(c);
            }
            //Se regresa el objeto            
            return listaVentas;
        }

        //Trae una cotizacion según el id
        public Cotizacion buscarIdVenta(int idVenta)
        {            
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_consultaCotizacionIndividual";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("idVenta", idVenta);            
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se crea la tabla
            DataTable dtCotizacion = new DataTable();
            //Se abre la conexión
            if (objConexinDB.getCon().State == ConnectionState.Closed)
            {
                objConexinDB.getCon().Open();
            }
            //Se le da el comando al adaptador
            adapter.SelectCommand = command;
            //Se llena la tabla con el adaptador
            adapter.Fill(dtCotizacion);
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
            //Objeto Cotizacion
            Cotizacion c = new Cotizacion();
            //Se llena la lista
            for (int i = 0; i < dtCotizacion.Rows.Count; i++)
            {
                
                c.IdVenta = int.Parse(dtCotizacion.Rows[i]["idVenta"].ToString());
                c.Total = Convert.ToDouble(dtCotizacion.Rows[i]["total"].ToString());
                c.Cliente = dtCotizacion.Rows[i]["Cliente"].ToString();
                c.IdCliente = int.Parse(dtCotizacion.Rows[i]["idCliente"].ToString());
                c.Email= dtCotizacion.Rows[i]["Email"].ToString();
                c.FechaCotizacion = Convert.ToDateTime(dtCotizacion.Rows[i]["fecha"].ToString());
                c.Iva = Convert.ToDouble(dtCotizacion.Rows[i]["IVA"].ToString());
                c.estatus = dtCotizacion.Rows[i]["estatus"].ToString();
                c.notas = dtCotizacion.Rows[i]["notas"].ToString();
                c.notasCompras = dtCotizacion.Rows[i]["notasCompras"].ToString();
            }
            //Se regresa el objeto            
            return c;
        }
        //Metodo para actualizar al información de la cotizacion y cotización detalle
        public void Actualizar(int idVenta, decimal Total, int IdCliente, string idVendedor, string fecha, decimal IVA, string Notas, string NotasCompras, string Estatus)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_actualizarCotizacion";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", idVenta);
            command.Parameters.AddWithValue("Total", Total);
            command.Parameters.AddWithValue("IdCliente", IdCliente);
            command.Parameters.AddWithValue("IdVendedor", idVendedor);
            command.Parameters.AddWithValue("Fecha", fecha);
            command.Parameters.AddWithValue("IVA", IVA);
            command.Parameters.AddWithValue("Notas", Notas);
            command.Parameters.AddWithValue("NotasCompras", NotasCompras);
            command.Parameters.AddWithValue("Estatus", Estatus);            
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se abre la conexión
            if (objConexinDB.getCon().State == ConnectionState.Closed)
            {
                objConexinDB.getCon().Open();
            }
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }
        //Metodo para eliminar la cotizacion
        public void Eliminar(int idVenta)
        {
            //Comando de uso
            SqlCommand command = new SqlCommand();
            //Tipo de comando-Procedimiento almacenado
            command.CommandType = CommandType.StoredProcedure;
            //Nombre de procedimiento almacenado
            command.CommandText = "sp_eliminarCotizacion";
            //Se le pasan los parametros            
            command.Parameters.AddWithValue("IdVenta", idVenta);            
            //Se le asigna la conexión a utilizar al comando
            command.Connection = objConexinDB.getCon();
            //Se crea el adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter();
            //Se abre la conexión
            if (objConexinDB.getCon().State == ConnectionState.Closed)
            {
                objConexinDB.getCon().Open();
            }
            //Se ejecuta el comando
            command.ExecuteNonQuery();
            //Se cierra la conexión
            objConexinDB.getCon().Close();
            command.Connection.Close();
        }

        public List<Cotizacion> buscarConEstatus(string Month, string Year, string Estatus)
        {
            List<Cotizacion> objListaCotizacion = new List<Cotizacion>();
            try
            {
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("sp_consultaCotizacionEstatus", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Month", Month);
                cmd.Parameters.AddWithValue("@Year", Year);
                cmd.Parameters.AddWithValue("@Estatus", Estatus);
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cotizacion objCotizacion = new Cotizacion();
                    objCotizacion.IdVenta = int.Parse(reader[0].ToString());
                    objCotizacion.Total = Convert.ToDouble(reader[1].ToString());
                    objCotizacion.Cliente = reader[2].ToString();
                    objCotizacion.FechaCotizacion = Convert.ToDateTime(reader[3].ToString());
                    objCotizacion.Iva = Convert.ToDouble(reader[4].ToString());
                    objCotizacion.estatus = reader[5].ToString();
                    objListaCotizacion.Add(objCotizacion);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return objListaCotizacion;
        }

        public List<Cotizacion> buscarConEstatus()
        {
            List<Cotizacion> objListaCotizacion = new List<Cotizacion>();
            try
            {
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("sp_consultaCotizacionEstatus", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cotizacion objCotizacion = new Cotizacion();
                    objCotizacion.IdVenta = int.Parse(reader[0].ToString());
                    objCotizacion.Total = Convert.ToDouble(reader[1].ToString());
                    objCotizacion.Cliente = reader[2].ToString();
                    objCotizacion.FechaCotizacion = Convert.ToDateTime(reader[3].ToString());
                    objCotizacion.Iva = Convert.ToDouble(reader[4].ToString());
                    objCotizacion.estatus = reader[5].ToString();
                    objListaCotizacion.Add(objCotizacion);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return objListaCotizacion;
        }

        public List<EmailMarketingCorreos> buscarEmailMarketing(string id)
        {
            List<EmailMarketingCorreos> objListaCotizacion = new List<EmailMarketingCorreos>();
            try
            {
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("consultaEmailMarketing", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idUsuario", id);
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EmailMarketingCorreos objEmailMarketing = new EmailMarketingCorreos();
                    objEmailMarketing.idMarketing = int.Parse(reader[0].ToString());
                    objEmailMarketing.nombre = reader[1].ToString();
                    objEmailMarketing.email = reader[2].ToString();
                    objEmailMarketing.fechaComienzo = Convert.ToDateTime(reader[4].ToString());
                    objEmailMarketing.dias = int.Parse(reader[3].ToString());
                    objEmailMarketing.idUsuario = reader[5].ToString();
                    objListaCotizacion.Add(objEmailMarketing);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return objListaCotizacion;
        }

        public string agregarRFQ(string idVenta,string idVendedor)
        {
            string mensaje = "";

            try
            {
                SqlCommand cmd = new SqlCommand("AgregarRFQ", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idVenta", Convert.ToInt32(idVenta));
                cmd.Parameters.AddWithValue("@idVendedor", idVendedor);
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mensaje = reader[0].ToString();           
                }
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return mensaje;
        }
        public List<RFQItem> buscarListaProductosRFQ(string id)
        {
            List<RFQItem> objListaRFQItem = new List<RFQItem>();
            //try
            //{
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("RFQConsultas", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "I");
                cmd.Parameters.AddWithValue("@idRfq", Convert.ToInt32(id));
            if (objConexinDB.getCon().State == ConnectionState.Closed) {
                objConexinDB.getCon().Open();
            }                
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                RFQItem objRFQ = new RFQItem();
                objRFQ.idRFQItem = int.Parse(reader[0].ToString());
                objRFQ.idProducto = reader[1].ToString();
                objRFQ.nombre = reader[2].ToString();
                if (!reader.IsDBNull(3))
                {
                    objRFQ.precio = reader.GetDecimal(3);
                }
            
                    objRFQ.cantidad = int.Parse(reader[4].ToString());
                    objRFQ.notas = reader[5].ToString();
                if (!reader.IsDBNull(6))
                {
                    objRFQ.idProveedor = int.Parse(reader[6].ToString());
                    objRFQ.empresa = reader[7].ToString();
                    objRFQ.empresaNombre = reader[8].ToString();      
                }
                else {
                    objRFQ.empresa = "N/A";
                    objRFQ.idProveedor = 0;
                    objRFQ.empresaNombre = "N/A";
                }
                objRFQ.marca = reader[9].ToString();
                if (!reader.IsDBNull(10))
                {
                    objRFQ.unidadDeMedida = reader[10].ToString();
                }
                else
                {
                    objRFQ.unidadDeMedida = "N/A";
                }

                    objListaRFQItem.Add(objRFQ);
                }

            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            //}
            return objListaRFQItem;
        }
        public RFQ buscarListaRFQ(string id)
        {
            RFQ objRFQ = new RFQ();
            try
            {
            //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
            SqlCommand cmd = new SqlCommand("RFQConsultas", objConexinDB.getCon());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@opcion", "R");
            cmd.Parameters.AddWithValue("@idRfq",int.Parse(id));
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                objRFQ.idRFQ = int.Parse(reader[0].ToString());
                objRFQ.idVendedor = reader[1].ToString();
                objRFQ.fecha = DateTime.Parse(reader[2].ToString());
                objRFQ.estatus = reader[3].ToString();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return objRFQ;
        }

        public List<RFQ> buscarListaRFQ()
        {
            List<RFQ> objListaRFQ = new List<RFQ>();
            try
            {
                //string findAll = "select*from cliente where nombre='" + objCLiente.Nombre + "' or dni='" + objCLiente.Dni + "' or idCliente=" + objCLiente.IdCliente + " or apPaterno='" + objCLiente.Appaterno + "'";
                SqlCommand cmd = new SqlCommand("RFQConsultas", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "T");
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RFQ objRFQ = new RFQ();
                    objRFQ.idRFQ = int.Parse(reader[0].ToString());
                    objRFQ.idVendedor = reader[1].ToString();
                    objRFQ.fecha = DateTime.Parse(reader[2].ToString());
                    objRFQ.estatus = reader[3].ToString();
                    objListaRFQ.Add(objRFQ);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return objListaRFQ;
        }
        public string updateRFQ(RFQ rfq)
        {
            string mensaje = "";

            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditarRFQ", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "R");
                cmd.Parameters.AddWithValue("@estatus", rfq.estatus);
                cmd.Parameters.AddWithValue("@idRFQ", Convert.ToInt32(rfq.idRFQ));
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                mensaje = "Actualizado Correctamente";
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return mensaje;
        }

        public string updateRFQItem(RFQItem rfqItem)
        {
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditarRFQ", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "I");
                cmd.Parameters.AddWithValue("@idRFQ", rfqItem.idRfq);
                cmd.Parameters.AddWithValue("@idRFQItem", rfqItem.idRFQItem);
                cmd.Parameters.AddWithValue("@idProveedor", rfqItem.idProveedor);
                cmd.Parameters.AddWithValue("@idProducto",rfqItem.idProducto);
                cmd.Parameters.AddWithValue("@precio", rfqItem.precio);
                cmd.Parameters.AddWithValue("@cantidad", rfqItem.cantidad);
                cmd.Parameters.AddWithValue("@notas", rfqItem.notas);
                cmd.Parameters.AddWithValue("@fecha", rfqItem.fecha);
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                mensaje = "Actualizado Correctamente";
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return mensaje;
        }

        public string eliminadosRFQ(RFQItemEliminado rfqItem)
        {
            string mensaje = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EditarRFQ", objConexinDB.getCon());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opcion", "E");
                cmd.Parameters.AddWithValue("@idRFQ", rfqItem.idRfq);
                cmd.Parameters.AddWithValue("@idRFQItem", rfqItem.idRFQItem);
                if (objConexinDB.getCon().State == ConnectionState.Closed)
                {
                    objConexinDB.getCon().Open();
                }
                reader = cmd.ExecuteReader();
                mensaje = "Actualizado Correctamente";
            }
            catch (Exception e)
            {
                mensaje = e.ToString();
            }
            finally
            {
                objConexinDB.getCon().Close();
                objConexinDB.closeDB();
            }
            return mensaje;
        }


    }
}
