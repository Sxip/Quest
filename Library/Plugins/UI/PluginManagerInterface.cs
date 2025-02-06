using System;
using System.Windows.Forms;
using Library.Plugins.Abstractions;

namespace Library.Plugins.UI
{
    public partial class PluginManagerInterface : Form
    {
        public static readonly PluginManagerInterface Instance = new PluginManagerInterface();

        public PluginManagerInterface()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
        }

        public void ListenForPluginLoadsAndUnloads()
        {
            PluginManager.PluginLoaded += PopulatePluginsInDataGridView;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                var pluginName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

                if (e.ColumnIndex == 3)
                {
                    PluginManager.Instance.Unload(pluginName);
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = true;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = false;
                }
            }
        }

        private void PopulatePluginsInDataGridView(QPlugin plugin) => dataGridView1.Rows.Add(
            plugin.Name,
            plugin.Description,
            plugin.Author
        );

        private void PluginManagerInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing) return;

            e.Cancel = true;
            Hide();
        }

        private void PluginManagerInterface_Load(object sender, EventArgs e)
        {
            // TODO: add code to handle the Load event here.
        }
    }
}