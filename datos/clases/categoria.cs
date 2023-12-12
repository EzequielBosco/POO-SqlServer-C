using System.Data;
using System.Data.SqlClient;

namespace clases
{
  class Categoria
  {
    private DB_Conexion conexion = new DB_Conexion();
    SqlDataReader reader;
    DataTable tabla = new DataTable();
    SqlCommand query = new SqlCommand();
    public DataTable Mostrar() {
      try
      {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT * FROM CATEGORIA";
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

    public void Insertar(string nombre_categoria) {
      query.Connection = conexion.AbrirConexion();
      query.CommandText = $"INSERT INTO CATEGORIA VALUES('" + nombre_categoria + "')";
      query.ExecuteNonQuery();

      Console.WriteLine("Categoría cargada correctamente");
    }
    public void MostrarDatos()
    {
      DataTable tablaCategorias = Mostrar();

      foreach (DataRow row in tablaCategorias.Rows)
      {
        Console.WriteLine($"Categoria: {row["NOMBRE_CATEGORIA"]}");
      }
    }
  }
}