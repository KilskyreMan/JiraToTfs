using System.Diagnostics;
using System.Windows.Forms;

namespace JiraToTfs.View
{
    public partial class MissingTfsDependenciesView : Form
    {
        public MissingTfsDependenciesView()
        {
            InitializeComponent();
        }

        private void OnClickTellMeMore(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://visualstudiogallery.msdn.microsoft.com/f30e5cc7-036e-449c-a541-d522299445aa");
            Close();
        }
    }
}
