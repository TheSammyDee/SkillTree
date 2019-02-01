using SkillTree.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Tests
{
    public class MockLevelFormula : ILevelFormula
    {
        public int Level(float total)
        {
            return (int)(total / 10) + 1;
        }

        public float LevelProgress(float total)
        {
            int level = Level(total);
            float previousLevels = LevelCompletionRequirementTotal(level - 1);

            return total - previousLevels;
        }

        public float LevelCompletionRequirementTotal(int level)
        {
            return 10 * level;
        }

        public float LevelCompletionRequirement(int level)
        {
            return LevelCompletionRequirementTotal(level) - LevelCompletionRequirementTotal(level - 1);
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