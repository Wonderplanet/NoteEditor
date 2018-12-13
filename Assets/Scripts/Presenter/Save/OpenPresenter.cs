using UnityEngine;
using System.Collections;
using NoteEditor.Model;
using SFB;
using System.IO;

namespace NoteEditor.Presenter
{
    public class OpenPresenter : MonoBehaviour
    {
        ExtensionFilter[] extensionFilters = new ExtensionFilter[]
        {
            new ExtensionFilter("json", "json")

        };

        public void OpenData()
        {
            var filePaths = StandaloneFileBrowser.OpenFilePanel("Select Edit Data", Settings.WorkSpacePath.Value, extensionFilters, false);
            if(filePaths.Length > 0)
            {
                var json = File.ReadAllText(filePaths[0], System.Text.Encoding.UTF8);
                EditDataSerializer.Deserialize(json);
            }
        }
    }
}