using System;
using DocStateApp.Core.Interfaces;


namespace DocStateApp.UI.Services
{
  
        public class DialogoService : IDialogo
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
    
{
	

