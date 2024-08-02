﻿using System;

namespace JMERGE
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DefineAttribute : Attribute
    {
        public string define;

        public DefineAttribute(string define)
        {
            this.define = define;
        }
    }
}
