using UnityEngine;
using System.Collections;
using NoteEditor.Model;
using SFB;
using System.IO;
using System.Linq;
using UniRx;

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
                var filePath = filePaths.First((arg) => !string.IsNullOrEmpty(arg));
                EditData.FileName.Value = filePath;
                var json = File.ReadAllText(filePath, System.Text.Encoding.UTF8);
                EditDataSerializer.Deserialize(json);
            }
        }
    }
}