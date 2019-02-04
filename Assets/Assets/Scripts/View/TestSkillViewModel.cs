using SkillTree.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace SkillTree.ViewModel
{
    public class TestSkillViewModel : SkillViewModel
    {
        [SerializeField]
        private TextMeshProUGUI nameText;

        [SerializeField]
        private TextMeshProUGUI level;

        [SerializeField]
        private Image bar;

        [SerializeField]
        private TextMeshProUGUI total;

        public override void Initialize(Skill skill)
        {
            base.Initialize(skill);
            nameText.text = skill.name;
            bar.color = skill.CalculatedColor();
            UpdateAmount();
        }
        
        protected override void UpdateAmount()
        {
            level.text = skill.Level().ToString();
            total.text = skill.Total().ToString();
            bar.fillAmount = skill.LevelProgress() / skill.LevelCompletionRequirement();
        }
    }
}