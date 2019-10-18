using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsCoreLib.Forms
{
    public partial class FormBase : Form
    {
        public static Form New()
        {
            return new FormBase();
        }

        public FormBase()
        {
            InitializeComponent();
        }
    }
}
