using System;
using System.Collections.Generic;

namespace Tienda.Models.data;

public partial class Carrito
{
    public int CarritoId { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();
}
