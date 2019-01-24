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
    /// Defines the properties, construtor and methods related to PostInitializeMainFormUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class PostInitializeMainFormUow : UnitOfWork
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
        /// Gets or sets the TranscriptionRepository repository.
        /// </summary>
        /// <value>
        /// The TranscriptionRepository repository.
        /// </value>
        private TranscriptionRepository TranscriptionRepository
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
        /// Initializes a new instance of the <see cref="PostInitializeMainFormUow" /> class.
        /// </summary>
        public PostInitializeMainFormUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private PostInitializeMainFormUow(bool isReadOnly)
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

            TranscriptionRepository = new TranscriptionRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            Util Util = new Util();

            List<TranscriptionModel> transcriptionModels = new List<TranscriptionModel>();

            IQueryable<transcription> transcriptions = TranscriptionRepository
                .FindBy(i => i.UpdatedBy == Request.UserModel.UserId)
                .OrderByDescending(i => i.CreatedDate)
                .Take(10);

            foreach (transcription item in transcriptions.ToList())
            {
                transcriptionModels.Add(Util.ConvertToTranscriptionModel(item));
            }

            MainFormModel mainFormModel = new MainFormModel()
            {
                BrowseRecordCount = TranscriptionRepository.GetAll().Count(),
                TranscrptionQueueRecordCount = TranscriptionRepository.FindBy(t => t.TranscriptStatus == false).Count(),
            };

            Response = new ResponseModel()
            {
                MainFormModel = mainFormModel,
                Transcriptions = transcriptionModels,
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
