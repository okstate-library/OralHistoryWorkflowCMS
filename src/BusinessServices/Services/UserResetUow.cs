using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    public class UserResetUow : UnitOfWork
    {

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        public RequestModel Request
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public ResponseModel Response
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the collection repository.
        /// </summary>
        /// <value>
        /// The collection repository.
        /// </value>
        private UserRepository UserRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the well known error.
        /// </summary>
        /// <value>
        /// The well known error.
        /// </value>
        private WellKnownErrors WellKnownError
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserResetUow" /> class.
        /// </summary>
        public UserResetUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private UserResetUow(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private class WellKnownErrors : WellKnownServiceErrors
        {
            // <summary>
            /// The invalid username well known error
            /// </summary>
            public static readonly Tuple<int, string> InvalidUsernameWellKnownError =
                new Tuple<int, string>(1, "Invalid username.");

            /// <summary>
            /// The invalid password well known error
            /// </summary>
            public static readonly Tuple<int, string> InvalidPasswordWellKnownError =
                new Tuple<int, string>(2, "Invalid password.");
        }

        /// <summary>
        /// Runs prior to the work being done.
        /// </summary>
        protected override void PreExecute()
        {
            this.WellKnownError = new WellKnownErrors();

            this.UserRepository = new UserRepository();

            this.WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            user selectUser = UserRepository.GetUserById(Request.UserModel.UserId);

            if (selectUser != null)
            {
                UserModel userModel = null;

                if (selectUser != null)
                {
                    selectUser.Password = Encryption.Encrypt("abc123");

                    UserRepository.Edit(selectUser);
                }
                else
                {
                    this.WellKnownError.Value = WellKnownErrors.InvalidUsernameWellKnownError;
                }

                this.Response = new ResponseModel()
                {
                    UserModel = userModel,
                    IsOperationSuccess = true
                };

            }
            else
            {
                this.WellKnownError.Value = WellKnownErrors.InvalidUsernameWellKnownError;
            }           

        }

        /// <summary>
        /// Runs after the work has been done.
        /// </summary>
        protected override void PostExecute()
        {
            int errorCode = this.WellKnownError.Value.Item1;

            if (errorCode > 0)
            {
                this.Response = new ResponseModel()
                {
                    ErrorCode = errorCode.ToString(),

                    ErrorMessage = this.WellKnownError.Value.Item2,

                    IsOperationSuccess = false

                };

            }
        }
    }
}
