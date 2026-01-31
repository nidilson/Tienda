using System.Text.Json.Serialization;

namespace Tienda.Models.DTO
{
	public class DetalleCarritoOutDTO
	{
		[JsonPropertyName("detalle_id")]
		public int Id { get; set; }
		[JsonPropertyName("producto_nombre")]
		public string Producto { get; set; }

		[JsonPropertyName("cantidad")]
		public int Cantidad { get; set; }
		[JsonPropertyName("precio")]
		public decimal Precio { get; set; }
	}
}
