﻿#region Imports

using System;
using System.ComponentModel;


#endregion

namespace BusinessServices
{

    /// <summary>
    /// Contains operation data.
    /// </summary>
    internal class WellKnownServiceErrors
    {
        public Tuple<int, string> Value
        {
            get;
            set;
        }

        /// <summary>
        /// The no error
        /// </summary>
        public readonly Tuple<int, string> NoError = new Tuple<int, string>(0, "No Error");

        /// <summary>
        /// The missing parameter well known service error
        /// </summary>
        public readonly Tuple<int, string> MissingParameterWellKnownServiceError =
            new Tuple<int, string>(95, "Required Parameter(s) Missing : Unable to identify parameter");


    }

}