﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poznamky2ITB
{
    public partial class PoznamkaView : UserControl
    {

        private Poznamka poznamka;
        public event Action PoznamkaChanged;
        public event Action PoznamkaDeleted;// uvzovoky nepotřebujeme, nepotřebujeeme nic dávat
        public PoznamkaView()
        {
            InitializeComponent();
        }
        public void SetPoznamka(Poznamka data)
        {
            if (data.Finished) { 
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }


            this.poznamka = data;
            label1.Text = data.Headline;
            label2.Text = data.Description;
            label3.Text = $"Vytvořeno {data.DueDate}";
            checkedListBox1.Items.Clear();
            if (poznamka.Finished) { 
                label1.Font = new Font(label1.Font, FontStyle.Strikeout);
            }
            else
            {
                label1.Font = new Font(label1.Font, FontStyle.Regular);
            }



            foreach (var task in data.Subtasks)
            {

                checkedListBox1.Items.Add(task);

            }

            var project = DataManager.Instance.ProjectList.First(p => p.Id == data.ProjectId);
            pictureBox1.BackColor = project.Color;
            label5.Text = project.Name;


        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            DataManager.Instance.RemovePoznamka(poznamka);
            PoznamkaDeleted?.Invoke();
        }

        private void button1_Click(object sender, EventArgs e)// splnění úkolů
        {
            if(checkedListBox1.CheckedItems.Count == checkedListBox1.Items.Count) {
            PoznamkaChanged?.Invoke();

            }
        }
    }
}
