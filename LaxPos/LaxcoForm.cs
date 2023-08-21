using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaxPos
{
    public class LaxcoForm: Form
    {
        public LaxcoForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            Text = "Laxco Form";
            Height = 400;
            Height = 600;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LaxcoForm
            // 
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            this.ClientSize = new System.Drawing.Size(338, 270);
            this.Name = "LaxcoForm";
            this.ResumeLayout(false);

        }
    }
}
