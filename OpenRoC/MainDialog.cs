namespace oroc
{
    using System.Windows.Forms;

    public partial class MainDialog : Form
    {
        private Form AddProcessForm;

        public MainDialog()
        {
            InitializeComponent();
        }

        private void DisposeAddedComponents()
        {
            if (AddProcessForm != null)
                AddProcessForm.Dispose();
        }

        private void OnProcessListViewResize(object sender, System.EventArgs e)
        {
            if (ProcessListView.Columns.Count > 0)
                ProcessListView.AutoResizeColumn(
                    ProcessListView.Columns.Count - 1,
                    ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void OnAddButtonMouseDown(object sender, MouseEventArgs e)
        {
            if (AddProcessForm == null || AddProcessForm.IsDisposed)
                AddProcessForm = new AddProcessDialog();

            if (!AddProcessForm.Visible)
                AddProcessForm.Show();
            else
                AddProcessForm.Focus();
        }
    }
}
