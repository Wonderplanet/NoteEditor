using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NoteEditor.Presenter
{
    using Model;
    using Notes;
    public class BeatShiftPresenter : MonoBehaviour
    {
        public void ShiftNotesToLeft()
        {
            var notes = EditData.Notes.Values.ToList();
            foreach(var noteObj in notes)
            {
                var position = noteObj.note.position;
                var note = noteObj.note;
                EditData.Notes.Remove(position);
                position.num -= position.LPB;
                note.position = position;
                var noteObject = new NoteObject();
                noteObject.SetState(note);
                noteObject.Init();
                EditData.Notes[position] = noteObj;
            }
        }

        public void ShiftNotesToRight()
        {
            var notes = EditData.Notes.Values.ToList();
            foreach (var noteObj in notes)
            {
                var position = noteObj.note.position;
                var note = noteObj.note;
                EditData.Notes.Remove(position);
                position.num += position.LPB;
                note.position = position;
                var noteObject = new NoteObject();
                noteObject.SetState(note);
                noteObject.Init();
                EditData.Notes[position] = noteObj;
            }
        }
    }
}