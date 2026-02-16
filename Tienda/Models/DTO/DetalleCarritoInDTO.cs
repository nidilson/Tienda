using System.Text.Json.Serialization;

namespace Tienda.Models.DTO
{
	public class DetalleCarritoInDTO
	{
		[JsonPropertyName("detalle_id")]
		public int DetalleId { get; set; }
		[JsonPropertyName("producto_id")]
		public int ProductoId { get; set; }
		[JsonPropertyName("cantidad")]
		public int Cantidad { get; set; }
	}
}
