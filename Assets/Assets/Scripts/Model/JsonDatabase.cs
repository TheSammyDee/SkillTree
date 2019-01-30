using Firebase.Database;
using SkillTree.Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility;

namespace SkillTree.Model
{
    public class JsonDatabase : ISkillsDataSource
    {
        FirebaseReader reader;
        private Dictionary<string, Skill> skills;
        private Dictionary<string, Record> records;
        private Dictionary<string, List<string>> skillParents;
        ILevelFormula formula;

        private event Action OnDataReady;

        public JsonDatabase(string userId)
        {
            reader = new FirebaseReader(userId);
            skills = new Dictionary<string, Skill>();
            records = new Dictionary<string, Record>();
            skillParents = new Dictionary<string, List<string>>();
            formula = new MockLevelFormula();
        }

        private class RecordObject
        {
            public float amount;
            public List<string> skills;
            public string origin;

            public RecordObject(Record record)
            {
                amount = record.amount;
                skills = record.skills.Select(x => x.guid).ToList();
                origin = record.originGuid;
            }
        }

        private class SkillObject
        {
            public string name;
            public Dictionary<string, float> color;
            public List<string> parents;

            public SkillObject(Skill skill)
            {
                name = skill.name;
                color = new Dictionary<string, float>();
                color.Add("r", skill.color.r);
                color.Add("g", skill.color.g);
                color.Add("b", skill.color.b);
                parents = skill.parents.Select(x => x.guid).ToList();
            }
        }

        public void PrepareData(Action OnDataReady)
        {
            this.OnDataReady = OnDataReady;

            Debugger.Instance.Log("checking Firebase");
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    Debugger.Instance.Log("successful check, requesting skills");
                    reader.Read("Skills", OnReceiveSkills, () => Debugger.Instance.Log("error requesting skills"));

                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    //   app = Firebase.FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    Debugger.Instance.Log("error checking Firebase");
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }

        private void OnReceiveSkills(DataSnapshot snapshot)
        {
            Debugger.Instance.Log("skills received");
            Debugger.Instance.Log("key = " + snapshot.Key.ToString());
            object jsonList = snapshot.Value;
            var jsonDict = (Dictionary<string, object>)jsonList;
            Debugger.Instance.Log("created dict");

            foreach (KeyValuePair<string, object> skillPair in jsonDict)
            {
                skills.Add(skillPair.Key, CreateSkill(skillPair.Key, skillPair.Value));
            }

            foreach (Skill skill in skills.Values)
            {
                if (skillParents.ContainsKey(skill.guid))
                {
                    foreach (string parentId in skillParents[skill.guid])
                    {
                        skill.parents.Add(skills[parentId]);
                    }
                }
            }
            Debugger.Instance.Log("requesting records");
            reader.Read("Records", OnReceiveRecords, () => Debugger.Instance.Log("error receiving records"));
        }

        private void OnReceiveRecords(DataSnapshot snapshot)
        {
            Debugger.Instance.Log("records received");
            object json = snapshot.Value;

            if (json != null)
            {
                var jsonDates = (Dictionary<string, object>)json;

                foreach (KeyValuePair<string, object> datePair in jsonDates)
                {
                    DateTime date = DateTime.Parse(datePair.Key);

                    var jsonRecords = (Dictionary<string, object>)datePair.Value;

                    foreach (KeyValuePair<string, object> recordPair in jsonRecords)
                    {
                        records.Add(recordPair.Key, CreateRecord(recordPair.Key, date, recordPair.Value));
                    }
                }
            }
            Debugger.Instance.Log("data ready");
            OnDataReady();
        }

        private Skill CreateSkill(string guid, object json)
        {
            Debugger.Instance.Log("creating skill " + guid);
            var jsonSkill = (Dictionary<string, object>)json;
            string name = jsonSkill["name"].ToString();

            var jsonColor = (Dictionary<string, object>)jsonSkill["color"];
            Color color = new Color(
                Convert.ToSingle(jsonColor["r"]), 
                Convert.ToSingle(jsonColor["g"]), 
                Convert.ToSingle(jsonColor["b"])
                );

            if (jsonSkill.ContainsKey("parents"))
            {
                var jsonParents = (Dictionary<string, object>)jsonSkill["parents"];
                skillParents.Add(guid, new List<string>());

                foreach (string parent in jsonParents.Keys)
                {
                    skillParents[guid].Add(parent);
                }
            }

            Debugger.Instance.Log("skill complete " + name);
            return new Skill(guid, name, formula, color, new HashSet<Skill>());
        }

        private Record CreateRecord(string guid, DateTime date, object json)
        {
            Debugger.Instance.Log("creating record " + guid);
            var jsonRecord = (Dictionary<string, object>)json;
            float amount = Convert.ToSingle(jsonRecord["amount"]);
            string origin = (string)jsonRecord["origin"];
            Record record = new Record(guid, date, amount, origin);

            var jsonSkills = (Dictionary<string, object>)jsonRecord["skills"];

            foreach (string id in jsonSkills.Keys)
            {
                record.skills.Add(skills[id]);
                skills[id].AddRecord(record);
            }

            Debugger.Instance.Log("created record");
            return record;
        }

        public void AddRecord(Record record)
        {
            string date = String.Format("{0:yyyy'-'MM'-'dd}", record.date);
            reader.WriteRecord(date, record.guid, record.amount, record.originGuid, record.skills.Select(x => x.guid).ToList());
        }

        public void AddSkill(Skill skill)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteRecord(Record record)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSkill(Skill skill)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, Record> GetAllRecords()
        {
            return records;
        }

        public Dictionary<string, Skill> GetAllSkills()
        {
            return skills;
        }

        public void UpdateRecord(Record record)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSkill(Skill skill)
        {
            throw new System.NotImplementedException();
        }
    }
}