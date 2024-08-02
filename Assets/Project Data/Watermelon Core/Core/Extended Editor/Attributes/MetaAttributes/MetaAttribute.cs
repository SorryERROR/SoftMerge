using System;

namespace JMERGE
{
    public abstract class MetaAttribute : ExtendedEditorAttribute
    {
        private int order = 0;

        public int Order
        {
            get
            {
                return this.order;
            }
            set
            {
                this.order = value;
            }
        }
    }
}