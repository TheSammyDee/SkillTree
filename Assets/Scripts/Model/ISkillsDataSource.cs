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
        HashSet<Skill> GetAllSkills();

        bool AddRecord(Record record);
        bool UpdateRecord(Record record);
        bool DeleteRecord(Record record);
        HashSet<Record> GetAllRecords();

        Dictionary<string, HashSet<string>> GetRecordSkillMappings();
    }
}
