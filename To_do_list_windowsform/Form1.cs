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
            if(isEditing)
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
    }
}
