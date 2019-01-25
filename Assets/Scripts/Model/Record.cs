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
        public List<Skill> skills { get; private set; }

        public Record(DateTime date, float amount, List<Skill> skills)
        {
            guid = Guid.NewGuid().ToString();
            this.date = date;
            this.amount = amount;
            this.skills = skills;
        }
    }
}