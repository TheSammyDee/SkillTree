using SkillTree.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkillTree.ViewModel
{
    public class ListRecordViewModel : RecordViewModel
    {
        [SerializeField]
        private TextMeshProUGUI date;

        [SerializeField]
        private TextMeshProUGUI amount;

        [SerializeField]
        private Button editButton;

        [SerializeField]
        private Button deleteButton;

        public override void Initialize(Record record)
        {
            base.Initialize(record);
            editButton.onClick.AddListener(OnEditButtonClickedInternal);
            deleteButton.onClick.AddListener(OnDeleteButtonClickedInternal);
            record.OnAmountUpdated += UpdateData;

            UpdateData();
        }

        public override void Destroy()
        {
            editButton.onClick.RemoveAllListeners();
            deleteButton.onClick.RemoveAllListeners();
            base.Destroy();
        }

        private void UpdateData()
        {
            date.text = record.date.ToShortDateString();
            amount.text = ((int)record.amount).ToString();
        }
    }
}