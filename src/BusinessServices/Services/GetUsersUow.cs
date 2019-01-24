using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Repository.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to GetUsersUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class GetUsersUow : UnitOfWork
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
        /// Initializes a new instance of the <see cref="GetUsersUow" /> class.
        /// </summary>
        public GetUsersUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetUsersUow(bool isReadOnly)
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
            List<UserModel> newlist = new List<UserModel>();

            IQueryable<user> dataset = UserRepository.GetAll();

            foreach (user item in dataset.ToList())
            {
                newlist.Add(Util.ConvertToUserModel(item));
            }

            Response = new ResponseModel()
            {
                Users = newlist,
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
