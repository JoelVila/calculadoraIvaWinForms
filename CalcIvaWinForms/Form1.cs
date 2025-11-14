using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalcIvaWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.Gray;
            // Llama al método de inicialización del ComboBox
            Form1_Load(null, null);
        }

        // Inicializa el ComboBox con las tasas de IVA al cargar el formulario
        private void Form1_Load(object sender, EventArgs e)
        {
            // Asumiendo que el ComboBox se llama comboBox1
            comboBox1.Items.Add("4");
            comboBox1.Items.Add("10");
            comboBox1.Items.Add("21");
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 2; // Selecciona 21 por defecto
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                decimal precioUnidad;
                int cantidad;
                decimal tipoIVA;

                // Inicialización de descuentoPorcentaje a 0m
                decimal descuentoPorcentaje = 0m;

                // --- 1️⃣ Validar y convertir cada campo ---

                // Precio por unidad (txtBox_precioUnidad)
                if (!decimal.TryParse(txtBox_precioUnidad.Text, out precioUnidad) || precioUnidad < 0)
                    throw new Exception("Por favor ingresa un precio válido en 'Precio por unidad'.");

                // Cantidad de productos (txtBox_cantidadProductos)
                if (!int.TryParse(txtBox_cantidadProductos.Text, out cantidad) || cantidad < 0)
                    throw new Exception("Por favor ingresa una cantidad válida en 'Cantidad de productos'.");

                // Descuento (%) - CORREGIDO PARA MANEJAR CAMPOS VACÍOS (asumiendo textBox3)
                string descuentoTexto = textBox3.Text;
                if (!string.IsNullOrWhiteSpace(descuentoTexto))
                {
                    if (!decimal.TryParse(descuentoTexto, out descuentoPorcentaje))
                    {
                        throw new Exception("El porcentaje de descuento no es válido (ingresa solo números).");
                    }
                }

                // Validación de rango (tanto si es 0 como si fue parseado)
                if (descuentoPorcentaje < 0 || descuentoPorcentaje > 100)
                {
                    throw new Exception("El porcentaje de descuento no es válido (debe ser 0-100).");
                }

                // Tipo de IVA (comboBox1)
                if (comboBox1.SelectedItem == null || !decimal.TryParse(comboBox1.SelectedItem.ToString(), out tipoIVA) || tipoIVA < 0)
                    throw new Exception("Selecciona un tipo de IVA válido.");

                bool esSocio = checkBox1.Checked;
                const decimal descuentoSocio = 0.05m;

                // --- 2️⃣ Calcular totales ---
                decimal subtotalBruto = precioUnidad * cantidad;
                decimal montoDescuentoBase = subtotalBruto * (descuentoPorcentaje / 100m);
                decimal subtotalDescontadoBase = subtotalBruto - montoDescuentoBase;
                decimal montoDescuentoSocio = esSocio ? subtotalDescontadoBase * descuentoSocio : 0m;

                decimal totalSinIVA = subtotalDescontadoBase - montoDescuentoSocio;

                decimal factorIVA = tipoIVA / 100m;
                decimal totalConIVA = totalSinIVA * (1m + factorIVA);


                // --- 3️⃣ Asignación de Resultados (usando textBox4 y textBox5) ---

                // NO IVA
                textBox4.Text = totalSinIVA.ToString("0.00") + " €";

                // CON IVA
                textBox5.Text = totalConIVA.ToString("0.00") + " €";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error de Cálculo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Limpiar los campos de resultado en caso de error
                textBox4.Text = "";
                textBox5.Text = "";
            }
        }
        // Se dejan aquí para evitar que el diseñador de Visual Studio se rompa.
        private void label1_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Este método ahora está vacío ya que la lógica principal está en button1_Click
        }

        private void label2_Click_1(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Este método ahora está vacío ya que la lógica principal está en button1_Click
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
    }
}