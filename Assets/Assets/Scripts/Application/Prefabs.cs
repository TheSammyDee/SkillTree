using SkillTree.ViewModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace SkillTree.App
{
    public class Prefabs : SingletonBehaviour<Prefabs>
    {
        [SerializeField]
        private SkillViewModel _listSkillViewModelPrefab;

        public SkillViewModel listSkillViewModelPrefab { get { return _listSkillViewModelPrefab; } }

        [SerializeField]
        private RecordViewModel _listRecordViewModelPrefab;

        public RecordViewModel listRecordViewModelPrefab { get { return _listRecordViewModelPrefab; } }

        [SerializeField]
        private TimeInputViewModel _timeInputViewModelPrefab;

        public TimeInputViewModel timeInputViewModel { get { return _timeInputViewModelPrefab; } }
    }
}