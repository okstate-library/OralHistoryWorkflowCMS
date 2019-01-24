using Core;
using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;
using System;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to UserSigninUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    public class UserSigninUow : UnitOfWork
    {

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
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
        /// Initializes a new instance of the <see cref="UserSigninUow" /> class.
        /// </summary>
        public UserSigninUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private UserSigninUow(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="BusinessServices.WellKnownServiceErrors" />
        private class WellKnownErrors : WellKnownServiceErrors
        {
            // <summary>
            /// <summary>
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
            WellKnownError = new WellKnownErrors();

            UserRepository = new UserRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            user selectUser = UserRepository.GetUser(Request.UserModel.Username);

            if (selectUser != null)
            {
                UserModel userModel = null;

                if (selectUser != null)
                {
                    string password = Encryption.convertToUNSecureString(Request.UserModel.Password);

                    string encriptedPassword = Encryption.Encrypt(password);

                    if (string.Equals(encriptedPassword, selectUser.Password))
                    {
                        userModel = new UserModel()
                        {
                            UserId = selectUser.UserId,
                            Name = selectUser.Name,
                            UserType = (byte)selectUser.UserType,
                        };
                    }
                    else
                    {
                        WellKnownError.Value = WellKnownErrors.InvalidPasswordWellKnownError;
                    }
                }
                else
                {
                    WellKnownError.Value = WellKnownErrors.InvalidUsernameWellKnownError;
                }

                Response = new ResponseModel()
                {
                    UserModel = userModel,
                    IsOperationSuccess = true
                };

            }
            else
            {
                WellKnownError.Value = WellKnownErrors.InvalidUsernameWellKnownError;
            }           

        }

        /// <summary>
        /// Runs after the work has been done.
        /// </summary>
        protected override void PostExecute()
        {
            int errorCode = WellKnownError.Value.Item1;

            if (errorCode > 0)
            {
                Response = new ResponseModel()
                {
                    ErrorCode = errorCode.ToString(),

                    ErrorMessage = WellKnownError.Value.Item2,

                    IsOperationSuccess = false

                };

            }
        }
    }
}
