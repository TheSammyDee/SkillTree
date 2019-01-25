using NUnit.Framework;
using SkillTree.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Tests
{
    public class SkillTests
    {
        [Test]
        public void Skill_NewSkill()
        {
            Skill skill = new Skill("skill");
            Assert.AreEqual("skill", skill.name);
            Assert.AreEqual(0, skill.records.Count);
            Assert.AreEqual(0, skill.parents.Count);
        }

        [Test]
        public void Skill_AddParent()
        {
            Skill child = new Skill("child");
            Skill parent = new Skill("parent");

            //Assert.AreEqual
            child.AddParent(parent);
        }
    }
}
