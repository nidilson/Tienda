using System;
using System.Collections.Generic;

namespace Tienda.Models.data;

public partial class CarritoDetalle
{
    public int CarritoDetalleId { get; set; }

    public int CarritoId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Carrito Carrito { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
