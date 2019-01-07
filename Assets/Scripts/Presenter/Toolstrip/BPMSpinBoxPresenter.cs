using NoteEditor.Model;
using UniRx;

namespace NoteEditor.Presenter
{
    public class BPMSpinBoxPresenter : FloatSpinBoxPresenterBase
    {
        protected override ReactiveProperty<float> GetReactiveProperty()
        {
            return EditData.BPM;
        }
    }
}
