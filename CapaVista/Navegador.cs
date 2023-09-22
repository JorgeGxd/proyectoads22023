﻿using CapaControlador;
using CapaVista.Componentes.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class Navegador : UserControl
    {
        private utilidadesConsultasI utilConsultasI;
        public string operacion = "";
        public string tabla = "";

        public Form parent;
        public Navegador()
        {
            InitializeComponent();
            this.parent = new Form();
            this.utilConsultasI = new utilidadesConsultasI();
            this.cambiarEstado(false);
        }

        public void config(string tabla, Form parent)
        {
            this.tabla = tabla;
            this.parent = parent;
            this.utilConsultasI.setTabla(this.tabla);
        }


        public void identificarFormulario(Form child, string operacion)
        {
            
           DataGridView dgvname = GetDGV(child);

            if (operacion.Equals("g")) this.utilConsultasI.guardar(child);
            if (operacion.Equals("m")) this.utilConsultasI.modificar(child); ;
        }



        public  DataGridView GetDGV(Form child) { 
         
             foreach (Control c in child.Controls)
             {
          

                 if (c is DataGridView dgv)
                 {

                    return dgv;
                 }

             }return null;
             throw new Exception("No se encontró un DataGridView en elasdas formulario.");
         }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            this.identificarFormulario(this.parent, this.operacion);
            this.cambiarEstado(false);
        }

        public void cambiarEstado(bool estado)
        {
            foreach (Control control in this.panel.Controls)
            {
                if (control is Button)
                {
                    Button btn = (Button)control;
                    if (btn.Name.Equals("btn_guardar") || btn.Name.Equals("btn_cancelar"))
                    {
                        btn.Enabled = estado;
                    }
                    else
                    {
                        btn.Enabled = !estado;
                    }
                }
            }

        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            this.cambiarEstado(true);
            this.operacion = "a";
        }

        public void limpiarControles()
        {
            foreach (Control control in this.parent.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Clear();
                }
                else if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Value = DateTime.Now;
                }
                else if (control is ComboBox)
                {
                    ((ComboBox)control).SelectedIndex = 0;
                }
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.limpiarControles();
            this.cambiarEstado(false);
        }

        private void btn_ayuda_Click_1(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Ayudas/AyudaSO2.chm", "NavAyuda.html");
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("boton de modificar xd");
            this.utilConsultasI.cargarModificar(this.parent,GetDGV(this.parent));
            this.operacion = "m";
            this.cambiarEstado(true);
      
            
        }
    }
}
