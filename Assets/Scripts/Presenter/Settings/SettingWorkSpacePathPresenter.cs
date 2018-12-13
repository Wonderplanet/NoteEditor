using NoteEditor.Model;
using System.IO;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using SFB;

namespace NoteEditor.Presenter
{
    public class SettingWorkSpacePathPresenter : MonoBehaviour
    {
        [SerializeField]
        InputField workSpacePathInputField;
        [SerializeField]
        Text workSpacePathInputFieldText;
        [SerializeField]
        Color defaultTextColor;
        [SerializeField]
        Color invalidStateTextColor;

        void Awake()
        {
            workSpacePathInputField.OnValueChangedAsObservable()
                .Select(path => Directory.Exists(path))
                .Subscribe(exists => workSpacePathInputFieldText.color = exists ? defaultTextColor : invalidStateTextColor);

            workSpacePathInputField.OnValueChangedAsObservable()
                .Where(path => Directory.Exists(path))
                .Subscribe(path => Settings.WorkSpacePath.Value = path);

            Settings.WorkSpacePath.DistinctUntilChanged()
                .Subscribe(path => workSpacePathInputField.text = path);

        }

        public void OpenSelectFolderWindow()
        {
            var paths = StandaloneFileBrowser.OpenFolderPanel("Select Workspace Folder", workSpacePathInputField.text, false);
            if(paths.Length > 0)
            {
                workSpacePathInputField.text = paths[0];
            }
        }
    }
}
