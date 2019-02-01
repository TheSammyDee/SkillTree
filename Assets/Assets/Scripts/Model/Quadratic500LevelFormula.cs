using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public class Quadratic500LevelFormula : ILevelFormula
    {
        public int Level(float total)
        {
            return (int)Mathf.Sqrt(((5f / 12f) * total));
        }

        public float LevelCompletionRequirement(int level)
        {
            return LevelCompletionRequirementTotal(level) - LevelCompletionRequirementTotal(level - 1);
        }

        public float LevelCompletionRequirementTotal(int level)
        {
            return Mathf.Pow(level + 1, 2) * (12f / 5f);
        }

        public float LevelProgress(float total)
        {
            return total - LevelCompletionRequirementTotal(Level(total) - 1);
        }

        public float BaseMinutesToUnits(float minutes)
        {
            return minutes;
        }

        public float UnitsToBaseMinutes(float amount)
        {
            return amount;
        }
    }
}