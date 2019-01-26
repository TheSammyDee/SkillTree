using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace SkillTree.Model
{
    public class SkillCollection
    {
        ISkillsDataSource dataSource;
        HashSet<Skill> skills;
        HashSet<Record> records;
        ILevelFormula formula;

        public SkillCollection(ISkillsDataSource dataSource, ILevelFormula formula)
        {
            this.dataSource = dataSource;
            this.formula = formula;
            skills = dataSource.GetAllSkills();
            records = dataSource.GetAllRecords();
            Dictionary<string, HashSet<string>> recordSkillMappings = dataSource.GetRecordSkillMappings();

            foreach (string recordGuid in recordSkillMappings.Keys)
            {
                Record record = records.Where(x => x.guid == recordGuid).First();
                HashSet<Skill> skillSet = new HashSet<Skill>();
                foreach(string skillGuid in recordSkillMappings[recordGuid])
                {
                    Skill sk = skills.Where(x => x.guid == skillGuid).First();
                    record.AddSkill(sk);
                    sk.AddRecord(record);
                }
            }

            foreach (Skill skill in skills)
            {
                skill.formula = formula;
            }
        }

        public void AddSkill(string name, Color color, List<Skill> parents)
        {
            Skill newSkill = new Skill(name, formula);
            newSkill.color = color;
            foreach (Skill skill in parents)
            {
                newSkill.AddParent(skill);
            }

            dataSource.AddSkill(newSkill);
        }

        public void AddRecord(DateTime date, float amount, Skill skill)
        {
            Record newRecord = new Record(date, amount, skill.guid);
            HashSet<>
        }
    }
}
