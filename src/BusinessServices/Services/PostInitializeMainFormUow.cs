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
    internal class PostInitializeMainFormUow : UnitOfWork
    {
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
        private class WellKnownErrors : WellKnownServiceErrors
        {
        }

        /// <summary>
        /// Runs prior to the work being done.
        /// </summary>
        protected override void PreExecute()
        {
            this.WellKnownError = new WellKnownErrors();

            this.TranscriptionRepository = new TranscriptionRepository();

            this.WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            Util Util = new Util();

            List<TranscriptionModel> transcriptionModels = new List<TranscriptionModel>();

            IQueryable<transcription> transcriptions = TranscriptionRepository
                .FindBy(i => i.UpdatedBy == this.Request.UserModel.UserId)
                .OrderByDescending(i => i.CreatedDate)
                .Take(10);

            foreach (transcription item in transcriptions.ToList())
            {
                transcriptionModels.Add(Util.ConvertToTranscriptionModel(item));
            }

            MainFormModel mainFormModel = new MainFormModel()
            {
                BrowseRecordCount = this.TranscriptionRepository.GetAll().Count(),
                TranscrptionQueueRecordCount = this.TranscriptionRepository.FindBy(t => t.TranscriptStatus == false).Count(),
            };

            this.Response = new ResponseModel()
            {
                MainFormModel = mainFormModel,
                Transcriptions = transcriptionModels,
                IsOperationSuccess = true
            };

        }

        private KeyValuePair<string, string> SetPair(string name, int count)
        {
            return new KeyValuePair<string, string>(name + " (" + count + ")", name);
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
