using NoteEditor.Notes;
using NoteEditor.Utility;
using System.Collections.Generic;
using UniRx;

namespace NoteEditor.Model
{
    public class EditData
    {
        static ReactiveProperty<string> filename = new ReactiveProperty<string>();

        static ReactiveProperty<string> name_ = new ReactiveProperty<string>();
        static ReactiveProperty<int> maxBlock_ = new ReactiveProperty<int>(5);
        static ReactiveProperty<int> LPB_ = new ReactiveProperty<int>(4);
        static ReactiveProperty<int> BPM_ = new ReactiveProperty<int>(120);
        static ReactiveProperty<int> offsetSamples_ = new ReactiveProperty<int>(0);
        static Dictionary<NotePosition, NoteObject> notes_ = new Dictionary<NotePosition, NoteObject>();

        public static ReactiveProperty<string> FileName { get { return filename; } }
        public static ReactiveProperty<string> Name { get { return name_; } }
        public static ReactiveProperty<int> MaxBlock { get { return maxBlock_; } }
        public static ReactiveProperty<int> LPB { get { return LPB_; } }
        public static ReactiveProperty<int> BPM { get { return BPM_; } }
        public static ReactiveProperty<int> OffsetSamples { get { return offsetSamples_; } }
        public static Dictionary<NotePosition, NoteObject> Notes { get { return notes_; } }
    }
}
