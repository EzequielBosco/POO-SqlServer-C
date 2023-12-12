using System.Data;
using System.Data.SqlClient;

namespace clases
{
  class Detalle_pedido
  {
    private DB_Conexion conexion = new DB_Conexion();
    SqlDataReader reader;
    DataTable tabla = new DataTable();
    SqlCommand query = new SqlCommand();
    public DataTable Mostrar() {
      try
      {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT p.id_pedido, p.id_producto, pr.nombre AS nombre_producto, p.cant " +
                            "FROM DETALLE_PEDIDO p " +
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

    public void Insertar(int id_pedido, int id_producto, int cant) {
      query.Connection = conexion.AbrirConexion();
      query.CommandText = $"INSERT INTO detalle_pedido VALUES('" + id_pedido + "', '" + id_producto + "', '" + cant + "')";
      query.ExecuteNonQuery();

      Console.WriteLine("Detalle de pedido cargado correctamente");
    }
    public void MostrarDatos()
    {
      DataTable tablaDetalles = Mostrar();

      foreach (DataRow row in tablaDetalles.Rows)
      {
        Console.WriteLine($"Detalle pedido: {row["id_pedido"]} - {row["nombre_producto"]} - {row["cant"]}");
      }
    }
  }
}