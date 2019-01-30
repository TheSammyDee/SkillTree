using SkillTree.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Tests
{
    /// <summary>
    /// A mock database prefilled with <see cref="Skill"/>s in a varied tree
    /// pattern and with no records.
    /// </summary>
    public class MockSkillsDataSource : ISkillsDataSource
    {
        Dictionary<string, Skill> skills;
        Dictionary<string, Record> records;

        public MockSkillsDataSource()
        {
            MockLevelFormula formula = new MockLevelFormula();
            Skill abe = new Skill("abe", "abe", formula, Color.white, new HashSet<Skill>());
            Skill cef = new Skill("cef", "cef", formula, Color.white, new HashSet<Skill>());
            Skill h = new Skill("h", "h", formula, Color.white, new HashSet<Skill>());
            Skill i = new Skill("i", "i", formula, Color.white, new HashSet<Skill>());
            Skill ab = new Skill("ab", "ab", formula, Color.white, new HashSet<Skill>() { abe });
            Skill e = new Skill("e", "e", formula, Color.white, new HashSet<Skill>() { abe, cef });
            Skill f = new Skill("f", "f", formula, Color.white, new HashSet<Skill>() { cef });
            Skill g = new Skill("g", "g", formula, Color.white, new HashSet<Skill>() { h, i });
            Skill a = new Skill("a", "a", formula, Color.white, new HashSet<Skill>() { ab });
            Skill b = new Skill("b", "b", formula, Color.white, new HashSet<Skill>() { ab });
            Skill c = new Skill("c", "c", formula, Color.white, new HashSet<Skill>() { e, f });
            Skill d = new Skill("d", "d", formula, Color.white, new HashSet<Skill>() { g });

            skills = new Dictionary<string, Skill>();

            skills.Add(abe.guid, abe);
            skills.Add(cef.guid, cef);
            skills.Add(h.guid, h);
            skills.Add(i.guid, i);
            skills.Add(ab.guid, ab);
            skills.Add(e.guid, e);
            skills.Add(f.guid, f);
            skills.Add(g.guid, g);
            skills.Add(a.guid, a);
            skills.Add(b.guid, b);
            skills.Add(c.guid, c);
            skills.Add(d.guid, d);

            records = new Dictionary<string, Record>();
        }

        public void AddRecord(Record record)
        {
            records.Add(record.guid, record);
        }

        public void AddSkill(Skill skill)
        {
            skills.Add(skill.guid, skill);
        }

        public void DeleteRecord(Record record)
        {
            records.Remove(record.guid);
        }

        public void DeleteSkill(Skill skill)
        {
            skills.Remove(skill.guid);
        }

        public Dictionary<string, Record> GetAllRecords()
        {
            return records;
        }

        public Dictionary<string, Skill> GetAllSkills()
        {
            return skills;
        }

        public void PrepareData(Action OnDataReady)
        {
            
        }

        public void UpdateRecord(Record record)
        {
            if (records.ContainsKey(record.guid))
            {
                records[record.guid] = record;
            }
        }

        public void UpdateSkill(Skill skill)
        {
            if (skills.ContainsKey(skill.guid))
            {
                skills[skill.guid] = skill;
            }
        }
    }
}
