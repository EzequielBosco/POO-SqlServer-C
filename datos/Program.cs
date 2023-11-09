using System.Data;
using System.Data.SqlClient;
using DotNetEnv;

namespace Datos
{
  class Program
  {
      static void Main(string[] args)
      {
        DotNetEnv.Env.Load();
        D_Clientes dClientes = new D_Clientes();
        DataTable tablaClientes = dClientes.Mostrar();

        foreach (DataRow row in tablaClientes.Rows)
        {
          Console.WriteLine($"{row["NOMBRE"]} - {row["APELLIDO"]}");
        }

        Console.ReadLine();
      }
    }
  }

  public class D_Clientes {
  private DB_Conexion conexion = new DB_Conexion();
  SqlDataReader reader;
  DataTable tabla = new DataTable();
  SqlCommand query = new SqlCommand();

  public DataTable Mostrar() {
    try
    {
        query.Connection = conexion.AbrirConexion();
        query.CommandText = "SELECT * FROM CLIENTES";
        reader = query.ExecuteReader();
        tabla.Load(reader);
        return tabla;
    }
    finally
    {
        conexion.CerrarConexion();
    }
  }
  
  public class DB_Conexion
  {
    string db_server = Environment.GetEnvironmentVariable("db_server");
    private SqlConnection Conexion;

    public DB_Conexion()
    {
        Conexion = new SqlConnection($"Server={db_server};DataBase=Final;Integrated Security=true");
    }
    
    public SqlConnection AbrirConexion()
    {
      if (Conexion.State == ConnectionState.Closed) { Conexion.Open(); Console.WriteLine("db is connected"); }
      return Conexion;
    }

    public SqlConnection CerrarConexion()
    {
      if (Conexion.State == ConnectionState.Open) { Conexion.Close(); }
      return Conexion;
    }

  }
}
