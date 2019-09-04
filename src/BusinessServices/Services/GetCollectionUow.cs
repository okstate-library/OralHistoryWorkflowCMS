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
    /// Defines the properties, construtor and methods related to SystemInitializeUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class GetCollectionUow : UnitOfWork
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
        private CollectionRepository CollectionRepository
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
        /// Initializes a new instance of the <see cref="GetCollectionUow" /> class.
        /// </summary>
        public GetCollectionUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetCollectionUow(bool isReadOnly)
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

            CollectionRepository = new CollectionRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            List<CollectionModel> newlist = new List<CollectionModel>();
            CollectionModel collectionModel = new CollectionModel();

            if (this.Request.CollectionId > 0)
            {
                collectionModel = Util.ConvertToCollectionModel(CollectionRepository.FirstOrDefault(c => c.Id == this.Request.CollectionId));
            }
            else
            {
                foreach (collection item in CollectionRepository.GetCollections())
                {
                    newlist.Add(Util.ConvertToCollectionModel(item));
                }
            }

            Response = new ResponseModel()
            {
                Collection = collectionModel,
                Collections = newlist,
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
