using BusinessServices.Services;
using EntityData;
using Model.Transfer;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to GetUserUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class GetUserUow : UnitOfWork
    {

        /// <summary>
        /// gets and sets the request model
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
        /// Gets or sets the TranscriptionRepository repository.
        /// </summary>
        /// <value>
        /// The TranscriptionRepository repository.
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
        /// Initializes a new instance of the <see cref="GetUserUow" /> class.
        /// </summary>
        public GetUserUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetUserUow(bool isReadOnly)
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
            this.WellKnownError = new WellKnownErrors();

            this.UserRepository = new UserRepository();

            this.WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            user user = this.UserRepository.GetUserById(this.Request.UserModel.UserId);

            this.Response = new ResponseModel()
            {
                UserModel = Util.ConvertToUserModel(user),
                IsOperationSuccess = true
            };

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
