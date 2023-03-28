using GestionEtudiants.DSTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace GestionEtudiants
{
    public partial class Form1 : Form
    {
        StudentTableAdapter ada = new StudentTableAdapter();    
        public Form1()
        {
            InitializeComponent();
        }
        private void btnAfficher_Click(object sender, EventArgs e)
        {
            grd.DataSource= ada.GetData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ada.Insert(txtNom.Text, int.Parse(txtAge.Text));
                grd.DataSource = ada.GetData();
                txtNom.Text = "";
                txtAge.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        private void grd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int numeroLigne;
            numeroLigne = e.RowIndex;
            DataGridViewRow ligneSelectionne = grd.Rows[numeroLigne];
            txtNom.Text = ligneSelectionne.Cells[1].Value.ToString();
            txtAge.Text = ligneSelectionne.Cells[2].Value.ToString();
            IdTxt.Text = ligneSelectionne.Cells[0].Value.ToString();
            oldName.Text = ligneSelectionne.Cells[1].Value.ToString();
            oldAge.Text = ligneSelectionne.Cells[2].Value.ToString();


        }
        private void modifier_Click(object sender, EventArgs e)
        {

            ada.Update(txtNom.Text, int.Parse(txtAge.Text),int.Parse(IdTxt.Text),oldName.Text,int.Parse(oldAge.Text));
            grd.DataSource = ada.GetData();
            txtNom.Text = "";
            txtAge.Text = "";
        }

        private void txtNom_TextChanged(object sender, EventArgs e)
        {

        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            try
            {
                ada.Delete(int.Parse(IdTxt.Text), txtNom.Text, int.Parse(txtAge.Text));
                txtNom.Text = "";
                txtAge.Text = "";
                grd.DataSource = ada.GetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

    }
        Bitmap bitmap;
        private void Imprimer_Click(object sender, EventArgs e)
        {
            int longeur = grd.Height;
            grd.Height = grd.RowCount * grd.RowTemplate.Height * 2;
            bitmap = new Bitmap(grd.Width, grd.Height);
            grd.DrawToBitmap(bitmap, new Rectangle(0, 0, grd.Width, grd.Height));
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            grd.Height = longeur;

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
