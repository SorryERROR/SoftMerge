﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace JMERGE
{
    public abstract class InitModule : ScriptableObject
    {
        [HideInInspector]
        [SerializeField]
        protected string moduleName;

        public abstract void CreateComponent(Initialiser Initialiser);

        public virtual void StartInit(Initialiser Initialiser) { }

        public InitModule()
        {
            moduleName = "Default Module";
        }
    }
}
