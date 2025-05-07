using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAccessControl
{
    public partial class AccountantForm : Form
    {
        public AccountantForm()
        {
            InitializeComponent();
            // Здесь можно загружать и отображать данные, специфичные для бухгалтера
            txtAccountantInfo.Text = "Accountant: Здесь должна быть информация, доступная только бухгалтеру.";
        }

        private void AccountantForm_Load(object sender, EventArgs e)
        {

        }
    }
}
