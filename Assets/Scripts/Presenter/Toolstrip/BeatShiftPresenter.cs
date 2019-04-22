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
                if (note.type == NoteTypes.Long)
                {
                    var currNote = note;
                    while(currNote != null && EditData.Notes.ContainsKey(currNote.next))
                    {
                        var subNoteObj = EditData.Notes[currNote.next];
                        var subNote = subNoteObj.note;
                        var subPos = subNote.position;
                        EditData.Notes.Remove(subPos);
                        subPos.num -= position.LPB;
                        currNote.next = subPos;
                        subNote.prev = currNote.position; 
                        subNoteObj = new NoteObject();
                        subNoteObj.SetState(subNote);
                        subNoteObj.Init();
                        EditData.Notes[subPos] = subNoteObj;
                        currNote = subNote;
                    }
                }

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
                if (note.type == NoteTypes.Long)
                {
                    var currNote = note;
                    while (currNote != null && EditData.Notes.ContainsKey(currNote.next))
                    {
                        var subNoteObj = EditData.Notes[currNote.next];
                        var subNote = subNoteObj.note;
                        var subPos = subNote.position;
                        EditData.Notes.Remove(subPos);
                        subPos.num += position.LPB;
                        currNote.next = subPos;
                        subNote.prev = currNote.position;
                        subNoteObj = new NoteObject();
                        subNoteObj.SetState(subNote);
                        subNoteObj.Init();
                        EditData.Notes[subPos] = subNoteObj;
                        currNote = subNote;
                    }
                }

                var noteObject = new NoteObject();
                noteObject.SetState(note);
                noteObject.Init();
                EditData.Notes[position] = noteObj;
            }
        }
    }
}