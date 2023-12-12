using System.Data;
using System.Data.SqlClient;

namespace clases
{
  class Provincia
  {
    private DB_Conexion conexion = new DB_Conexion();
    SqlDataReader reader;
    DataTable tabla = new DataTable();
    SqlCommand query = new SqlCommand();
    public DataTable Mostrar() {
      try
      {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT * FROM PROVINCIAS";
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

    public void Insertar(string provincia) {
      query.Connection = conexion.AbrirConexion();
      query.CommandText = $"INSERT INTO provincias VALUES('" + provincia + "')";
      query.ExecuteNonQuery();

      Console.WriteLine("Provincia cargada correctamente");
    }
    public void MostrarDatos()
    {
      DataTable tablaProvincias = Mostrar();

      foreach (DataRow row in tablaProvincias.Rows)
      {
        Console.WriteLine($"Provincia: {row["provincia"]}");
      }
    }
  }
}