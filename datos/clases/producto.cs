using System.Data;
using System.Data.SqlClient;

namespace clases
{
  class Producto
  {
    private DB_Conexion conexion = new DB_Conexion();
    SqlDataReader reader;
    DataTable tabla = new DataTable();
    SqlCommand query = new SqlCommand();
    public DataTable Mostrar() {
      try
      {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT p.descripcion, p.precio, c.nombre_categoria AS categoria " +
                            "FROM PRODUCTO p " +
                            "INNER JOIN CATEGORIA c ON p.id_categoria = c.id_categoria";
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

    public void Insertar(string descripcion, int id_categoria, int precio, string nombre) {
      query.Connection = conexion.AbrirConexion();
      query.CommandText = $"INSERT INTO producto VALUES('" + descripcion + "', '" + id_categoria + "', '" + precio + "', '" + nombre + "')";
      query.ExecuteNonQuery();

      Console.WriteLine("Producto cargado correctamente");
    }
    public void MostrarDatos()
    {
      DataTable tablaProductos = Mostrar();

      foreach (DataRow row in tablaProductos.Rows)
      {
        Console.WriteLine($"Producto: {row["descripcion"]} - {row["categoria"]} - {row["precio"]}");
      }
    }
  }
}