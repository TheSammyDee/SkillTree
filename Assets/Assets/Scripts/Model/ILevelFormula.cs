using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.Model
{
    public interface ILevelFormula
    {
        int Level(float total);
        float LevelProgress(float total);
        float LevelCompletionRequirementTotal(int level);
        float LevelCompletionRequirement(int level);
    }
}
