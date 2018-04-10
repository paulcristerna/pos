-- Crear base de datos
CREATE DATABASE MiniSuper;
-- Usar base de datos
USE MiniSuper;

-- Crear Tablas
create table Usuarios (
  Nombre varchar (25) NOT NULL,
  Apellido varchar (25) NOT NULL,
  Direccion varchar (50) NOT NULL,
  Usuario varchar (10) NOT NULL PRIMARY KEY,
  Contrasena varchar (8) NOT NULL,
  Tipo varchar (15) NOT NULL
);

create table Productos (
  Codigo bigint NOT NULL PRIMARY KEY,
  Nombre varchar (20) NOT NULL,
  Descripcion varchar (35) NOT NULL,
  Precio float (10) NOT NULL,
  Stock float (10,3) NOT NULL,
  TipoUnidad varchar(25) NOT NULL
);

create table Ventas (
  IdVenta bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Cliente VARCHAR(20),
  Fecha TIMESTAMP DEFAULT CURRENT_TIMESTAMP NOT NULL,
  Total float (10,2) NOT NULL,
  PagoCon float (10,2) NOT NULL,
  Cambio float (10,2) NOT NULL,
  Estado char(1) DEFAULT 'A' NOT NULL
);

create table Detalle_Ventas (
  IdDetalle bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Codigo bigint,
  Cantidad float (10,3) NOT NULL,
  PrecioUnitario float (10) NOT NULL
);

create table Clientes (
  Id_Cliente bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Nombre VARCHAR (20) NOT NULL,
  Apellido VARCHAR (20) NOT NULL,
  Direccion VARCHAR (50) NOT NULL,
  TelefonoCasa VARCHAR (10) NOT NULL,
  Celular VARCHAR (10) NOT NULL,
  Email VARCHAR (30) NOT NULL,
  FechaNacimiento DATE NOT NULL
);

create table Proveedores (
  Id_Proveedor bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
  Nombre VARCHAR (20) NOT NULL,
  Apellido VARCHAR (20) NOT NULL,
  Razon_Social VARCHAR (30) NOT NULL,
  Direccion VARCHAR (50) NOT NULL,
  TelefonoOficina VARCHAR (10) NOT NULL,
  Celular VARCHAR (10) NOT NULL,
  Email VARCHAR (30) NOT NULL
);

create table Venta_Proceso (
  Codigo bigint NOT NULL,
  Nombre VARCHAR (20) NOT NULL,
  Descripcion VARCHAR (35) NOT NULL,
  TipoUnidad varchar(25) NOT NULL,
  PrecioUnitario float (10) NOT NULL,
  Cantidad float (10) NOT NULL,
  Importe float (10) NOT NULL
);

select * from Venta_Proceso;

DROP TABLE Venta_Proceso;

SET SQL_SAFE_UPDATES = 0;

delete from Venta_Proceso;


UPDATE Venta_Proceso
SET Cantidad = '2', Importe = '100'
WHERE Codigo = '1';


DELETE FROM Venta_Proceso where 1;

DELETE FROM Venta_Proceso
WHERE Id_Cadena BETWEEN 32 AND 34;

delete from Cadenas where Id_Cadena = 31;

-- Crear usuario default
insert into Usuarios values ('Hector','Inzunza Russell','Conocido #123','Admin', '123', 'Administrador');

select * from ventas;
select * from productos;
select * from detalle_ventas;