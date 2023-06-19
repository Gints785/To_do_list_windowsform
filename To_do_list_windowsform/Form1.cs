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
        public List<string> Categories = new List<string>();
        private void BindCategory(String Nosaukums)
        {
            var combobox = (DataGridViewComboBoxColumn)ToDoListView.Columns["Kategorija"];
            combobox.DataSource = SetCategories(Nosaukums);
        }
        private List<string> SetCategories(string Nosaukums) 
        {
            Categories.Add(Nosaukums);
            return Categories;
               
        }
        //private List<Category> GetCategories()
        //{
        //    return new List<Category>
        //    {
        //        new Category{Name="Diary",ID=1},
        //        new Category{Name="Grocery", ID=2}
        //    };

        //}


        private void Form1_Load(object sender, EventArgs e)
        {
            todolist.Columns.Add("Svarīgi", typeof(bool));
            todolist.Columns.Add("Virsraksts");
            todolist.Columns.Add("Teksts");

            ToDoListView.DataSource = todolist;
            BindCategory(" ");
        
            ToDoListView.DefaultCellStyle.SelectionBackColor = ToDoListView.DefaultCellStyle.BackColor;
            ToDoListView.DefaultCellStyle.SelectionForeColor = ToDoListView.DefaultCellStyle.ForeColor;
            ToDoListView.AllowUserToAddRows = false;


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TitleTextBox.Text) && string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
              
                return;
            }


            if (isEditing)
            {
               
                bool starStatus = (bool)todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Svarīgi"];
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Virsraksts"] = TitleTextBox.Text;
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Teksts"] = DescriptionTextBox.Text;
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Svarīgi"] = starStatus;

            }
            else
            {
                todolist.Rows.Add(false, TitleTextBox.Text, DescriptionTextBox.Text);
            }
            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
            isEditing = false;
     
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isEditing = false;
            TitleTextBox.Text = "";
            DescriptionTextBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int selectedRowIndex = ToDoListView.CurrentCell?.RowIndex ?? -1;

            if (selectedRowIndex >= 0)
            {


                isEditing = true;
            TitleTextBox.Text = todolist.Rows[ToDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
            DescriptionTextBox.Text = todolist.Rows[ToDoListView.CurrentCell.RowIndex].ItemArray[2].ToString();
            }
        }   

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                isEditing = false;
                todolist.Rows[ToDoListView.CurrentCell.RowIndex].Delete();
                TitleTextBox.Text = "";
                DescriptionTextBox.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }





        private void ToDoListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
          
                ToDoListView.CellClick -= ToDoListView_CellClick;

                bool starStatus = (bool)todolist.Rows[e.RowIndex]["Svarīgi"];
                todolist.Rows[e.RowIndex]["Svarīgi"] = !starStatus;


                ToDoListView.Refresh();
             
            }
        }
        private void ToDoListView_SelectionChanged(object sender, EventArgs e)
        {
            if (ToDoListView.SelectedRows.Count > 0)
            {
                TitleTextBox.ReadOnly = true;
                DescriptionTextBox.ReadOnly = true;
            }
            else
            {
                TitleTextBox.ReadOnly = false;
                DescriptionTextBox.ReadOnly = false;
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


        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CategoryTextBox.Text))
            {

                return;
            }

            else
            {
                BindCategory(CategoryTextBox.Text);
            }
            CategoryTextBox.Text = "";

        }


    }

}
