using SkillTree.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.ViewModel
{
    public class SkillsListViewModel
    {
        private SkillViewModel skillViewModelPrefab;
        private List<SkillViewModel> viewModels;
        private GameObject anchor;

        public SkillsListViewModel(SkillViewModel skillViewModelPrefab, SkillCollection skillCollection, GameObject anchor)
        {
            this.skillViewModelPrefab = skillViewModelPrefab;
            this.anchor = anchor;
            viewModels = new List<SkillViewModel>();

            foreach (Skill skill in skillCollection.skills.Values)
            {
                SkillViewModel viewModel = GameObject.Instantiate(skillViewModelPrefab);
                viewModel.Initialize(skill);
                viewModel.gameObject.transform.SetParent(anchor.transform);
            }
        }
    }
}