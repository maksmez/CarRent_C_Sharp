using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DGCar newForm = new DGCar();
            newForm.ShowDialog();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DGRate newForm = new DGRate();
            newForm.ShowDialog();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DGClient newForm = new DGClient();
            newForm.ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DGWorker newForm = new DGWorker();
            newForm.ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DGContract newForm = new DGContract();
            newForm.ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            DGPayment newForm = new DGPayment();
            newForm.ShowDialog();
        }
    }
}
