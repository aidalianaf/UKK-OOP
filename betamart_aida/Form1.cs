using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace betamart_aida
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    this.tokoTableAdapter.Fill(this.appData.toko);
                    tokoBindingSource.DataSource = this.appData.toko;
                }
                else
                {
                    var query = from o in this.appData.toko
                                where o.namaBarang.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                || o.ukuran == txtSearch.Text
                                || o.stokBarang == txtSearch.Text
                                || o.hargaJual.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0
                                select o;
                    tokoBindingSource.DataSource = query.ToList();
                } 
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    tokoBindingSource.RemoveCurrent();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {

                //ini berarti diisi button search coba gih cari da 

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {

            try
            {
                
                panel1.Enabled = true;
                txtNamaBarang.Focus();
                this.appData.toko.AddtokoRow(this.appData.toko.NewtokoRow());
                tokoBindingSource.MoveLast();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tokoBindingSource.ResetBindings(false);

            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           
            panel1.Enabled = true;
            txtNamaBarang.Focus();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        
        {
            if (MessageBox.Show("Yakin Ingin Menghapus Produk Ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tokoBindingSource.RemoveCurrent();
                tokoTableAdapter.Update(this.appData.toko);
                tokoBindingSource.EndEdit();
                tokoBindingSource.ResetBindings(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {

                tokoBindingSource.EndEdit();
                tokoTableAdapter.Update(this.appData.toko);
                panel1.Enabled = false;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tokoBindingSource.ResetBindings(false);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            // TODO: This line of code loads data into the 'appData.toko' table. You can move, or remove it, as needed.
            this.tokoTableAdapter.Fill(this.appData.toko);
            tokoBindingSource.DataSource = this.appData.toko;

        }

        private void tokoBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
