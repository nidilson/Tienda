using System.Text.Json.Serialization;

namespace Tienda.Models.DTO
{
	public class DetalleCarritoInDTO
	{
		[JsonPropertyName("producto_id")]
		public int ProductoId { get; set; }
		[JsonPropertyName("cantidad")]
		public int Cantidad { get; set; }
	}
}
