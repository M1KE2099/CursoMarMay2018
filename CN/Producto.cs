﻿using CD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Producto // se agrego public para permitir accesso al Windows form  HomeDepot despues de agregar la referencia
    {
        #region Propiedades
        public int idProducto { get; }
        public string unidadMed { get; }
        public string descripcion { get; }
        public float precioUnit { get; }
        public bool esActivo { get; }
        public DateTime fechaCreacion { get; }

        #endregion

        #region Constructores

        public Producto (int _idProducto, string _unidadMed, string _descripcion, float _precioUnit,
           bool _esActivo)
        {
            idProducto = _idProducto;
            unidadMed = _unidadMed;
            descripcion = _descripcion;
            precioUnit = _precioUnit;
            esActivo = _esActivo;
      
        }

        public Producto (DataRow fila) //DataRow, sus propiedades y métodos son utilizados para recuperar, evaluar, insertar, eliminar y actualizar los valores de DataTable(SQL)
        {
            idProducto = fila.Field<int>("idProducto");//entre comillas estan los nombres de las columnas en la tabla de SQL
            unidadMed = fila.Field<string>("unidadMed");
            descripcion = fila.Field<string>("descripcion");
            precioUnit = fila.Field<float>("precioUnit");
            esActivo = fila.Field<bool>("esActivo");
            fechaCreacion = fila.Field<DateTime>("fechaCreacion");
            }

        #endregion

        #region Metodos y Funciones
        public void guardar()
        {
            //throw new NotImplementedException();

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@unidadMed", unidadMed)); 
            parametros.Add(new SqlParameter("@descripcion", descripcion));
            parametros.Add(new SqlParameter("@precioUnit", precioUnit));
            parametros.Add(new SqlParameter("@esActivo", esActivo));


            try
            {
                if (idProducto > 0)
                {
                    //Update
                    parametros.Add(new SqlParameter("@idProducto", idProducto));
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPUProductos", parametros.ToArray()) == 0)  //entre comillas esta el nombre del stored procedure en la base de datos.
                    {
                        throw new Exception("No se acutalizo el registro");
                    }
                }
                else
                {
                    //Insert
                    if (DataBaseHelper.ExecuteNonQuery("dbo.SPIProductos", parametros.ToArray()) == 0)  //entre comillas esta el nombre del stored procedure en la base de datos.
                    {
                        throw new Exception("No se creo el registro");
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }

        }
        public static void desactivar(int idProducto, bool esActivo = false)
        {

            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idProducto", idProducto));
            parametros.Add(new SqlParameter("@esActivo", esActivo));

            try
            {
                if (DataBaseHelper.ExecuteNonQuery("dbo.SPDProductos", parametros.ToArray()) == 0)  //entre comillas esta el nombre del stored procedure en la base de datos.
                {
                    throw new Exception("No se desactivo el registro");
                }
            }
            catch (Exception ex)
            {

#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }

        }
        public static Producto buscarPorId(int idProducto)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@idProducto", idProducto));

            DataTable dt = new DataTable();


            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSProductos", parametros.ToArray());

                Producto resultado = null;
                foreach (DataRow fila in dt.Rows)
                {
                    resultado = new Producto(fila);
                    break;
                }
                if (resultado == null)
                {
                    throw new Exception("No se han encontrado coincidencias.");
                }
                return resultado;
            }
            catch (Exception ex)
            {

#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }
        }

        public static List<Producto> traerTodos(bool filtrarsoloActivos = false)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (filtrarsoloActivos)
            {
                parametros.Add(new SqlParameter("@esActivo", true));
            }

            DataTable dt = new DataTable();


            try
            {
                DataBaseHelper.Fill(dt, "dbo.SPSProductos", parametros.ToArray());

                List<Producto> listado = new List<Producto>();
                foreach (DataRow fila in dt.Rows)
                {
                    listado.Add(new Producto(fila));

                }

                return listado;
            }
            catch (Exception ex)
            {

#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception("Ha ocurrido un error con la base de datos");
#endif
            }

            #endregion

        }
    }
}
