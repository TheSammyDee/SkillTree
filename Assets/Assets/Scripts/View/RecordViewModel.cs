using SkillTree.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillTree.ViewModel
{
    public abstract class RecordViewModel : MonoBehaviour
    {
        public event Action<string> OnEditButtonClicked;
        public event Action<Record> OnDeleteButtonClicked;
        protected Record record;

        public virtual void Initialize(Record record)
        {
            this.record = record;
            record.OnAmountUpdated += UpdateAmount;
        }

        public virtual void Destroy()
        {
            record.OnAmountUpdated -= UpdateAmount;
            GameObject.Destroy(gameObject);
        }

        protected virtual void UpdateAmount() { }

        protected void OnEditButtonClickedInternal()
        {
            if (OnEditButtonClicked != null)
                OnEditButtonClicked(record.guid);
        }

        protected void OnDeleteButtonClickedInternal()
        {
            if (OnDeleteButtonClicked != null)
                OnDeleteButtonClicked(record);
        }
    }
}