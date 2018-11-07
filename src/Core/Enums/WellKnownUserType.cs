using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    /// <summary>
    /// Defines the enumeration values of well known user types.
    /// </summary>
    public enum WellKnownUserType
    {
        /// <summary>
        /// The guest
        /// </summary>
        [Description("Guest user")]
        GuestUser = 1,

        /// <summary>
        /// The Student
        /// </summary>
        [Description("Student")]
        Student = 2,

        /// <summary>
        /// The Interviewer
        /// </summary>
        [Description("Interviewer")]
        Interviewer = 3,

        /// <summary>
        /// The admin
        /// </summary>
        [Description("Admin")]
        AdminUser = 4,
    }
}
