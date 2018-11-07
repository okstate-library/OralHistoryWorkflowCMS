using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets the is operation success Request Dto.
        /// </summary>
        /// <value>
        /// A string value.
        /// </value>
        public bool IsOperationSuccess
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error message Info Request Dto.
        /// </summary>
        /// <value>
        /// A string value.
        /// </value>
        public string ErrorMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode
        {
            get;
            set;
        }
    }
}
