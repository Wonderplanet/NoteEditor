using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NoteEditor.Presenter
{
    using Model;
    using Notes;
    using DTO;
    public class BeatShiftPresenter : MonoBehaviour
    {
        public void ShiftNotesToLeft()
        {
            var jsonString = EditDataSerializer.Serialize();

            var editData = UnityEngine.JsonUtility.FromJson<MusicDTO.EditData>(jsonString);

            foreach(var note in editData.notes)
            {
                note.num -= note.LPB;
                if(note.type != 1)
                {
                    foreach(var subnote in note.notes)
                    {
                        subnote.num -= note.LPB;
                    }
                }
            }

            jsonString = UnityEngine.JsonUtility.ToJson(editData);


            foreach (var note in EditData.Notes.Values)
            {
                note.Dispose();
            }

            EditData.Notes.Clear();

            EditDataSerializer.Deserialize(jsonString);
        }

        public void ShiftNotesToRight()
        {
            var jsonString = EditDataSerializer.Serialize();

            var editData = UnityEngine.JsonUtility.FromJson<MusicDTO.EditData>(jsonString);

            foreach (var note in editData.notes)
            {
                note.num += note.LPB;
                if (note.type != 1)
                {
                    foreach (var subnote in note.notes)
                    {
                        subnote.num += note.LPB;
                    }
                }
            }

            jsonString = UnityEngine.JsonUtility.ToJson(editData);

            foreach (var note in EditData.Notes.Values)
            {
                note.Dispose();
            }

            EditData.Notes.Clear();

            EditDataSerializer.Deserialize(jsonString);
        }
    }
}