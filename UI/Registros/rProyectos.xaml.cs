using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RaldyP2Apli.BLL;
using RaldyP2Apli.Entidades;

namespace RaldyP2Apli.UI.Registros
{
    public partial class rProyectos: Window
    {
        private Proyectos proyectos = new Proyectos();
        public rProyectos()
        {
            InitializeComponent();
            this.DataContext = proyectos;

            //****************ComboBox de Tipotarea
            TipoTareaComboBox.ItemsSource = TareasBLL.GetList(s => true);
            TipoTareaComboBox.SelectedValuePath = "TareaId";
            TipoTareaComboBox.DisplayMemberPath = "TipoTarea";
           
        }
        // ****************Funcion Cagar
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = proyectos;
        }
        //****************Funcion Limpiar
        private void Limpiar()
        {
            this.proyectos = new Proyectos();
            this.DataContext = proyectos;
        }
        //****************Funcion Validar
        private bool Validar()
        {
            bool esValidado = true;
            if (DescripcionTextBox.Text.Length == 0)
            {
                esValidado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if(DescripcionTextBox.Text.Trim() == String.Empty){
                    MessageBox.Show("No puede dejar el campo Descripcion vacio", "Error", MessageBoxButton.OK , MessageBoxImage.Error);
                   
            }

            return esValidado;
        }
        //****************Boton Buscar
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Proyectos encontrado = ProyectosBLL.Buscar(proyectos.ProyectoId);

            if (encontrado != null)
            {
                proyectos = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Este pedido no fue encontrado.\n\nAsegurese que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                ProyectoIdTextBox.Clear();
                ProyectoIdTextBox.Focus();
            }
        }
        
        //****************Boton Nuevo
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //****************Boton Guardar
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                var paso = ProyectosBLL.Guardar(this.proyectos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Errada", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //****************Boton Eliminar
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (ProyectosBLL.Eliminar(Utilidades.ToInt(ProyectoIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

         //**********************Boton de Agregar Fila
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            var filaDetalle = new ProyectosDetalle
            {
                ProyectoId = this.proyectos.ProyectoId,
                TareaId = Convert.ToInt32(TipoTareaComboBox.SelectedValue.ToString()),
                Tipo = ((Tareas)TipoTareaComboBox.SelectedItem),
                Requerimiento = (RequerimientoTextBox.Text),
                Tiempo = Convert.ToSingle(TiempoTextBox.Text)
            };
            
            proyectos.TiempoTotal += Convert.ToDouble(TiempoTextBox.Text.ToString());
            
            this.proyectos.Detalle.Add(filaDetalle);
            Cargar();

            TipoTareaComboBox.SelectedIndex = -1;
            RequerimientoTextBox.Clear();
            TiempoTextBox.Clear();
        }
        //**********************Boton de Eliminar Fila

        private void EliminarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try

            {
                var detalle = (ProyectosDetalle)DetalleDataGrid.SelectedItem;
                double total = Convert.ToDouble(TiempoTotalTextBox.Text);
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    proyectos.TiempoTotal = proyectos.TiempoTotal - detalle.Tiempo;
                    proyectos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                   
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
    }
}