using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace BetaMart
{
    public partial class BetaMart : Form
    {
        OleDbConnection oleDbConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\lucyp\OneDrive\Documents\BetaMart.accdb");
        int id_edit_barang;
        public BetaMart()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string nama_barang = textBox2.Text;
            int harga_barang = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            int jumlah_barang = Convert.ToInt32(Math.Round(numericUpDown2.Value, 0));
            try
            {
                oleDbConnection.Open();
                string query = "INSERT INTO Barang (Nama, Harga, Stok) VALUES ('" + nama_barang + "'," + harga_barang + "," + jumlah_barang + ")";
                OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Barang Berhasil Ditambahkan");

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oleDbConnection.Close();
                panel2.Visible = false;
                panel1.Visible = true;
            }
        }

        private void HomeVisibleChanged(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                try
                {
                    oleDbConnection.Open();
                    string query = "SELect * FROM Barang";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, oleDbConnection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oleDbConnection.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel1.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int id_edit = Convert.ToInt32(Math.Round(numericUpDown5.Value, 0));

            try
            {
                oleDbConnection.Open();
                string query = "SELECT * FROM Barang WHERE (ID =" +id_edit+ ")";
                OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        panel1.Visible = false;
                        panel3.Visible = true;

                        id_edit_barang = id_edit;
                        textBox3.Text = String.Format("{0}", reader["Nama"]);
                        numericUpDown4.Value = int.Parse(String.Format("{0}", reader["harga"]));
                        numericUpDown3.Value = int.Parse(String.Format("{0}", reader["stok"]));
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ditemukan", "Info");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oleDbConnection.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nama_barang = textBox3.Text;
            int harga_barang = Convert.ToInt32(Math.Round(numericUpDown4.Value, 0));
            int jumlah_barang = Convert.ToInt32(Math.Round(numericUpDown3.Value, 0));

            try
            {
                oleDbConnection.Open();
                string query = "UPDATE Barang SET Nama = '" + nama_barang + "', Harga =" + harga_barang + ", Stok = " + jumlah_barang + " WHERE ID = " + id_edit_barang + "";
                OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Barang Berhasil Diubah");

                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oleDbConnection.Close();

                panel3.Visible = false;
                panel1.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id_delete = Convert.ToInt32(Math.Round(numericUpDown6.Value, 0));

            DialogResult dialogResult = MessageBox.Show("Anda yakin ingin menghapus ID" + id_delete + "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if(dialogResult == DialogResult.Yes)
            {
                try
                {
                    oleDbConnection.Open();
                    string query = "DELETE FROM barang WHERE ID =" + id_delete;
                    OleDbCommand command = new OleDbCommand(query, oleDbConnection);
                    command.ExecuteNonQuery();

                    MessageBox.Show("Barang Berhasil Dihapus");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    oleDbConnection.Close();
                    panel1.Visible = false;
                    panel1.Visible = true;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }
