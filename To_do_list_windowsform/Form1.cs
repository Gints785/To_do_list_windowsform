using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_do_list_windowsform
{
    public partial class ToDoList : Form
    {
        bool LightTheme = true;
        public ToDoList()
        {
            InitializeComponent();
        }

        DataTable todolist = new DataTable();
        bool isEditing = false;


        private void Form1_Load(object sender, EventArgs e)
        {
            todolist.Columns.Add("Title");
            todolist.Columns.Add("Description");
            ToDoListView.DataSource = todolist;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Title"] = TitleTextBox.Text;
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Description"] = DescriptionTextBox.Text;
            }
            else
            {
                todolist.Rows.Add(TitleTextBox.Text, DescriptionTextBox.Text);
            }
            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            isEditing = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isEditing = true;
            TitleTextBox.Text = todolist.Rows[ToDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
            DescriptionTextBox.Text = todolist.Rows[ToDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                todolist.Rows[ToDoListView.CurrentCell.RowIndex].Delete();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        // Apakšā ir poga kas atbild par gaismas un tumšās tēmas pārslēgšanu
        private void button5_Click(object sender, EventArgs e)
        {
            LightTheme = !LightTheme;
            if (!LightTheme)
            {
                //Galvenā paneļa krāsa
                this.BackColor = Color.FromArgb(255, 31, 31, 31);

                foreach (Control component in this.Controls)
                {
                    // Visu programmā esošo pogu krāsas
                    if (component is Button)
                    {

                        component.BackColor = Color.FromArgb(255, 31, 31, 31);
                        component.ForeColor = Color.White; ;
                    }
                    // Visu programmā esošo TextBox krāsas
                    else if (component is TextBox)
                    {

                        component.BackColor = Color.FromArgb(255, 31, 31, 31);
                        component.ForeColor = Color.White;

                    }
                    // Visu programmā esošo Label krāsas
                    else if (component is Label)
                    {

                        component.BackColor = Color.FromArgb(255, 31, 31, 31);
                        component.ForeColor = Color.White;

                    }
                }
                // Šis koda bloks ir atbildīgs par krāsu maiņu tabulā, kas atrodas labajā pusē
                this.ToDoListView.EnableHeadersVisualStyles = false;
                this.ToDoListView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 31, 31, 31);
                this.ToDoListView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                this.ToDoListView.DefaultCellStyle.BackColor = Color.FromArgb(255, 31, 31, 31);
                this.ToDoListView.DefaultCellStyle.SelectionBackColor = Color.Black;
                this.ToDoListView.BackgroundColor = Color.FromArgb(255, 31, 31, 31);
                // Maina šīs pogas attēlu
                this.button5.BackgroundImage = To_do_list_windowsform.Properties.Resources.light_theme;
            }
            else if (LightTheme)
            {
                this.BackColor = Color.White;

                foreach (Control component in this.Controls)
                {

                    if (component is Button)
                    {

                        component.BackColor = Color.White;
                        component.ForeColor = Color.FromArgb(255, 31, 31, 31);
                    }
                    else if (component is TextBox)
                    {

                        component.BackColor = Color.White;
                        component.ForeColor = Color.FromArgb(255, 31, 31, 31);

                    }
                    else if (component is Label)
                    {

                        component.BackColor = Color.White;
                        component.ForeColor = Color.FromArgb(255, 31, 31, 31);

                    }
                }
                this.ToDoListView.EnableHeadersVisualStyles = false;
                this.ToDoListView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                this.ToDoListView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(255, 31, 31, 31);
                this.ToDoListView.DefaultCellStyle.BackColor = Color.White;
                this.ToDoListView.DefaultCellStyle.SelectionBackColor = Color.White;
                this.ToDoListView.BackgroundColor = Color.White;
                this.button5.BackgroundImage = To_do_list_windowsform.Properties.Resources.dark_theme;
            }
        }

    }

}
