using Registro_Roles.BLL;
using Registro_Roles.DAL;
using Registro_Roles.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro_Roles
{
    public partial class rPermisos : Form
    {
        public rPermisos()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            IdnumericUpDown1.Value = 0;
            txt_descripcion.Text = string.Empty;
            errorProvider1.Clear();
        }

        private void LlenaCampo(Roles rol)
        {
            IdnumericUpDown1.Value = rol.RolId;
            txt_descripcion.Text = rol.Descripcion;
        }

        private Roles LlenaClase()
        {
            Roles rol = new Roles();
            rol.RolId = Convert.ToInt32(IdnumericUpDown1.Value);
            rol.Descripcion = txt_descripcion.Text;

            return rol;
        }
        private bool ExisteEnLaBaseDeDatos()
        {
            Roles rol = RolesBLL.Buscar((int)IdnumericUpDown1.Value);

            return (rol != null);
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            Roles rol;
            bool paso = false;

            if (!Validar())
                return;

            rol = LlenaClase();

            if (IdnumericUpDown1.Value == 0)
                paso = RolesBLL.Guardar(rol);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puede modificar", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = RolesBLL.Modificar(rol);
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!!", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No fue posible guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool Validar()
        {
            bool paso = true;
            errorProvider1.Clear();
            if(txt_descripcion.Text == string.Empty)
            {
                errorProvider1.SetError(txt_descripcion, "El campo descripcion no puede estar vacio");
                txt_descripcion.Focus();
                paso = false;
            }
            return paso;
        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();

            int id;
            int.TryParse(IdnumericUpDown1.Text, out id);

            Limpiar();

            if (RolesBLL.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                errorProvider1.SetError(IdnumericUpDown1, "Rol no existe");
        }

        private void btn_buscar_Click_1(object sender, EventArgs e)
        {
            int id;
            Roles rol = new Roles();
            int.TryParse(IdnumericUpDown1.Text, out id);

            Limpiar();

            rol = RolesBLL.Buscar(id);

            if(rol != null)
            {
                MessageBox.Show("Rol encontrado");
                LlenaCampo(rol);
            }
            else
            {
                MessageBox.Show("Rol no encontrado");
            }
        }

    }
}
