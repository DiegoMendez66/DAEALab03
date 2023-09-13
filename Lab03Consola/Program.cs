//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using System.Reflection.PortableExecutable;
using Lab03Consola;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-18\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";


    static void Main()
    {

        #region FormaDesconectada
        //Datatable
        DataTable dataTable = ListarEstudiantesDataTable();


        Console.WriteLine("Lista de Estudiantes DataTable:");
        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine($"ID: {row["StudentID"]}, Nombre: {row["firstname"]}, Cargo: {row["lastname"]}");
        }
        #endregion




        #region FormaConectada
        //Datareader
        List<Student> estudiantes = ListarEstudiantesListaObjetos();
        foreach (var item in estudiantes)
        {
            Console.WriteLine($"ID: {item.StudentId}, Nombre: {item.firstname}, Cargo: {item.lastname}");
        }
        #endregion


    }

    //De forma desconectada
    private static DataTable ListarEstudiantesDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Students";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }
    //De forma conectada
    private static List<Student> ListarEstudiantesListaObjetos()
    {
        List<Student> estudiantes = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT StudentID,firstname,lastname FROM Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        Console.WriteLine("\nLista de Estudiantes DataReader:");
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            estudiantes.Add(new Student
                            {
                                StudentId = (int)reader["StudentID"],
                                firstname = reader["firstname"].ToString(),
                                lastname = reader["lastname"].ToString()
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();

        }
        return estudiantes;

    }

}
