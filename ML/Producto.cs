namespace ML
{
    public class Producto
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public float? Precio { get; set; }
        public Categoria? Categoria { get; set; }
        public List<ML.Producto>? Productos { get; set; }
        public Informacion? Informacion { get; set; }
    }
}