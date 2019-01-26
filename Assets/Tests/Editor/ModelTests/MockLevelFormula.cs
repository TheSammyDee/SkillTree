using SkillTree.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockLevelFormula : ILevelFormula
{
    public int Level(float total)
    {
        return (int)(total / 10) + 1;
    }

    public float LevelProgress(float total)
    {
        int level = Level(total);
        float previousLevels = LevelCompletionRequirement(level - 1);

        return total - previousLevels;
    }

    public float LevelCompletionRequirement(int level)
    {
        return 10 * level;
    }
}
