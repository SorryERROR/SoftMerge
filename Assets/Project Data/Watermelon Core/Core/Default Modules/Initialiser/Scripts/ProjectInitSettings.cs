#pragma warning disable 0649

using UnityEngine;

namespace JMERGE
{
    [SetupTab("Init Settings", priority = 1, texture = "icon_puzzle")]
    [CreateAssetMenu(fileName = "Project Init Settings", menuName = "Settings/Project Init Settings")]
    public class ProjectInitSettings : ScriptableObject
    {
        [SerializeField] InitModule[] coreModules;
        public InitModule[] CoreModules => coreModules;

        [SerializeField] InitModule[] initModules;
        public InitModule[] InitModules => initModules;

        public void Initialise(Initialiser initialiser)
        {
            for (int i = 0; i < coreModules.Length; i++)
            {
                coreModules[i].CreateComponent(initialiser);
            }

            for (int i = 0; i < initModules.Length; i++)
            {
                initModules[i].CreateComponent(initialiser);
            }
        }

        public void StartInit(Initialiser initialiser)
        {
            for (int i = 0; i < coreModules.Length; i++)
            {
                coreModules[i].StartInit(initialiser);
            }

            for (int i = 0; i < initModules.Length; i++)
            {
                initModules[i].StartInit(initialiser);
            }
        }
    }
}