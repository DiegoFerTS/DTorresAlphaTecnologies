using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [Route("GetAll/{nombreProducto?}")]
        [HttpGet]
        public ActionResult GetAll(string? nombreProducto = null)
        {
            ML.Producto producto = BL.Productos.GetAll(nombreProducto);

            if (producto.Informacion.Estatus.Value)
            {
                return StatusCode(200, producto);
            } else
            {
                return StatusCode(400, producto);
            }
        }

        [Route("GetById/{idProducto}")]
        [HttpGet]
        public ActionResult GetById(int idProducto)
        {
            ML.Producto producto = BL.Productos.GetById(idProducto);

            if (producto.Informacion.Estatus.Value)
            {
                return StatusCode(200, producto);
            }
            else
            {
                return StatusCode(400, producto);
            }
        }

        [Route("Add/")]
        [HttpPost]
        public ActionResult Add(ML.Producto producto)
        {
            ML.Informacion informacion = BL.Productos.Add(producto);

            if (informacion.Estatus.Value)
            {
                return StatusCode(200, informacion);
            }
            else
            {
                return StatusCode(400, informacion);
            }
        }

        [Route("Update/{idProducto}")]
        [HttpPut]
        public ActionResult Update(int idProducto ,[FromBody] ML.Producto producto)
        {
            producto.Id = idProducto;
            ML.Informacion informacion = BL.Productos.Update(producto);

            if (informacion.Estatus.Value)
            {
                return StatusCode(200, informacion);
            }
            else
            {
                return StatusCode(400, informacion);
            }
        }

        [Route("Delete/{idProducto}")]
        [HttpDelete]
        public ActionResult Delete(int idProducto)
        {
            ML.Informacion informacion = BL.Productos.Delete(idProducto);

            if (informacion.Estatus.Value)
            {
                return StatusCode(200, informacion);
            }
            else
            {
                return StatusCode(400, informacion);
            }
        }
    }
}
