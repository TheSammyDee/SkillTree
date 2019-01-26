using NUnit.Framework;
using SkillTree.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SkillTree.Tests
{
    public class SkillTests
    {
        [Test]
        public void Skill_NewSkill()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill skill = new Skill("skill", formula);
            Assert.AreEqual("skill", skill.name);
            Assert.AreEqual(0, skill.records.Count);
            Assert.AreEqual(0, skill.parents.Count);
        }

        [Test]
        public void Skill_AddParent()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill child = new Skill("child", formula);
            Skill parent = new Skill("parent", formula);

            child.AddParent(parent);
            Assert.AreSame(parent, child.parents.First());
            child.AddParent(parent);
            Assert.AreEqual(1, child.parents.Count);
        }

        [Test]
        public void Skill_AncestoryContains()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill child = new Skill("child", formula);
            Skill parent = new Skill("parent", formula);
            Skill otherParent = new Skill("otherParent", formula);
            Skill grandparent = new Skill("grandparent", formula);
            Skill uncle = new Skill("uncle", formula);

            child.AddParent(parent);
            parent.AddParent(grandparent);
            uncle.AddParent(grandparent);
            Assert.IsTrue(child.AncestryContains(grandparent));
            Assert.IsFalse(child.AncestryContains(uncle));
            otherParent.AddParent(grandparent);
            child.AddParent(otherParent);
            Assert.IsTrue(child.AncestryContains(grandparent));
        }

        [Test]
        public void Skill_PreventCircularParents()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill child = new Skill("child", formula);
            Skill parent = new Skill("parent", formula);
            Skill grandparent = new Skill("grandparent", formula);

            child.AddParent(parent);
            Assert.False(parent.AddParent(child));
            Assert.AreEqual(0, parent.parents.Count);
            parent.AddParent(grandparent);
            Assert.False(grandparent.AddParent(child));
        }

        [Test]
        public void Skill_IsChildOf()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill child = new Skill("child", formula);
            Skill parent = new Skill("parent", formula);
            Skill uncle = new Skill("uncle", formula);

            child.AddParent(parent);
            Assert.True(child.IsChildOf(parent));
            Assert.False(child.IsChildOf(uncle));
        }

        [Test]
        public void Skill_AddRecord()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill skill = new Skill("skill", formula);
            Skill otherSkill = new Skill("otherSkill", formula);
            Record record = new Record(DateTime.Today, 10f, skill);
            Record record2 = new Record(DateTime.Today, 20f, skill);
            skill.AddRecords(new List<Record>() { record, record2 });
            Assert.True(skill.records.Contains(record));
            Assert.True(skill.records.Contains(record2));
            Assert.AreEqual(2, skill.records.Count);
            Record otherRecord = new Record(DateTime.Today, 10f, otherSkill);
            skill.AddRecords(new List<Record>() { otherRecord });
            Assert.AreEqual(2, skill.records.Count);
            skill.AddRecords(new List<Record>() { record });
            Assert.AreEqual(2, skill.records.Count);
        }

        [Test]
        public void Skill_Total()
        {
            Skill skill = BuildTestSkill();
            Assert.AreEqual(175f, skill.Total());
        }

        [Test]
        public void Skill_Level()
        {
            Skill skill = BuildTestSkill();
            Assert.AreEqual(18, skill.Level());
        }

        [Test]
        public void Skill_LevelProgress()
        {
            Skill skill = BuildTestSkill();
            Assert.AreEqual(5, skill.LevelProgress());
        }

        [Test]
        public void Skill_LevelRequirement()
        {
            Skill skill = BuildTestSkill();
            Assert.AreEqual(180, skill.LevelCompletionRequirement());
        }

        private bool fired;

        [Test]
        public void Skill_FireOnUpdated()
        {
            fired = false;

            ILevelFormula formula = new MockLevelFormula();
            Skill skill = new Skill("skill", formula);
            skill.OnUpdated += () => { fired = true; };
            Record r1 = new Record(DateTime.Today, 10f, skill);
            skill.AddRecords(new List<Record>() { r1 });
            Assert.IsTrue(fired);
            fired = false;
            skill.RemoveRecord(r1);
            Assert.IsTrue(fired);
        }

        private Skill BuildTestSkill()
        {
            ILevelFormula formula = new MockLevelFormula();
            Skill skill = new Skill("skill", formula);
            Assert.AreEqual(0, skill.Total());
            Record r1 = new Record(DateTime.Today, 10f, skill);
            Record r2 = new Record(DateTime.Today, 45f, skill);
            Record r3 = new Record(DateTime.Today, 120f, skill);
            skill.AddRecords(new List<Record>() { r1, r2, r3 });
            return skill;
        }
    }
}
