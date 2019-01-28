using SkillTree.App;
using SkillTree.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.ViewModel
{
    public class SkillsListViewModel
    {
        private List<SkillViewModel> viewModels;
        private GameObject anchor;
        private TimeInputManager timeInput;
        SkillCollection skillCollection;

        public SkillsListViewModel(SkillCollection skillCollection, TimeInputManager timeInput, GameObject anchor)
        {
            this.anchor = anchor;
            this.timeInput = timeInput;
            viewModels = new List<SkillViewModel>();
            this.skillCollection = skillCollection;

            foreach (Skill skill in skillCollection.skills.Values)
            {
                SkillViewModel viewModel = GameObject.Instantiate(Prefabs.Instance.listSkillViewModelPrefab);
                viewModel.Initialize(skill);
                viewModel.gameObject.transform.SetParent(anchor.transform);
                viewModel.OnAddButtonClicked += NewRecordTimeInput;
            }
        }

        private void NewRecordTimeInput(Skill skill)
        {
            timeInput.GetTimeInput((x) => CreateNewRecord(skill, (float)x), null);
        }

        private void CreateNewRecord(Skill skill, float amount)
        {
            skillCollection.AddRecord(DateTime.Today, amount, skill);
        }
    }
}