using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteEditor.Model;
using UnityEngine.UI;
using UniRx;

namespace NoteEditor.Presenter
{
    public class NoteMotionSelectPresenter : MonoBehaviour
    {
        [SerializeField] Dropdown motionOptionSelection;

        // Start is called before the first frame update
        void Start()
        {
            EditState.DirectionVector
                .Subscribe(val =>
                {
                    motionOptionSelection.value = val;
                })
                .AddTo(this);
            motionOptionSelection.onValueChanged
                .AddListener((arg0) => SetMotionOption(arg0));
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetMotionOption(int option)
        {
            EditState.DirectionVector.Value = option;
        }
    }
}