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
        Prefabs prefabsPrefab;

        [SerializeField]
        Canvas canvas;

        [SerializeField]
        GameObject listViewAnchor;

        void Start()
        {
            Prefabs prefabs = GameObject.Instantiate(prefabsPrefab);

            TimeInputManager timeInput = new TimeInputManager(GameObject.Instantiate(prefabs.timeInputViewModel), canvas);
            SkillCollection skillCollection = new SkillCollection(new JsonDatabase("TheSammyDee"), new MockLevelFormula());
            SkillsListViewModel listViewModel = new SkillsListViewModel(skillCollection, timeInput, listViewAnchor);
        }
    }
}