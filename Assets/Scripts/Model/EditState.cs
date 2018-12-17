using NoteEditor.Notes;
using NoteEditor.Utility;
using UniRx;

namespace NoteEditor.Model
{
    public class EditState : SingletonMonoBehaviour<EditState>
    {
        ReactiveProperty<bool> isOperatingPlaybackPositionDuringPlay_ = new ReactiveProperty<bool>(false);
        ReactiveProperty<NoteTypes> noteType_ = new ReactiveProperty<NoteTypes>(NoteTypes.Single);
        ReactiveProperty<NotePosition> longNoteTailPosition_ = new ReactiveProperty<NotePosition>();
        ReactiveProperty<int> note_Attributes = new ReactiveProperty<int>();
        ReactiveProperty<int> note_DirectionVector = new ReactiveProperty<int>();

        public static ReactiveProperty<bool> IsOperatingPlaybackPositionDuringPlay { get { return Instance.isOperatingPlaybackPositionDuringPlay_; } }
        public static ReactiveProperty<NoteTypes> NoteType { get { return Instance.noteType_; } }
        public static ReactiveProperty<NotePosition> LongNoteTailPosition { get { return Instance.longNoteTailPosition_; } }
        public static ReactiveProperty<int> AttributeType { get { return Instance.note_Attributes; } }
        public static ReactiveProperty<int> DirectionVector { get { return Instance.note_DirectionVector; } }
    }
}
