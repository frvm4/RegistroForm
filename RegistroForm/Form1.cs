using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO; //librería para leer archivos txt
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroForm
{
    public partial class Form1 : Form
    {
        string SQLConection = "Server=localhost; Port=3306; " +
            "Database=Formulario; Uid=root; Pwd=root";
        public Form1()
        {
            InitializeComponent();
            //Manejadores de evento, declaración
            txtEdad.TextChanged += ValidarEdad;
            txtApellido.TextChanged += ValidarApellidos;
            txtNombre.TextChanged += ValidarNombre;
            txtEstatura.TextChanged += ValidarEstatura;
            txtTelefono.Leave += ValidarTelefono;
        }
        private void insertarRegistros(string nombre, string apellidos, int edad,
            decimal estatura, string telefono, string genero)
        {
            using (MySqlConnection conectar = new MySqlConnection(SQLConection))
            {
                conectar.Open();
                string insertQuery = "INSERT INTO registros (nombres, " +
                    "apellidos, telefono, estatura, edad, genero) " +
                    "VALUES (@nombres, @apellidos,@telefono,@estatura," +
                    "@edad, @genero)";
                using (MySqlCommand cmd = new MySqlCommand(insertQuery, conectar))
                {
                    cmd.Parameters.AddWithValue("@nombres", nombre);
                    cmd.Parameters.AddWithValue("@apellidos",
                    apellidos);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@estatura", estatura);
                    cmd.Parameters.AddWithValue("@edad", edad);
                    cmd.Parameters.AddWithValue(" @genero", genero);
                    cmd.ExecuteNonQuery();
                }
                conectar.Close();
            }

        }
        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox cajaNombre = (TextBox)sender;
            if (!EsTextoValido(cajaNombre.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el Nombre",
                    "Error Nombre", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cajaNombre.Clear();
            }
        }
        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox cajaApellidos = (TextBox)sender;
            if (!EsTextoValido(cajaApellidos.Text))
            {
                MessageBox.Show("Ingrese valores correctos para los Apellidos",
                    "Error Apellidos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cajaApellidos.Clear();
            }
        }
        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox cajaEdad = (TextBox)sender;
            if (!EsEnteroValido(cajaEdad.Text))
            {
                MessageBox.Show("Ingrese valores correctos para la Edad",
                    "Error Edad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cajaEdad.Clear();
            }
        }
        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox cajaEstatura = (TextBox)sender;
            if (!EsFlotanteValido(cajaEstatura.Text))
            {
                MessageBox.Show("Ingrese valores correctos para la Estatura",
                    "Error Estatura", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cajaEstatura.Clear();
            }
        }
        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string input = textBox.Text;
            if (input.Length > 10)
            {
                if (!EsEnteroValidoDe10Digitos(input))
                {
                    textBox.BackColor = Color.Red;
                }
            }
            else if (!EsEnteroValidoDe10Digitos(input))
            {
                textBox.BackColor = Color.Yellow;
            }
            else
            {
                textBox.BackColor = Color.SeaGreen;
            }
        }
        private bool EsTextoValido(string valor) { return Regex.IsMatch(valor, @"^[a-zA-Z\s]+$"); }
        private bool EsEnteroValido(string valor)
        {   int resultado;
            return int.TryParse(valor, out resultado);
        }
        private bool EsFlotanteValido(string valor)
        {   float resultado;
            return float.TryParse(valor, out resultado);}
        private bool EsEnteroValidoDe10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado) && valor.Length ==
            10;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text;
            string apellidos = txtApellido.Text;
            string edad = txtEdad.Text;
            string estatura = txtEstatura.Text;
            string telefono = txtTelefono.Text;
            string genero = "";
            if (rbHombre.Checked) { genero = "Hombre"; }
            else if (rbMujer.Checked) { genero = "Mujer"; }

            if (EsEnteroValido(edad) && EsFlotanteValido(estatura) &&
                EsEnteroValidoDe10Digitos(telefono) && EsTextoValido(nombre) &&
                EsTextoValido(apellidos))
            {
                string datos = $"Nombres: {nombre}\r\n" +
                    $"Apellidos: {apellidos}\r\n" +
                    $"Edad: {edad}\r\n" +
                    $"Teléfono: {telefono}\r\n" +
                    $"Generooo: {genero}\r\n";

                string rutaArchivo = "D:\\aaa_UNACH\\programación\\txt\\Registro";
                bool archivoExiste = File.Exists(rutaArchivo);
                if (archivoExiste == false) { File.WriteAllText(rutaArchivo, datos); }
                else {
                    using (StreamWriter escritor = new StreamWriter(rutaArchivo, true))
                    {
                        if (archivoExiste) { escritor.WriteLine(); }
                        escritor.WriteLine(datos);
                        insertarRegistros(nombre, apellidos, int.Parse(edad), decimal.Parse(estatura),
                            telefono, genero);
                        MessageBox.Show($"Datos Insertados en la Base de Datos: \n\n {datos}",
                            "Información BD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtEdad.Clear();
            txtEstatura.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            rbHombre.Checked = false;
            rbMujer.Checked = false;
        }
    }
}
