using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocStateApp.Core.Interfaces;
using System.Windows.Forms; 

namespace DocStateApp.UI.Services
{
    public class DialogService : IDialogService
    {
        public string? OpenFolderDialog(string? initialPath = null)
        {
            using var dlg = new FolderBrowserDialog();
            if (!string.IsNullOrWhiteSpace(initialPath))
                dlg.SelectedPath = initialPath;
            var res = dlg.ShowDialog();
            return res == DialogResult.OK ? dlg.SelectedPath : null;
        }
    }
}
