using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SkillTree.Model
{
    public class Skill
    {
        public string guid { get; private set; }
        public string name { get; set; }
        public Color color { get; set; }
        public HashSet<Skill> parents { get; private set; }
        public List<Record> records { get; private set; }
        public ILevelFormula formula { get; set; }

        public UnityAction OnAmountUpdated;

        public Skill(string name, ILevelFormula formula)
        {
            guid = Guid.NewGuid().ToString();
            this.name = name;
            this.formula = formula;
            parents = new HashSet<Skill>();
            records = new List<Record>();
            color = Color.white;
        }

        public Skill(string guid, string name, Color color, HashSet<Skill> parents)
        {
            this.guid = guid;
            this.name = name;
            this.color = color;
            this.parents = parents;
            records = new List<Record>();
        }

        public Skill(string guid, string name, ILevelFormula formula, Color color, HashSet<Skill> parents)
        {
            this.guid = guid;
            this.name = name;
            this.formula = formula;
            this.color = color;
            this.parents = parents;
            records = new List<Record>();
        }

        /// <summary>
        /// Adds the given <see cref="Skill"/> to this Skill's list of
        /// parents and returns True, if it is not already in the list, 
        /// and if this Skill is not an ancestor of the given Skill. 
        /// Otherwise returns false;
        /// </summary>
        /// <param name="skill"></param>
        /// <returns>True if the Skill is added</returns>
        public bool AddParent(Skill skill)
        {
            if (!skill.AncestryContains(this))
            {
                return parents.Add(skill);
            }

            return false;
        }

        /// <summary>
        /// Removes the given <see cref="Skill"/> from the list of
        /// parent Skills, if it is present.
        /// </summary>
        /// <param name="skill"></param>
        public void RemoveParent(Skill skill)
        {
            parents.Remove(skill);
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

        /// <summary>
        /// Returns true if the given <see cref="Skill"/> is an ancestor
        /// of this Skill.
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool AncestryContains(Skill skill)
        {
            if (parents.Count == 0)
            {
                return false;
            }
            else if (IsChildOf(skill))
            {
                return true;
            }
            else
            {
                foreach (Skill parent in parents)
                {
                    if (parent.AncestryContains(skill))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public HashSet<Skill> Ancestors()
        {
            HashSet<Skill> ancestors = new HashSet<Skill>();
            ancestors.UnionWith(parents);

            foreach (Skill parent in parents)
            {
                ancestors.UnionWith(parent.Ancestors());
            }

            return ancestors;
        }

        // TODO: Figure this shit out between hashsets and lists
        // and single or multiple add and firing events

        /// <summary>
        /// Adds the given <see cref="Record"/> to this <see cref="Skill"/>'s 
        /// list of records, if this Record contains a reference to this Skill
        /// and if it does not already exist in the list.
        /// </summary>
        /// <param name="record"></param>
        public void AddRecord(Record record)
        {
            // TODO: use hashset here?
            if (!records.Contains(record) && record.skills.Contains(this))
            {
                records.Add(record);

                if (OnAmountUpdated != null)
                    OnAmountUpdated();
            }
        }

        public void AddRecords(List<Record> records)
        {
            foreach (Record record in records)
            {
                AddRecord(record);
            }

            if (OnAmountUpdated != null)
                OnAmountUpdated();
        }

        // TODO: Make remove like add
        public void RemoveRecord(Record record)
        {
            records.Remove(record);

            if (OnAmountUpdated != null)
                OnAmountUpdated();
        }

        // TODO: Store total for reuse

        /// <summary>
        /// Returns the total value accruded in this <see cref="Skill"/>
        /// </summary>
        /// <returns></returns>
        public float Total()
        {
            return records.Sum(x => x.amount);
        }

        // TODO: Improve efficiency here with repeated calculations

        /// <summary>
        /// Returns the current level of this <see cref="Skill"/>
        /// </summary>
        /// <returns></returns>
        public int Level()
        {
            return formula.Level(Total());
        }

        /// <summary>
        /// Returns the value accrued since reaching the current level
        /// </summary>
        /// <returns></returns>
        public float LevelProgress()
        {
            return formula.LevelProgress(Total());
        }

        /// <summary>
        /// Returns the value required to complete the current level
        /// and reach the next level
        /// </summary>
        /// <returns></returns>
        public float LevelCompletionRequirementTotal()
        {
            return formula.LevelCompletionRequirementTotal(Level());
        }

        public float LevelCompletionRequirement()
        {
            return formula.LevelCompletionRequirement(Level());
        }
    }
}