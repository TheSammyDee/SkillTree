using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager {

    XP_Database database;
    List<Skill> skills;

    public SkillManager() {
        database = new XP_Database();
        skills = database.GetAllSkills();

        Test();
    }

    private void Test() {
        
        Skill testSkill = new Skill(0, "Mage", new Color(1.0f, 0f, 0f), new List<long>());
        skills.Add(testSkill);
        database.InsertSkill(testSkill);
        Debug.Log(skills[0].name);
    }

}
