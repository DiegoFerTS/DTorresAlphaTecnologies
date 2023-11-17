using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static List<ML.Categoria> GetAll()
        {
            ML.Categoria categoria = new ML.Categoria();
            categoria.Categorias = new List<ML.Categoria>();


            using (DL.DtorresAlphaTecnologiesContext conexion = new DL.DtorresAlphaTecnologiesContext())
            {

                var query = (from tablaCategoria in conexion.Categoria
                             select new
                             {
                                 IdCategoria = tablaCategoria.Id,
                                 NombreCategoria = tablaCategoria.Nombre
                             }).ToList();

                if (query.Count > 0)
                {
                    foreach (var datos in query)
                    {
                        ML.Categoria categoriaResult = new ML.Categoria();

                        categoriaResult.Id = datos.IdCategoria;
                        categoriaResult.Nombre = datos.NombreCategoria;

                        categoria.Categorias.Add(categoriaResult);
                    }
                } 
            }
            return categoria.Categorias;
        }
    }
}
