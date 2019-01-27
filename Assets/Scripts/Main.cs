using SkillTree.Model;
using SkillTree.Tests;
using SkillTree.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree
{
    public class Main : MonoBehaviour
    {
        [SerializeField]
        SkillViewModel listSkillViewModelPrefab;

        [SerializeField]
        GameObject listViewAnchor;

        void Start()
        {
            SkillCollection skillCollection = new SkillCollection(new MockSkillsDataSource(), new MockLevelFormula());
            SkillsListViewModel listViewModel = new SkillsListViewModel(listSkillViewModelPrefab, skillCollection, listViewAnchor);
        }
    }
}