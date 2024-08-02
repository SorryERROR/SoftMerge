#pragma warning disable 0649 

using System.Collections;
using UnityEngine;

namespace JMERGE
{
    public abstract class JMergeSettingsAnimation : ScriptableObject
    {
        protected JMergeSettingsPanel jMergeSettingsPanel;
        protected JMergeSettingsPanel.SettingsButtonInfo[] settingsButtonsInfo;

        public void Init(JMergeSettingsPanel jMergeSettingsPanel)
        {
            this.jMergeSettingsPanel = jMergeSettingsPanel;

            settingsButtonsInfo = jMergeSettingsPanel.SettingsButtonsInfo;

            AddExtraComponents();
        }

        protected virtual void AddExtraComponents()
        {

        }

        public abstract IEnumerator Show(AnimationCallback callback);
        public abstract IEnumerator Hide(AnimationCallback callback);

        public delegate void AnimationCallback();
    }
}

// -----------------
// Settings Panel v 0.3
// -----------------