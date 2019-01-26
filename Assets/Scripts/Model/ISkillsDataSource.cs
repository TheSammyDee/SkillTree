using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public interface ISkillsDataSource
    {
        bool AddSkill(Skill skill);
        bool UpdateSkill(Skill skill);
        bool DeleteSkill(Skill skill);
        Dictionary<string, Skill> GetAllSkills();

        bool AddRecord(Record record);
        bool UpdateRecord(Record record);
        bool DeleteRecord(Record record);
        Dictionary<string, Record> GetAllRecords();
    }
}
