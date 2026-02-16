##Tienda Virtual – Prototipo

Este proyecto es un prototipo de tienda virtual desarrollado con fines educativos y de demostración, enfocado en la arquitectura web, buenas prácticas y separación de responsabilidades entre frontend y backend.

La aplicación simula el flujo básico de una tienda en línea: visualización de productos, carrito de compras y comunicación con el servidor.

##Objetivo del proyecto

Simular el funcionamiento básico de una tienda virtual

Aplicar el patrón MVC

Practicar el consumo de vistas parciales y fetch

Implementar un carrito de compras controlado desde el servidor

Demostrar buenas prácticas de seguridad (no confiar en el cliente)

##Tecnologías utilizadas
#Backend

ASP.NET Core MVC

Entity Framework Core

SQL Server

#Frontend

HTML5

CSS3

JavaScript (Fetch API)

Bootstrap (opcional)

##Arquitectura

El proyecto sigue el patrón Modelo–Vista–Controlador (MVC):

Modelo:
Entidades y acceso a datos mediante Entity Framework Core.

Vista:
Vistas Razor (.cshtml) y Partial Views para actualizar secciones de la interfaz sin recargar la página.

Controlador:
Maneja las solicitudes del cliente, coordina la lógica y devuelve vistas o respuestas HTML.

##Carrito de compras

El carrito se gestiona del lado del servidor

El cliente solo envía acciones (agregar, quitar, actualizar)

Los precios y totales siempre se calculan en el backend

Se utiliza HttpContext.Session para mantener el estado del carrito

Esto evita manipulaciones del lado del cliente y mejora la seguridad del prototipo.

##Actualización dinámica de la interfaz

Se utilizan Partial Views

Las vistas parciales se cargan dinámicamente mediante fetch

El JavaScript se carga una sola vez desde el layout

Se usa delegación de eventos para manejar elementos dinámicos


##Limitaciones del prototipo

No incluye pasarela de pagos real

No incluye autenticación completa

No está optimizado para producción

La sesión se pierde al reiniciar el servidor
