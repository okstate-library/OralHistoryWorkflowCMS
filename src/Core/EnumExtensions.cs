using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public class DropdownEnableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DropdownEnableAttribute"/> class.
        /// </summary>
        /// <param name="isVisibleInDropDown">if set to <c>true</c> [is visible in drop down].</param>
        public DropdownEnableAttribute(bool isVisibleInDropDown)
        {
            IsVisibleInDropDown = isVisibleInDropDown;
        }

        public bool IsVisibleInDropDown { get; private set; }

    }

}
