#pragma warning disable 649

using UnityEngine;
using UnityEngine.UI;

namespace JMERGE
{
    public class JMergeCloseApp : JMERGESettingsButtonBase
    {
        [SerializeField] Image imageRef;

        [Space]
        [SerializeField] Sprite activeSprite;
        [SerializeField] Sprite disableSprite;

        private bool isActive = true;

        public override bool IsActive()
        {
            return true;
        }

        public override void OnClick()
        {
            Application.Quit();
        }
    }
}

// -----------------
// Settings Panel v 0.3
// -----------------