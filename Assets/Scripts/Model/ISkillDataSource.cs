using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public interface ISkillDataSource
    {
        bool AddSkill(Skill skill);
        bool UpdateSkill(Skill skill);
        bool DeleteSkill(Skill skill);
        List<Skill> GetAllSkills();

        bool AddRecord(Record record);
        bool UpdateRecord(Record record);
        bool DeleteRecord(Record record);
        List<Record> GetAllRecords();
    }
}
