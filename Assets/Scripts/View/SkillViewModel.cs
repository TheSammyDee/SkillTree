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
        protected Skill skill;

        public virtual void Initialize(Skill skill)
        {
            this.skill = skill;
            skill.OnAmountUpdated += UpdateAmount;
        }

        public void OnAddButtonClickedInternal()
        {
            if (OnAddButtonClicked != null)
                OnAddButtonClicked(skill);
        }

        protected virtual void UpdateAmount() { }
    }
}