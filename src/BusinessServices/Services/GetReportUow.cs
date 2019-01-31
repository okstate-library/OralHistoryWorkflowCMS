using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessServices.Services;
using EntityData;
using Model;
using Model.Transfer;
using Model.Transfer.Search;
using PagedList;
using Repository;
using Repository.Implementations;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// Defines the properties, construtor and methods related to GetTranscriptionsForBrowseUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class GetReportUow : UnitOfWork
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
        /// Initializes a new instance of the <see cref="GetReportUow" /> class.
        /// </summary>
        public GetReportUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private GetReportUow(bool isReadOnly)
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
            List<TranscriptionModel> newlist = new List<TranscriptionModel>();

            List<transcription> allTranscriptions = TranscriptionRepository.GetAll().ToList();

            var predicate = PredicateBuilder.True<transcription>();

            //predicate = predicate.And(p => p.IsConvertToDigital == Request.ReportModel.IsDigitallyMigrated);
            ////predicate = predicate.And(p => p.IsBornDigital == Request.ReportModel.IsDigitallyMigrated);

            //predicate = predicate.And(p => p.IsAudioFormat == Request.ReportModel.IsAudioFormat);
            //predicate = predicate.And(p => p.IsVideoFormat == Request.ReportModel.IsVideoFormat);

            // Online and offline comparision  
            if (Request.ReportModel.IsOnline && Request.ReportModel.IsOffline)
            {
                predicate = predicate;
            }
            else if (Request.ReportModel.IsOnline)
            {
                predicate = predicate.And(p => p.IsInContentDm);
            }
            else if (Request.ReportModel.IsOffline)
            {
                predicate = predicate.And(p => !p.IsInContentDm);
            }

            // Born digital or converted to digital
            if (Request.ReportModel.IsConvertedDigital && Request.ReportModel.IsBornDigitally)
            {
                predicate = predicate.And(p => p.IsBornDigital && p.IsConvertToDigital);
            }
            else if (Request.ReportModel.IsConvertedDigital)
            {
                predicate = predicate.And(p => p.IsConvertToDigital);
            }
            else if (Request.ReportModel.IsBornDigitally)
            {
                predicate = predicate.And(p => p.IsBornDigital);
            }
                       
            // Begin and end date
            if (Request.ReportModel.BeginDate != null && Request.ReportModel.EndDate != null &&
            Request.ReportModel.BeginDate != DateTime.MinValue && Request.ReportModel.EndDate != DateTime.MinValue)
            {
                predicate = predicate.And(p => p.InterviewDate >= Request.ReportModel.BeginDate
                && p.InterviewDate < Request.ReportModel.EndDate);
            }

            // Interviewer
            if (!string.IsNullOrEmpty(Request.ReportModel.Interviewer))
            {
                predicate = predicate.And(p => p.Interviewer.ToLower().Contains(Request.ReportModel.Interviewer.ToLower()));
            }

            // location
            if (!string.IsNullOrEmpty(Request.ReportModel.Location))
            {
                predicate = predicate.And(p => p.Place.ToLower().Contains(Request.ReportModel.Location.ToLower()));
            }

            IEnumerable<transcription> dataset3 = allTranscriptions.Where<transcription>(predicate.Compile());

            foreach (transcription item in dataset3.ToList())
            {
                newlist.Add(Util.ConvertToTranscriptionModel(item));
            }

            Response = new ResponseModel()
            {
                Transcriptions = newlist,
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
