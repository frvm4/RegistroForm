using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO; //librería para leer archivos txt
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            string datos = $"Nombres: {nombre}\r\n" +
                $"Apellidos: {apellidos}\r\n" +
                $"Edad: {edad}\r\n" +
                $"Teléfono: {telefono}\r\n" +
                $"Genero: {genero}\r\n";

            string rutaArchivo = "D:\\aaa_UNACH\\programación\\txt\\Registro";
            bool archivoExiste = File.Exists(rutaArchivo);
            using (StreamWriter escritor = new StreamWriter(rutaArchivo, true))
            {
                if (archivoExiste) { escritor.WriteLine(); }
                escritor.WriteLine(datos);
            }
            MessageBox.Show($"Datos Guardados Correctamente: \r\n {datos}", "Información - Actividad 04", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
