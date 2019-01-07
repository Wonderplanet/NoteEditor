using System;
using System.Text.RegularExpressions;
using NoteEditor.Common;
using NoteEditor.Utility;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NoteEditor.Presenter
{
    public abstract class FloatSpinBoxPresenterBase : MonoBehaviour
    {
        [SerializeField]
        InputField inputField;
        [SerializeField]
        Button increaseButton;
        [SerializeField]
        Button decreaseButton;
        [SerializeField]
        float valueStep;
        [SerializeField]
        float minValue;
        [SerializeField]
        float maxValue;
        [SerializeField]
        int longPressTriggerMilliseconds;
        [SerializeField]
        int continuousPressIntervalMilliseconds;

        Subject<float> _operateSpinButtonObservable = new Subject<float>();

        protected abstract ReactiveProperty<float> GetReactiveProperty();

        void Awake()
        {
            increaseButton.AddListener(EventTriggerType.PointerUp, e => _operateSpinButtonObservable.OnNext(0));
            decreaseButton.AddListener(EventTriggerType.PointerUp, e => _operateSpinButtonObservable.OnNext(0));
            increaseButton.AddListener(EventTriggerType.PointerDown, e => _operateSpinButtonObservable.OnNext(valueStep));
            decreaseButton.AddListener(EventTriggerType.PointerDown, e => _operateSpinButtonObservable.OnNext(-valueStep));

            var property = GetReactiveProperty();

            property.Subscribe(x => inputField.text = x.ToString());
            //float temp = 0f;
            var updateValueFromInputFieldStream = inputField.OnValueChangedAsObservable()
                //.Where(x => Regex.IsMatch(x, @"^[0-9]+$"))
                .Select(x => {
                    var temp = 0f;
                    if (float.TryParse(x, out temp))
                        return temp;
                    return property.Value;
                });

            var updateValueFromSpinButtonStream = _operateSpinButtonObservable
                //.Throttle(TimeSpan.FromMilliseconds(longPressTriggerMilliseconds))
                //.Where(delta => Math.Abs(delta) > float.Epsilon)
                //.SelectMany(delta => Observable.Interval(TimeSpan.FromMilliseconds(continuousPressIntervalMilliseconds))
                //    .TakeUntil(_operateSpinButtonObservable.Where(d => Math.Abs(d) <= float.Epsilon))
                //    .Select(_ => delta))
                //.Merge(_operateSpinButtonObservable.Where(d => Math.Abs(d) > float.Epsilon))
                .Select(delta => property.Value + delta);

            var isUndoRedoAction = false;

            Observable.Merge(
                    updateValueFromSpinButtonStream,
                    updateValueFromInputFieldStream)
                .Select(x => Mathf.Clamp(x, minValue, maxValue))
                .DistinctUntilChanged()
                .Where(_ => isUndoRedoAction ? (isUndoRedoAction = false) : true)
                .Select(x => new { current = x, prev = property.Value })
                .Subscribe(x => EditCommandManager.Do(
                    new Command(
                        () => property.Value = x.current,
                        () => { isUndoRedoAction = true; property.Value = x.prev; },
                        () => { isUndoRedoAction = true; property.Value = x.current; })))
                .AddTo(this);
        }
    }
}