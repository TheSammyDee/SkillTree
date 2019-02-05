using SkillTree.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.ViewModel
{
    public abstract class SkillViewModel : MonoBehaviour
    {
        public event Action<Skill> OnAddButtonClicked;
        public event Action<Skill> OnSeeRecordsClicked;
        protected Skill skill;

        public virtual void Initialize(Skill skill)
        {
            this.skill = skill;
            skill.OnAmountUpdated += UpdateAmount;
        }

        public virtual void Destroy()
        {
            skill.OnAmountUpdated -= UpdateAmount;
            GameObject.Destroy(gameObject);
        }

        public void OnAddButtonClickedInternal()
        {
            if (OnAddButtonClicked != null)
                OnAddButtonClicked(skill);
        }

        public void OnSeeRecordsClickedInternal()
        {
            if (OnSeeRecordsClicked != null)
                OnSeeRecordsClicked(skill);
        }

        protected virtual void UpdateAmount() { }
    }
}