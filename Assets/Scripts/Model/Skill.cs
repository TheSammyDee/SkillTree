using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkillTree.Model
{
    public class Skill
    {
        public string guid { get; private set; }

        private string _name;
        public string name { get; set; }
        public Color color { get; set; }
        public List<Skill> parents { get; private set; }
        public List<Record> records { get; private set; }

        public UnityAction OnSkillUpdated;

        public Skill(string name)
        {
            guid = Guid.NewGuid().ToString();
            this.name = name;
            parents = new List<Skill>();
            records = new List<Record>();
            color = Color.white;
        }

        public Skill(string guid, string name, Color color, List<Skill> parents)
        {
            this.guid = guid;
            this.name = name;
            this.color = color;
            this.parents = parents;
        }

        /// <summary>
        /// Adds the given <see cref="Skill"/> to this Skill's list of
        /// parents, if it is not already in the list.
        /// </summary>
        /// <param name="skill"></param>
        public void AddParent(Skill skill)
        {
            if (!parents.Contains(skill))
            {
                parents.Add(skill);
            }
        }

        /// <summary>
        /// Removes the given <see cref="Skill"/> from the list of
        /// parent Skills, if it is present.
        /// </summary>
        /// <param name="skill"></param>
        public void RemoveParent(Skill skill)
        {
            if (parents.Contains(skill))
            {
                parents.Remove(skill);
            }
        }

        /// <summary>
        /// Returns true if this <see cref="Skill"/> is a child of
        /// the given Skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool IsChildOf(Skill skill)
        {
            return (parents.Contains(skill));
        }

        public void AddRecord(Record record)
        {

        }

        public void RemoveRecord(Record record)
        {

        }

        public float Total()
        {
            return 0;
        }

        public int Level()
        {
            return 0;
        }

        public float LevelProgress()
        {
            return 0;
        }

        public float LevelRequirement()
        {
            return 0;
        }
    }
}