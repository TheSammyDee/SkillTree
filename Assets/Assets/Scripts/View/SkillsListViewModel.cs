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
        private List<RecordViewModel> recordViewModels;
        private GameObject anchor;
        private TimeInputManager timeInput;
        SkillCollection skillCollection;

        public SkillsListViewModel(SkillCollection skillCollection, TimeInputManager timeInput, GameObject anchor)
        {
            this.anchor = anchor;
            this.timeInput = timeInput;
            viewModels = new List<SkillViewModel>();
            recordViewModels = new List<RecordViewModel>();
            this.skillCollection = skillCollection;
            skillCollection.OnSkillAdded += NewSkill;
        }

        private void NewRecordTimeInput(Skill skill)
        {
            timeInput.GetTimeInput((x) => CreateNewRecord(skill, (float)x), null);
        }

        private void CreateNewRecord(Skill skill, float amount)
        {
            skillCollection.AddRecord(DateTime.Today, amount, skill);
        }

        private void NewSkill(Skill skill)
        {
            SkillViewModel viewModel = GameObject.Instantiate(Prefabs.Instance.listSkillViewModelPrefab);
            viewModel.Initialize(skill);
            viewModel.gameObject.transform.SetParent(anchor.transform);
            viewModel.OnAddButtonClicked += NewRecordTimeInput;
            viewModel.OnSeeRecordsClicked += SeeRecords;
        }

        private void SeeRecords(Skill skill)
        {
            foreach (SkillViewModel vm in viewModels)
            {
                vm.Destroy();
            }
            viewModels.Clear();
        }
    }
}