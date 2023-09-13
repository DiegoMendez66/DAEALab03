using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace Lab03WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string connectionString = "Data Source=LAB1504-18\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";
        public MainWindow()
        {
            InitializeComponent();
            McDataGrid.ItemsSource = ListarEstudiantesListaObjetos();

        }

        private static List<Student> ListarEstudiantesListaObjetos()
        {
            List<Student> estudiantes = new List<Student>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Consulta SQL para seleccionar datos
                string query = "SELECT StudentID,firstname,lastname FROM Students";

                if (true)
                {
                    
                }

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

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}