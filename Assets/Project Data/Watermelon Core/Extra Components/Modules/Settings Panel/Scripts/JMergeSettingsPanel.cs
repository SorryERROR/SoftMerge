#pragma warning disable 0414, 0649

using UnityEngine;
using UnityEngine.Serialization;

namespace JMERGE
{
    public class JMergeSettingsPanel : MonoBehaviour
    {
        private static JMergeSettingsPanel instance;

        [FormerlySerializedAs("settingsAnimation")]
        [DrawReference]
        [SerializeField] JMergeSettingsAnimation jMergeSettingsAnimation;

        [Space]
        [SerializeField] UIScalableObject mainRect;

        [Header("Panel Paddings")]
        [SerializeField] float xPanelPosition;
        [SerializeField] float yPanelPosition;

        [Header("Element Paddings")]
        [SerializeField] float elementSpace;

        [SerializeField] SettingsButtonInfo[] settingsButtonsInfo;
        public SettingsButtonInfo[] SettingsButtonsInfo
        {
            get { return settingsButtonsInfo; }
        }

        private bool isActiveSettingsButton = false;
        public bool IsActiveSettingsButton => isActiveSettingsButton;

        private static bool IsPanelActive { get; set; }
        private static UIScalableObject MainRect => instance.mainRect;

        private bool isAnimationActive = false;

        private Vector2[] buttonPositions;
        public Vector2[] ButtonPositions
        {
            get { return buttonPositions; }
        }

        private void Awake()
        {
            instance = this;

            // Disable all buttons
            for (int i = 0; i < settingsButtonsInfo.Length; i++)
            {
                if (settingsButtonsInfo[i].JmergeSettingsButton == null)
                    continue;
                
                settingsButtonsInfo[i].JmergeSettingsButton.gameObject.SetActive(false);
            }

            InitAnimation();
            InitPositions();

        }

        public void InitAnimation()
        {
            jMergeSettingsAnimation.Init(this);
        }

        public void InitPositions()
        {
            Vector2 lastPosition = new Vector2(xPanelPosition, yPanelPosition);

            buttonPositions = new Vector2[settingsButtonsInfo.Length];
            for (int i = 0; i < buttonPositions.Length; i++)
            {
                if (settingsButtonsInfo[i].JmergeSettingsButton != null)
                {
                    settingsButtonsInfo[i].JmergeSettingsButton.Init(i, this);

                    if (settingsButtonsInfo[i].JmergeSettingsButton.IsActive())
                    {
#if UNITY_EDITOR
                        if (!Application.isPlaying)
                            settingsButtonsInfo[i].JmergeSettingsButton.RectTransform.gameObject.SetActive(true);
#endif

                        RectTransform button = settingsButtonsInfo[i].JmergeSettingsButton.RectTransform;

                        Vector2 buttonPosition = lastPosition;

                        lastPosition -= new Vector2(0, elementSpace);

                        button.anchoredPosition = new Vector2(xPanelPosition, buttonPosition.y);

                        buttonPositions[i] = buttonPosition;
                    }
                    else
                    {
#if UNITY_EDITOR
                        if (!Application.isPlaying)
                            settingsButtonsInfo[i].JmergeSettingsButton.RectTransform.gameObject.SetActive(false);
#endif

                        buttonPositions[i] = Vector3.zero;
                    }
                }
                else
                {
                    Debug.Log("[Settings Panel]: Button reference is missing!");
                }
            }
        }

        public void SettingsButton()
        {
            if (isAnimationActive)
                return;

            if (isActiveSettingsButton)
            {
                Hide();

                isActiveSettingsButton = false;
            }
            else
            {
                Show();

                isActiveSettingsButton = true;
            }

            AudioController.PlaySound(AudioController.Sounds.buttonSound);
        }

        public void Show()
        {
            isAnimationActive = true;

            StartCoroutine(jMergeSettingsAnimation.Show(delegate
            {
                isAnimationActive = false;
            }));
        }

        public void Hide(bool immediately = false)
        {
            if (!immediately)
            {
                isAnimationActive = true;

                StartCoroutine(jMergeSettingsAnimation.Hide(delegate
                {
                    isAnimationActive = false;
                }));

                isActiveSettingsButton = false;
                return;
            }

            for (int i = settingsButtonsInfo.Length - 1; i >= 0; i--)
            {
                settingsButtonsInfo[i].JmergeSettingsButton.gameObject.SetActive(false);
            }

            isActiveSettingsButton = false;
        }

        public static void ShowPanel(bool immediately = false)
        {
            if (IsPanelActive)
                return;

            IsPanelActive = true;

            MainRect.Show(immediately: immediately);
        }

        public static void HidePanel(bool immediately = false)
        {
            if (!IsPanelActive)
                return;

            IsPanelActive = false;

            if (instance != null)
                instance.Hide(immediately);

            OnSettingButtonsHidden(immediately);

        }

        private static void OnSettingButtonsHidden(bool immediately = false)
        {
            MainRect.Hide(immediately: immediately);
        }

        [System.Serializable]
        public class SettingsButtonInfo
        {
            public JMERGESettingsButtonBase JmergeSettingsButton { get => jmergeSettingsButton; set => jmergeSettingsButton = value; }
            [FormerlySerializedAs("settingsButton")] [SerializeField] JMERGESettingsButtonBase jmergeSettingsButton;
        }
    }
}

// -----------------
// Settings Panel v 0.3.1
// -----------------

// Changelog
// v 0.3.1
// • Added animations
// v 0.3
// • Added button sounds
// v 0.2.1
// • Removed GDPR controller refference from GDPR button
// • Added extra check for IAP button (don't show it if the product already purchased)
// v 0.2
// • Fast toggle fix
// v 0.1
// • Added basic version
// • Added example prefab