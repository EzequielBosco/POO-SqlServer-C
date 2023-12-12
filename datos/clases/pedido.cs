using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace clases
{
  class Pedido
  {
    private DB_Conexion conexion = new DB_Conexion();
    SqlDataReader reader;
    DataTable tabla = new DataTable();
    SqlCommand query = new SqlCommand();
    public DataTable Mostrar() {
      try
      {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT p.fecha, p.stotal, p.iva, p.total, pr.id_producto AS producto, c.nombre AS nombre_cliente " +
                            "FROM PEDIDOS p " +
                            "INNER JOIN CLIENTES c ON p.id_cliente = c.id_cliente " +
                            "INNER JOIN PRODUCTO pr ON p.id_producto = pr.id_producto";
        reader = query.ExecuteReader();
        tabla.Load(reader);
        return tabla;
      } catch (Exception ex) {
        throw new Exception("Error:", ex);
      }
      finally
      {
        conexion.CerrarConexion();
      }
    }

    public void Insertar(int stotal, int iva, int total, int id_cliente, int id_producto) {
      query.Connection = conexion.AbrirConexion();
      DateTime fecha = DateTime.Now;
      query.CommandText = $"INSERT INTO pedidos VALUES('{fecha.ToString("yyyy-MM-dd HH:mm:ss")}', '" + stotal + "', '" + iva + "', '" + total + "', '" + id_cliente + "', '" + id_producto + "')";
      query.ExecuteNonQuery();

      Console.WriteLine("Pedido cargado correctamente");
    }
    
    public void MostrarDatos()
    {
      DataTable tablaPedidos = Mostrar();

      foreach (DataRow row in tablaPedidos.Rows)
      {
        Console.WriteLine($"Pedido: {row["fecha"]} - {row["stotal"]} - {row["iva"]} - {row["total"]} - {row["nombre_cliente"]} - {row["producto"]}");
      }
    }
  }
}