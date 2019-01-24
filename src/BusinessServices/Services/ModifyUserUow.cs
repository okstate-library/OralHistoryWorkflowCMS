using BusinessServices.Services;
using Core;
using EntityData;
using Model.Transfer;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to ModifyUserUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class ModifyUserUow : UnitOfWork
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
        /// Initializes a new instance of the <see cref="ModifyUserUow" /> class.
        /// </summary>
        public ModifyUserUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private ModifyUserUow(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="BusinessServices.WellKnownServiceErrors" />
        private class WellKnownErrors : WellKnownServiceErrors
        {
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
            user daUser = null;

            switch (Request.WellKnownModificationType)
            {
                case Core.Enums.WellKnownModificationType.Add:

                    UserRepository.Add(Util.ConvertToUser(Request.UserModel));
                    UserRepository.Save();

                    break;
                case Core.Enums.WellKnownModificationType.Edit:

                    daUser = UserRepository.GetUserToEdit(Request.UserModel.UserId);

                    daUser = Util.ConvertToUser(daUser, Request.UserModel);

                    UserRepository.Edit(daUser);
                    UserRepository.Save();

                    break;
                case Core.Enums.WellKnownModificationType.Delete:

                    daUser = UserRepository.GetUserToEdit(Request.UserModel.UserId);

                    UserRepository.Delete(daUser);
                    UserRepository.Save();
                    break;
                case Core.Enums.WellKnownModificationType.Reset:

                    daUser = UserRepository.GetUserToEdit(Request.UserModel.UserId);

                    daUser.Password = Encryption.Encrypt("abc123");
                    UserRepository.Edit(daUser);
                    UserRepository.Save();
                    break;
                default:
                    break;
            }

            Response = new ResponseModel()
            {
                IsOperationSuccess = true
            };

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
