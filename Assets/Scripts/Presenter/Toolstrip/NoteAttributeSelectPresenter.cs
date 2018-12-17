using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NoteEditor.Model;
using UnityEngine.UI;
using UniRx;

namespace NoteEditor.Presenter
{
    public class NoteAttributeSelectPresenter : MonoBehaviour
    {
        [SerializeField] Dropdown attributeSelection;

        // Start is called before the first frame update
        void Start()
        {
            EditState.AttributeType
                .Subscribe(index =>
                {
                    attributeSelection.value = index;
                })
                .AddTo(this);
            attributeSelection.onValueChanged
                .AddListener((arg0) => SelectOption(arg0));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SelectOption(int index)
        {
            EditState.AttributeType.Value = index;
        }
    }
}