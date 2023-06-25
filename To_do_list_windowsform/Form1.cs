﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace To_do_list_windowsform
{
    public partial class ToDoList : Form
    {
        bool LightTheme = true;
        private DataGridViewComboBoxColumn statusColumn;
        bool IsBold = false;
        bool IsItalic = false;
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
            CategoryBox.DataSource = null;
            CategoryBox.DataSource = Categories;

        }
        private List<string> SetCategories(string Nosaukums) 
        {
            Categories.Add(Nosaukums);
            return Categories;
               
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Categories.Add(" ");
            var combo = (DataGridViewComboBoxColumn)ToDoListView.Columns["Kategorija"];
            combo.DataSource = Categories;
            todolist.Columns.Add("Svarīgi", typeof(bool));
            todolist.Columns.Add("Nosaukums");
            todolist.Columns.Add("Teksts");
        
            ToDoListView.DataSource = todolist;
        
            ToDoListView.DefaultCellStyle.SelectionBackColor = ToDoListView.DefaultCellStyle.BackColor;
            ToDoListView.DefaultCellStyle.SelectionForeColor = ToDoListView.DefaultCellStyle.ForeColor;
            ToDoListView.AllowUserToAddRows = false;

            ToDoListView.Columns["Nosaukums"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
            ToDoListView.Columns["Teksts"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
       
            ToDoListView.Columns["Svarīgi"].Width = 45;
           



            statusColumn = new DataGridViewComboBoxColumn();
            statusColumn.DataPropertyName = "Status";
            statusColumn.HeaderText = "Status";
            statusColumn.Items.AddRange("", "Plānots", "Pabeigts");
            ToDoListView.Columns.Add(statusColumn);
            LoadCategory();
            LoadData();

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
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Nosaukums"] = TitleTextBox.Text;
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Teksts"] = DescriptionTextBox.Text;
                todolist.Rows[ToDoListView.CurrentCell.RowIndex]["Svarīgi"] = starStatus;

            }
            else
            {
                todolist.Rows.Add(false, TitleTextBox.Text, DescriptionTextBox.Text);
                if (IsBold && IsItalic)
                {

                    ToDoListView.Rows[ToDoListView.RowCount - 1].Cells[3].Style.Font = new Font(ToDoListView.DefaultCellStyle.Font, FontStyle.Bold | FontStyle.Italic);
                }
                else if(IsBold && !IsItalic)
                {
                    ToDoListView.Rows[ToDoListView.RowCount - 1].Cells[3].Style.Font = new Font(ToDoListView.DefaultCellStyle.Font, FontStyle.Bold);
                }
                else if(!IsBold && IsItalic)
                {
                    ToDoListView.Rows[ToDoListView.RowCount - 1].Cells[3].Style.Font = new Font(ToDoListView.DefaultCellStyle.Font, FontStyle.Italic);
                }
                else if(!IsBold && !IsItalic)
                {
                    ToDoListView.Rows[ToDoListView.RowCount - 1].Cells[3].Style.Font = new Font(ToDoListView.DefaultCellStyle.Font, FontStyle.Regular);
                }
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

            int Fcounter = 0;
            foreach (string finding in Categories)
            {
                if(finding == CategoryTextBox.Text)
                {
                    Fcounter++;
                }
            }
            if (string.IsNullOrEmpty(CategoryTextBox.Text))
            {

                return;
            }
            else if(Fcounter == 0)
            {
                BindCategory(CategoryTextBox.Text);
            }
            else{
                MessageBox.Show("Nevar izveidot divas identiskas kategorijas", "Radīšanas kļūda", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CategoryTextBox.Text = "";

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (CategoryBox.SelectedIndex != -1 && (string)CategoryBox.SelectedItem != " ")
            {
                if(MessageBox.Show("Vai esat pārliecināts, ka vēlaties noņemt kategoriju?", "Delete Kategorija",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in ToDoListView.Rows)
                    {
                        if (row.Cells[0].Value != null)
                        {
                            if (row.Cells[0].Value.ToString() == (string)CategoryBox.SelectedItem)
                            {
                                row.Cells[0].Value = Categories[0];
                            }
                        }
                        if (row.Cells[0].Value == null)
                        {
                            row.Cells[0].Value = Categories[0];
                        }
                    }
                    Categories.Remove((string)CategoryBox.SelectedItem);
                    CategoryBox.DataSource = null;
                    CategoryBox.DataSource = Categories;

                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Izvēlieties, kuru kategoriju vēlaties noņemt", "Delete Kategorija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void SaveData()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "data.txt");

            try
            {
                int RCounter = 0;
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (DataRow row in todolist.Rows)
                    {
                        
                        bool important = (bool)row["Svarīgi"];
                        string title = row["Nosaukums"].ToString();
                        string description = row["Teksts"].ToString();
                        string kategorijas;
                        string status;


                        if (ToDoListView.Rows[RCounter].Cells[0].Value != null)
                        {
                           kategorijas = ToDoListView.Rows[RCounter].Cells[0].Value.ToString();
                        }
                        else
                        {
                            
                            kategorijas = Categories[0];
                           
                        }
                        if (ToDoListView.Rows[RCounter].Cells[4].Value != null)
                        {
                            status = ToDoListView.Rows[RCounter].Cells[4].Value.ToString();
                        }
                        else
                        {

                            status = "";

                        }

                        RCounter++;




                        sw.WriteLine($"{kategorijas}\t{important}\t{title}\t{description}\t{status}"); 
                    }
                }
                MessageBox.Show("Data saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data: " + ex.Message);
            }
        }
        private void SaveCategory()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "CategoryData.txt");

            try
            {
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (string row in Categories)
                    {

                        if (row != " ")
                        {
                            string kategorijas = row.ToString();

                            sw.WriteLine($"{kategorijas}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data: " + ex.Message);
            }
        }







        private void LoadData()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "data.txt");

            if (File.Exists(filePath))
            {
                try
                {
                    int RCounter = 0;
                    todolist.Clear();
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] data = line.Split('\t');
                            string kategorijas = data[0];
                            bool important = bool.Parse(data[1]);
                            string title = data[2];
                            string description = data[3];
                            string status = data[4];
                            



                            todolist.Rows.Add(important, title, description);
                            ToDoListView.Rows[RCounter].Cells[0].Value = kategorijas;
                            ToDoListView.Rows[RCounter].Cells[4].Value = status;
                            RCounter++;

                       


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading data: " + ex.Message);
                }
            }

       


        }

        private void LoadCategory()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "CategoryData.txt");

            if (File.Exists(filePath))
            {
                try
                {
                    todolist.Clear();
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {

                            string[] data = line.Split('\n');
                            if (data[0] != " ")
                            {
                                BindCategory(data[0]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading data: " + ex.Message);
                }
            }
        }

        //==============================================================================================
      

        //==============================================================================================

        private void button8_Click_1(object sender, EventArgs e)
        {
            SaveData();
            SaveCategory();
           
        }



        private void button9_Click(object sender, EventArgs e)
        {
            IsBold = !IsBold;
            if (IsBold && IsItalic)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Bold | FontStyle.Italic);
            }
            else if (!IsBold && IsItalic)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Regular | FontStyle.Italic);
            }
            else if (IsBold)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Bold);
            }
            else if (!IsBold)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Regular);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            IsItalic = !IsItalic;
            if (IsItalic && IsBold)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Italic | FontStyle.Bold);
            }
            else if (!IsItalic && IsBold)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Regular | FontStyle.Bold);
            }
            else if (IsItalic)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Italic);
            }
            else if(!IsItalic)
            {
                DescriptionTextBox.Font = new Font(DescriptionTextBox.Font, FontStyle.Regular);
            }
        }
    }

}
