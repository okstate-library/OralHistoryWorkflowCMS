using BusinessServices.Services;
using Core;
using EntityData;
using Model.Transfer;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to ModifySubseriesUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class ModifySubseriesUow : UnitOfWork
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
        /// Initializes a new instance of the <see cref="ModifySubseriesUow" /> class.
        /// </summary>
        public ModifySubseriesUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private ModifySubseriesUow(bool isReadOnly)
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
            subsery subsery = null;

            switch (Request.WellKnownModificationType)
            {
                case Core.Enums.WellKnownModificationType.Add:

                    subsery = new subsery()
                    {
                        CollectionId = Request.SubseryModel.CollectionId,
                        SubseriesName = Request.SubseryModel.SubseryName

                    };

                    SubseryRepository.Add(subsery);

                    SubseryRepository.Save();

                    break;
                case Core.Enums.WellKnownModificationType.Edit:

                    subsery = SubseryRepository.GetSubseriesToEdit(Request.SubseryModel.Id);
                    subsery.CollectionId = Request.SubseryModel.CollectionId;

                    subsery = Util.ConvertToSubsery(subsery, Request.SubseryModel);

                    SubseryRepository.Edit(subsery);
                    SubseryRepository.Save();

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
