using NUnit.Framework;
using SkillTree.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SkillTree.Tests
{
    public class SkillCollectionTests
    {
        [Test]
        public void SkillCollection_AddRecord()
        {
            ISkillsDataSource database = new MockSkillsDataSource();
            SkillCollection skillCollection = new SkillCollection(database, new MockLevelFormula(), new MockLevelFormula());
            Dictionary<string, Skill> skills = database.GetAllSkills();

            Skill a = skills["a"];
            Skill abe = skills["abe"];
            Skill e = skills["e"];
            Skill c = skills["c"];
            Skill cef = skills["cef"];
            Skill f = skills["f"];

            skillCollection.AddRecord(DateTime.Today, 15f, a);
            Record record = database.GetAllRecords().Select(x => x.Value).Where(y => y.originGuid == a.guid).First();
            Assert.AreSame(record, a.records[0]);
            Assert.AreSame(record, abe.records[0]);
            Assert.AreEqual(15f, abe.records.Sum(x => x.amount));
            Assert.AreEqual(0, e.records.Count);

            skillCollection.AddRecord(DateTime.Today, 22f, c);
            Record record2 = database.GetAllRecords().Select(x => x.Value).Where(y => y.originGuid == c.guid).First();
            Assert.AreSame(record2, abe.records[1]);
            Assert.AreSame(record2, f.records[0]);
            Assert.AreEqual(1, cef.records.Count);
        }

        [Test]
        public void SkillCollection_AddSkill()
        {
            ISkillsDataSource database = new MockSkillsDataSource();
            SkillCollection skillCollection = new SkillCollection(database, new MockLevelFormula(), new MockLevelFormula());
            Dictionary<string, Skill> skills = database.GetAllSkills();

            Skill f = skills["f"];
            Skill g = skills["g"];

            skillCollection.AddSkill("newSkill", Color.black, new List<Skill>() { f, g });
            Skill newSkill = skills.Select(x => x.Value).Where(y => y.name == "newSkill").First();
            skillCollection.AddRecord(DateTime.Today, 25f, newSkill);
            Assert.AreEqual(newSkill.records[0], g.records[0]);
        }
    }
}