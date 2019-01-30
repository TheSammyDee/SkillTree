using Firebase.Database;
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

        private event Action OnDataReady;

        public JsonDatabase(string userId)
        {
            reader = new FirebaseReader(userId);
            skills = new Dictionary<string, Skill>();
            records = new Dictionary<string, Record>();
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

            reader.Read("Skills", OnReceiveSkills, null);
        }

        private void OnReceiveSkills(DataSnapshot snapshot)
        {
            object jsonList = snapshot.Value;
            var jsonDict = (Dictionary<string, object>)jsonList;

            foreach (KeyValuePair<string, object> skillPair in jsonDict)
            {
                skills.Add(skillPair.Key, CreateSkill(skillPair.Key, skillPair.Value));
            }

            foreach (Skill skill in skills.Values)
            {
                foreach (string parentId in skillParents[skill.guid])
                {
                    skill.parents.Add(skills[parentId]);
                }
            }

            reader.Read("Records", OnReceiveRecords, null);
        }

        private void OnReceiveRecords(DataSnapshot snapshot)
        {
            object json = snapshot.Value;
            var jsonDates = (Dictionary<string, object>)json;

            foreach (KeyValuePair<string, object> datePair in jsonDates)
            {
                DateTime date = DateTime.Parse(datePair.Key);

                var jsonRecords = (Dictionary<string, object>)datePair.Value;

                foreach (KeyValuePair<string, object> recordPair in jsonRecords)
                {

                }
            }
        }

        private Skill CreateSkill(string guid, object json)
        {
            var jsonSkill = (Dictionary<string, object>)json;
            string name = jsonSkill["name"].ToString();

            var jsonColor = (Dictionary<string, float>)jsonSkill["color"];
            Color color = new Color(jsonColor["r"], jsonColor["g"], jsonColor["b"]);

            if (jsonSkill.ContainsKey("parents"))
            {
                var jsonParents = (Dictionary<string, object>)jsonSkill["parents"];
                skillParents.Add(guid, new List<string>());

                foreach (string parent in jsonParents.Keys)
                {
                    skillParents[guid].Add(parent);
                }
            }

            return new Skill(guid, name, color, new HashSet<Skill>());
        }

        private void CreateRecord(string guid, DateTime date, object json)
        {

        }

        public void AddRecord(Record record)
        {
            RecordObject recordObject = new RecordObject(record);
            string json = JsonUtility.ToJson(recordObject);
            reader.WriteRecord(record.date.ToShortDateString(), record.guid, json);
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
            throw new System.NotImplementedException();
        }

        public Dictionary<string, Skill> GetAllSkills()
        {
            throw new System.NotImplementedException();
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