﻿using System.Data;
using System.Data.SqlClient;
using DotNetEnv;
using clases;

namespace Datos
{
  class Program
  {
    static void Main(string[] args)
    {
      DotNetEnv.Env.Load();

      Provincia nuevaProvincia = new Provincia();
      nuevaProvincia.Insertar("Buenos Aires");
      nuevaProvincia.MostrarDatos();

      Cliente nuevoCliente = new Cliente();
      nuevoCliente.Insertar("NuevoCliente", "NuevoApellido", "nuevo@prueba.com", 2);
      nuevoCliente.MostrarDatos();

      Categoria nuevaCategoria = new Categoria();
      nuevaCategoria.Insertar("Consolas");
      nuevaCategoria.MostrarDatos();

      Producto nuevoProducto = new Producto();
      nuevoProducto.Insertar("Potencia de ultima generacion", 4, 1000000, "Ps5");
      nuevoProducto.MostrarDatos();

      Pedido nuevoPedido = new Pedido();
      nuevoPedido.Insertar(100000, 21, 120000, 8, 5);
      nuevoPedido.MostrarDatos();

      Detalle_pedido nuevoDetalle = new Detalle_pedido();
      nuevoDetalle.Insertar(7, 5, 80);
      nuevoDetalle.MostrarDatos();
    }
  }
}

public class DB_Conexion
{
  string db_server = Environment.GetEnvironmentVariable("db_server");
  private SqlConnection Conexion;
  private static bool mensajeBD = false;

  public DB_Conexion()
  {
    Conexion = new SqlConnection($"Server={db_server};DataBase=Final;Integrated Security=true");
  }
  
  public SqlConnection AbrirConexion()
  {
    if (Conexion.State == ConnectionState.Closed)
    {
      Conexion.Open(); 
      if (!mensajeBD)
      {
        Console.WriteLine("Conexión a BD con exito");
        mensajeBD = true;
      } 
    }
    return Conexion;
  }

  public SqlConnection CerrarConexion()
  {
    if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
    return Conexion;
  }

}
