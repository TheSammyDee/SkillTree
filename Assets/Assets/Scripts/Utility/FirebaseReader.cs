using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// Access variable on Firebase on a by-request basis
    /// </summary>
    public class FirebaseReader
    {
        private DatabaseReference reference;
        private const string url = "https://skill-tree-tracker.firebaseio.com/";
        private string userID;

        public FirebaseReader(string userID)
        {
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(url);
            reference = FirebaseDatabase.DefaultInstance.RootReference;
            this.userID = userID;
        }

        /// <summary>
        /// Reads the data at the given <paramref name="key"/> from the user's branch
        /// </summary>
        /// <param name="key"></param>
        /// <param name="OnComplete"></param>
        /// <param name="OnFaulted"></param>
        public void Read(string key, Action<DataSnapshot> OnComplete, Action OnFaulted)
        {
            reference.Child(userID).Child(key).GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    if (OnFaulted != null)
                        OnFaulted();
                }
                else if (task.IsCompleted)
                {
                    if (OnComplete != null)
                        OnComplete(task.Result);
                }
            });
        }

        public void WriteSkill(string skillGuid, string json)
        {
            reference.Child(userID).Child("Skills").Child(skillGuid).SetRawJsonValueAsync(json);
        }

        public void WriteRecord(string date, string recordGuid, float amount, string origin, List<string> skills)
        {
            reference.Child(userID).Child("Records").Child(date).Child(recordGuid).Child("amount").SetValueAsync(amount);
            reference.Child(userID).Child("Records").Child(date).Child(recordGuid).Child("origin").SetValueAsync(origin);

            foreach (string skill in skills)
            {
                reference.Child(userID).Child("Records").Child(date).Child(recordGuid).Child("skills").Child(skill).SetValueAsync(true);
            }
        }
    }
}
