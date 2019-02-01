using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public interface ILevelFormula
    {
        /// <summary>
        /// Returns the current level based on the given total.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        int Level(float total);

        /// <summary>
        /// Returns the value accrued since reaching the current level.
        /// </summary>
        /// <param name="total"></param>
        /// <returns></returns>
        float LevelProgress(float total);

        /// <summary>
        /// Returns the total value required to complete the current level
        /// and reach the next level
        /// </summary>
        /// /// <param name="level"></param>
        /// <returns></returns>
        float LevelCompletionRequirementTotal(int level);

        /// <summary>
        /// Returns the value required to go from the current level
        /// to the next level.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        float LevelCompletionRequirement(int level);
    }
}
