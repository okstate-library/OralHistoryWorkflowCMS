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
    internal class GetSubseriesUoW : UnitOfWork
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
        /// Gets or sets the SubseryRepository repository.
        /// </summary>
        /// <value>
        /// The collection repository.
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
        /// Initializes a new instance of the <see cref="GetSubseriesUoW" /> class.
        /// </summary>
        public GetSubseriesUoW()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetSubseriesUoW(bool isReadOnly)
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
            SubseryRepository = new SubseryRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            List<CollectionModel> newlist = new List<CollectionModel>();
            List<SubseryModel> newSubseriesList = new List<SubseryModel>();
            SubseryModel subseryModel = new SubseryModel();

            List<collection> collections = CollectionRepository.GetCollections();

            if (this.Request.SubseriesId > 0)
            {
                subsery subsery = SubseryRepository.FirstOrDefault(s => s.Id == this.Request.SubseriesId);

                subseryModel = Util.ConvertToSubseryModel(subsery, collections.FirstOrDefault(s => s.Id == subsery.CollectionId).CollectionName);
            }
            else
            {
                foreach (collection item in collections)
                {
                    newlist.Add(Util.ConvertToCollectionModel(item));
                }

                foreach (subsery item in SubseryRepository.GetSubseries())
                {
                    newSubseriesList.Add(Util.ConvertToSubseryModel(item, collections.FirstOrDefault(s => s.Id == item.CollectionId).CollectionName));
                }
            }

            Response = new ResponseModel()
            {
                Subseries = newSubseriesList,
                Collections = newlist,
                SubseriesModel = subseryModel,
                IsOperationSuccess = true
            };

        }

        /// <summary>
        /// Sets the pair.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        private KeyValuePair<string, string> SetPair(string name, int count)
        {
            return new KeyValuePair<string, string>(name + " (" + count + ")", name);
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
