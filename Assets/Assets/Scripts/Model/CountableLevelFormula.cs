using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace SkillTree.Model
{
    public class CountableLevelFormula : ILevelFormula
    {
        private float equivalentMin;
        private ILevelFormula minFormula;

        public CountableLevelFormula(float equivalentMin, ILevelFormula minFormula)
        {
            this.equivalentMin = equivalentMin;
            this.minFormula = minFormula;
        }

        public float BaseMinutesToUnits(float minutes)
        {
            return Mathf.Round(minutes / equivalentMin);
        }

        public float UnitsToBaseMinutes(float amount)
        {
            return amount * equivalentMin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="total">The total points in this formula's units</param>
        /// <returns></returns>
        public int Level(float total)
        {
            Debugger.Instance.Log("level based on " + total);
            return minFormula.Level(total * equivalentMin);
        }

        public float LevelCompletionRequirement(int level)
        {
            return minFormula.LevelCompletionRequirement(level) / equivalentMin;
        }

        public float LevelCompletionRequirementTotal(int level)
        {
            return minFormula.LevelCompletionRequirementTotal(level) / equivalentMin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="total">The total points in this formula's units</param>
        /// <returns></returns>
        public float LevelProgress(float total)
        {
            return minFormula.LevelProgress(total * equivalentMin) / equivalentMin;
        }
    }
}