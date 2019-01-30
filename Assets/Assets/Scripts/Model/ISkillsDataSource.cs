using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public interface ISkillsDataSource
    {
        void PrepareData(Action OnDataReady);

        void AddSkill(Skill skill);
        void UpdateSkill(Skill skill);
        void DeleteSkill(Skill skill);
        Dictionary<string, Skill> GetAllSkills();

        void AddRecord(Record record);
        void UpdateRecord(Record record);
        void DeleteRecord(Record record);
        Dictionary<string, Record> GetAllRecords();
    }
}
