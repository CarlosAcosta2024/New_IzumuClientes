DOCUMENTO DE INSTALACION: PROYECTO IZUMUCLIENTES 
 

INTRODUCCIÓN 

El proyecto IzumuClientes es una solución integral desarrollada para la gestión de clientes. 

Se compone de una interfaz gráfica basada en ASP.NET Core Razor Pages, un microservicio backend en .NET Core 8 y una base de datos SQL Server para la persistencia de datos. 
 

En este documento detalló el proceso de instalación y configuración necesaria para desplegar la solución en un entorno local o de producción, asegurando que todos los componentes funcionen de manera coordinada. 



OBJETIVO 

El objetivo de este documento es proporcionar una guía clara y detallada de la instalación de la solución IzumuClientes. 
 

Componentes del Proyecto 

1.	Base de Datos SQL Server: Maneja la persistencia de los datos de los clientes: en el repositorio se encuentran alojados en la carpeta con nombre: 01. Scripts_BD.zip, descomprimir y ejecutar los scripts que son necesarios para la ejecución del proyecto. 

2.	Proyecto Microservice_Izumu: Microservicio en (Back-end).NET Core 8: Gestiona la lógica de negocio y expone operaciones CRUD de un servicio. est proyecto se encuentra en Microservice_Izumu.zip descomprimir y adicionar a la solución.

3. Proyecto IzumuClientes: 	Interfaz Gráfica – (Front-end) ASP.NET Core Razor Pages: Proporciona una interfaz de usuario para interactuar con el sistema, comunicándose con el microservicio para la gestión de clientes.
   
4. Proyecto MicroService_Izumu.Test: proyecto de pruebas unitarias con Nunit.
 

Requisitos Previos: 

Antes de proceder con la instalación, asegurarse de cumplir con los siguientes requisitos: 

•	.NET SDK 8 instalado. 

•	SQL Server 2022 y SQL Server Management Studio (SSMS) versión 20.2. 

•	Visual Studio 2022 (opcional, pero recomendado para un desarrollo más eficiente). 

•	Docker, para ejecutar el microservicio en un contenedor. 

 

 

Procedimiento de Instalación 

 

A. Configuración de la Base de Datos 

  1.  Iniciar SQL Server y crear una nueva base de datos denominada DB_IZUMU. 

  2.  En SQL Server Management Studio, ejecutar los scripts SQL ubicados en el archivo 01. Scripts_BD.zip para generar las tablas necesarias y procedimientos requeridos para  ejecutar la solución. 

B. Configuración de la solución (IzumuClientes) 

  1.	Abrir el proyecto en Visual Studio o en su editor de código preferido. 
  
  2.	En el proyecto Microservice_Izumu Configurar la cadena de conexión a la base de datos en el archivo appsettings.json, reemplazando los valores de servidor, usuario y contraseña por los correspondientes a su entorno: 
        
      "ConnectionStrings":  
      { 
      "Bd_Izumu_Cx":"DataSource=DESKTOP-E4LKU9R;Initial Catalog=DB_IZUMU;Integrated Security=true;MultipleActiveResultSets=True;Application Name=IZUMU;connection timeout=800;TrustServerCertificate=True;Encrypt=True;" 
      } 
Los datos que se modificarán son nombre del servidor, atributos o propiedades (usuario y contraseña) dependiendo el motor de bd donde se ejecute 

 
3. En el proyecto IzumuClientes verificar la url del microservicio en el archivo appsettings.json. 
