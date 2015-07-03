using System.Windows.Forms;

namespace BoredTool
{
    public partial class SQL : Form
    {
        public SQL(string sql)
        {
            InitializeComponent();
            richTextBox1.Text = sql;
        }
    }
}
