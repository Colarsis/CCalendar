using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing.Design;

namespace CCalendar
{
    public partial class WebColorPicker : UserControl
    {
        public WebColorPicker()
        {
            InitializeComponent();

            init();
        }

        public Color SelectedColor
        {
            get { return Color.FromName(dataGridView1.SelectedRows[0].Cells[1].Value.ToString()); }
            private set{}
        }

        private void init()
        {

            Type colorType = typeof(System.Drawing.Color);

            PropertyInfo[] propInfos = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

            List<Color> color = new List<Color>();

            foreach (PropertyInfo propInfo in propInfos)
            {
                color.Add(Color.FromName(propInfo.Name));
            }

            color.Sort(new ColorComparer());

            foreach (Color c in color)
            {
                if (c.Name != "Transparent")
                {
                    string[] row = { "", c.Name };

                    dataGridView1.Rows.Add(row);

                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.BackColor = Color.FromName(c.Name);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Style.SelectionBackColor = Color.FromName(c.Name);
                }
            }
        }

        private void WebColorPicker_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width;
            dataGridView1.Height = this.Height;
        }
    }
}
