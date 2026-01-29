using System;
using System.Collections.Generic;

namespace Tienda.Models.data;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public bool? Activo { get; set; }

    public bool? Nuevo { get; set; }

    public bool? Oferta { get; set; }

    public decimal? PrecioAnterior { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();
}
