using BusinessServices.Services;
using Core;
using EntityData;
using Model.Transfer;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to ModifyCollectionUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class ModifyRepositoryUow : UnitOfWork
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
        private RepositoryRepository RepositoryRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the subsery repository.
        /// </summary>
        /// <value>
        /// The subsery repository.
        /// </value>
        private SubseryRepository SubseryRepository
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
        /// Initializes a new instance of the <see cref="ModifyCollectionUow" /> class.
        /// </summary>
        public ModifyRepositoryUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private ModifyRepositoryUow(bool isReadOnly)
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

            RepositoryRepository = new RepositoryRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            repository repository = null;

            switch (Request.WellKnownModificationType)
            {
                case Core.Enums.WellKnownModificationType.Add:

                    repository = Util.ConvertToRepository(Request.RepositoryModel);

                    RepositoryRepository.Add(repository);
                    RepositoryRepository.Save();
                                      
                    break;
                case Core.Enums.WellKnownModificationType.Edit:

                    repository = RepositoryRepository.GetRepositoryToEdit(Request.RepositoryModel.Id);

                    repository = Util.ConvertToRepository(repository, Request.RepositoryModel);

                    RepositoryRepository.Edit(repository);
                    RepositoryRepository.Save();

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
