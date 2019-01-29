using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public class Record
    {
        public string guid { get; private set; }
        public DateTime date { get; private set; }
        public float amount { get; private set; }
        public HashSet<Skill> skills { get; private set; }
        public string originGuid { get; set; }

        public Record(DateTime date, float amount, string originGuid)
        {
            guid = Guid.NewGuid().ToString();
            this.date = date;
            this.amount = amount;
            this.skills = skills;
            this.originGuid = originGuid;
        }

        public Record(DateTime date, float amount, Skill origin)
        {
            guid = Guid.NewGuid().ToString();
            this.date = date;
            this.amount = amount;

            originGuid = origin.guid;
            skills = new HashSet<Skill>();
            skills.Add(origin);
            skills.UnionWith(origin.Ancestors());
        }

        public Record(string guid, DateTime date, float amount, string origin)
        {
            this.guid = guid;
            this.date = date;
            this.amount = amount;
            this.originGuid = origin;
            skills = new HashSet<Skill>();
        }

        public void AddSkills(HashSet<Skill> newSkills)
        {
            skills.UnionWith(newSkills);
        }

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }
    }
}