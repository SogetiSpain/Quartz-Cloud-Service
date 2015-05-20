# Quartz-Cloud-Service
<b>Programador de tareas en la nube (Azure worker role + Quartz.NET)</b>

Muchas veces en el desarrollo de los aplicativos de negocio se requiere dotar de funcionalidad extra para cubrir aspectos relacionados con la ejecución de múltiples tareas en segundo plano, tales como, envío de correo electrónico, mantenimiento de datos o creación de informes.

Supongamos que tenemos ejecutando un servidor IIS con una aplicación ASP.NET MVC o con los servicios WCF necesarios para dar soporte a nuestro aplicativo de negocio.  En cualquier caso, para ejecutar las tareas en segundo plano y que dichas tareas no consuman hilos del IIS optaremos por desarrollar un servicio Windows .NET para estos menesteres.

Pero, ¿y si este concepto lo orientamos a la programación en la nube?  Aquí es cuando entra en juego la plataforma Azure y los Worker Roles.

[Seguir leyendo](http://bit.ly/1cOuz69)

<i><b><u>NOTA:</u></b> Se ha actualizado la solución para que utilice la versión 2.6 de Azure SDK.</i>
