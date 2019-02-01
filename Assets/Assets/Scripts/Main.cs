using SkillTree.App;
using SkillTree.Model;
using SkillTree.Tests;
using SkillTree.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkillTree
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        bool staging;

        [SerializeField]
        Prefabs prefabsPrefab;

        [SerializeField]
        Canvas canvas;

        [SerializeField]
        GameObject listViewAnchor;

        void Start()
        {
            Prefabs prefabs = GameObject.Instantiate(prefabsPrefab);
            string user = staging ? "test" : "TheSammyDee";
            ILevelFormula minutesFormula = new Quadratic500LevelFormula();
            ILevelFormula countableFormula = new CountableLevelFormula(20, minutesFormula);

            TimeInputManager timeInput = new TimeInputManager(GameObject.Instantiate(prefabs.timeInputViewModel), canvas);
            SkillCollection skillCollection = new SkillCollection(
                new JsonDatabase(user, minutesFormula, countableFormula), 
                minutesFormula, 
                countableFormula);
            SkillsListViewModel listViewModel = new SkillsListViewModel(skillCollection, timeInput, listViewAnchor);
        }
    }
}