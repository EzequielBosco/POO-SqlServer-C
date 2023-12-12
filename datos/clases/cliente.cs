using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace clases
{
  class Cliente
  {
    private DB_Conexion conexion = new DB_Conexion();
    SqlDataReader reader;
    DataTable tabla = new DataTable();
    SqlCommand query = new SqlCommand();
    public DataTable Mostrar() {
      try
      {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT c.nombre, c.apellido, c.mail, c.id_provincia, p.provincia AS provincia " +
                            "FROM CLIENTES c " +
                            "INNER JOIN PROVINCIAS p ON c.id_provincia = p.id_provincia";
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

    public void Insertar(string nombre, string apellido, string mail, int provincia) {
      query.Connection = conexion.AbrirConexion();

      if (!ClienteExiste(mail))
      {
        query.CommandText = $"INSERT INTO CLIENTES VALUES('" + nombre + "', '" + apellido + "', '" + mail + "', '" + provincia + "')";
        query.ExecuteNonQuery();
        
        Console.WriteLine("Cliente cargado correctamente");
      }
      else
      {
        Console.WriteLine("El correo electrónico ya existe en la base de datos.");
      }
    }

    private bool ClienteExiste(string mail)
    {
      query.CommandText = $"SELECT COUNT(*) FROM CLIENTES WHERE MAIL = '{mail}'";
      int count = (int)query.ExecuteScalar();
      return count > 0;
    }
    public void MostrarDatos()
    {
      DataTable tablaClientes = Mostrar();

      foreach (DataRow row in tablaClientes.Rows)
      {
        Console.WriteLine($"Cliente: {row["NOMBRE"]} - {row["APELLIDO"]} - {row["MAIL"]} - {row["provincia"]}");
      }
    }
  }
}