using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;
using System.Diagnostics.Metrics;

namespace BL
{
    public class Productos
    {
        public static ML.Informacion Add(ML.Producto producto)
        {
            producto.Informacion = new ML.Informacion();
            try
            {
                using (DL.DtorresAlphaTecnologiesContext conexion = new DL.DtorresAlphaTecnologiesContext())
                {
                    DL.Producto nuevoProducto = new DL.Producto();

                    nuevoProducto.Nombre = producto.Nombre;
                    nuevoProducto.Precio = producto.Precio;
                    nuevoProducto.IdCategoria = producto.Categoria.Id;

                    conexion.Productos.Add(nuevoProducto);
                    conexion.SaveChanges();
                }
                producto.Informacion.Estatus = true;
                producto.Informacion.Mensaje = "Se registro el producto: " + producto.Nombre + ".";
            }
            catch (Exception ex)
            {
                producto.Informacion.Estatus = false;
                producto.Informacion.Mensaje = "Ocurrio un error: " + ex.Message;
            }
            return producto.Informacion;
        }


        public static ML.Informacion Update(ML.Producto producto)
        {
            producto.Informacion = new ML.Informacion();

            try
            {
                using (DL.DtorresAlphaTecnologiesContext conexion = new DL.DtorresAlphaTecnologiesContext())
                {
                    var query = (from tablaProducto in conexion.Productos
                                 where tablaProducto.Id == producto.Id.Value
                                 select tablaProducto).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = producto.Nombre;
                        query.Precio = producto.Precio;
                        query.IdCategoria = producto.Categoria.Id;

                        conexion.SaveChanges();

                        producto.Informacion.Estatus = true;
                        producto.Informacion.Mensaje = "El producto " + producto.Nombre + " ha sido actualizado.";
                    }
                }
            }
            catch (Exception ex)
            {
                producto.Informacion.Estatus = false;
                producto.Informacion.Mensaje = "Ocurrio un error: " + ex.Message;
            }

            return producto.Informacion;
        }


        public static ML.Informacion Delete(int idProducto)
        {
            ML.Informacion informacion = new ML.Informacion();

            try
            {
                using (DL.DtorresAlphaTecnologiesContext conexion = new DL.DtorresAlphaTecnologiesContext())
                {
                    var query = (from tablaProducto in conexion.Productos
                                 where tablaProducto.Id == idProducto
                                 select tablaProducto).First();

                    if (query != null)
                    {
                        conexion.Productos.Remove(query);
                        conexion.SaveChanges();

                        informacion.Estatus = true;
                        informacion.Mensaje = "Se ha eliminado el producto con el id: " + idProducto + ".";
                    }
                    else
                    {
                        informacion.Estatus = false;
                        informacion.Mensaje = "No se elimino el producto.";
                    }
                }
            }
            catch (Exception ex)
            {
                informacion.Estatus = false;
                informacion.Mensaje = "Ocurrio un error: " + ex.Message;
            }

            return informacion;
        }


        public static ML.Producto GetAll(string? nombreProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Productos = new List<ML.Producto>();
            producto.Informacion = new ML.Informacion();

            try
            {
                using (DL.DtorresAlphaTecnologiesContext conexion = new DL.DtorresAlphaTecnologiesContext())
                {

                    if (nombreProducto != null && nombreProducto != "")
                    {
                        var query = (from tablaProducto in conexion.Productos
                                     join tablaCategoria in conexion.Categoria on tablaProducto.IdCategoria equals tablaCategoria.Id
                                     where tablaProducto.Nombre.Contains(nombreProducto)
                                     select new
                                     {
                                         IdProducto = tablaProducto.Id,
                                         NombreProducto = tablaProducto.Nombre,
                                         Precio = tablaProducto.Precio,
                                         IdCategoria = tablaCategoria.Id,
                                         NombreCategoria = tablaCategoria.Nombre
                                     }).ToList();

                        if (query.Count > 0)
                        {
                            foreach (var datos in query)
                            {
                                ML.Producto productoResult = new ML.Producto();
                                productoResult.Categoria = new ML.Categoria();

                                productoResult.Id = datos.IdProducto;
                                productoResult.Nombre = datos.NombreProducto;
                                productoResult.Precio = float.Parse(datos.Precio.ToString());
                                productoResult.Categoria.Id = datos.IdCategoria;
                                productoResult.Categoria.Nombre = datos.NombreCategoria;

                                producto.Productos.Add(productoResult);
                            }

                            producto.Informacion.Estatus = true;
                            producto.Informacion.Mensaje = "Se encontraron " + producto.Productos.Count + " resultados.";
                        }
                        else
                        {
                            producto.Informacion.Estatus = false;
                            producto.Informacion.Mensaje = "No se encontraron resultados.";
                        }
                    } else
                    {
                        var query = (from tablaProducto in conexion.Productos
                                     join tablaCategoria in conexion.Categoria on tablaProducto.IdCategoria equals tablaCategoria.Id
                                     select new
                                     {
                                         IdProducto = tablaProducto.Id,
                                         NombreProducto = tablaProducto.Nombre,
                                         Precio = tablaProducto.Precio,
                                         IdCategoria = tablaCategoria.Id,
                                         NombreCategoria = tablaCategoria.Nombre
                                     }).ToList();

                        if (query.Count > 0)
                        {
                            foreach (var datos in query)
                            {
                                ML.Producto productoResult = new ML.Producto();
                                productoResult.Categoria = new ML.Categoria();

                                productoResult.Id = datos.IdProducto;
                                productoResult.Nombre = datos.NombreProducto;
                                productoResult.Precio = float.Parse(datos.Precio.ToString());
                                productoResult.Categoria.Id = datos.IdCategoria;
                                productoResult.Categoria.Nombre = datos.NombreCategoria;

                                producto.Productos.Add(productoResult);
                            }

                            producto.Informacion.Estatus = true;
                            producto.Informacion.Mensaje = "Se encontraron " + producto.Productos.Count + " resultados.";
                        }
                        else
                        {
                            producto.Informacion.Estatus = false;
                            producto.Informacion.Mensaje = "No se encontraron resultados.";
                        }
                    }                                        
                }
            }
            catch (Exception ex)
            {
                producto.Informacion.Estatus = false;
                producto.Informacion.Mensaje = "Ocurrio un error: " + ex.Message;
            }

            return producto;
        }


        public static ML.Producto GetById(int idProducto)
        {
            ML.Producto producto = new ML.Producto();
            producto.Categoria = new ML.Categoria();
            producto.Informacion = new ML.Informacion();

            try
            {
                using (DL.DtorresAlphaTecnologiesContext conexion = new DL.DtorresAlphaTecnologiesContext())
                {
                    var query = (from tablaProducto in conexion.Productos
                                 join tablaCategoria in conexion.Categoria 
                                 on tablaProducto.IdCategoria equals tablaCategoria.Id
                                 where tablaProducto.Id == idProducto
                                 select new
                                 {
                                     IdProducto = tablaProducto.Id,
                                     NombreProducto = tablaProducto.Nombre,
                                     Precio = tablaProducto.Precio,
                                     IdCategoria = tablaCategoria.Id,
                                     NombreCategoria = tablaCategoria.Nombre
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        producto.Id = query.IdProducto;
                        producto.Nombre = query.NombreProducto;
                        producto.Precio = float.Parse(query.Precio.ToString());
                        producto.Categoria.Id = query.IdCategoria;
                        producto.Categoria.Nombre = query.NombreCategoria;

                        producto.Informacion.Estatus = true;
                    } else
                    {
                        producto.Informacion.Estatus = false;
                        producto.Informacion.Mensaje = "No se encontro el producto.";
                    }
                }
            }
            catch (Exception ex)
            {
                producto.Informacion.Estatus = false;
                producto.Informacion.Mensaje = "Ocurrio un error: " + ex.Message;
            }

            return producto;
        }
    }
}