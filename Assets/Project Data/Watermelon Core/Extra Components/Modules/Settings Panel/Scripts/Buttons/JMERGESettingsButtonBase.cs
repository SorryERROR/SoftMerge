using UnityEngine;

namespace JMERGE
{
    public abstract class JMERGESettingsButtonBase : MonoBehaviour
    {
        private int index;
        protected JMergeSettingsPanel jMergeSettingsPanel;

        private RectTransform rectTransform;
        public RectTransform RectTransform { get { return rectTransform; } }

        public void Init(int index, JMergeSettingsPanel jMergeSettingsPanel)
        {
            this.index = index;
            this.jMergeSettingsPanel = jMergeSettingsPanel;

            this.rectTransform = GetComponent<RectTransform>();
        }

        public abstract bool IsActive();
        public abstract void OnClick();
    }
}

// -----------------
// Settings Panel v 0.3
// -----------------