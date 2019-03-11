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
    /// Defines the properties, construtor and methods related to ImportTranscriptionUow
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    internal class ImportTranscriptionUow : UnitOfWork
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
        private TranscriptionRepository TranscriptionRepository
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the interviewer repository.
        /// </summary>
        /// <value>
        /// The interviewer repository.
        /// </value>
        public InterviewerRepository InterviewerRepository
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
        /// Initializes a new instance of the <see cref="ImportTranscriptionUow" /> class.
        /// </summary>
        /// <returns></returns>
        public ImportTranscriptionUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private ImportTranscriptionUow(bool isReadOnly)
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
            InterviewerRepository = new InterviewerRepository();

            WellKnownError.Value = WellKnownError.NoError;
        }

        /// <summary>
        /// The actual Work to be done.
        /// </summary>
        protected override void Execute()
        {
            List<transcription> transcriptions = TranscriptionRepository.GetAll().ToList();

            List<string> codes = transcriptions.Select(s => s.ProjectCode.ToLower()).ToList();

            List<string> interviewerList = InterviewerRepository.List();

            List<string> importInterviewerList = new List<string>();

            int duplicateRecordCount = 0, insertedRecordCount = 0;

            foreach (TranscriptionModel item in Request.TranscriptionModels)
            {
                if (!codes.Exists(s => s.Contains(item.ProjectCode.ToLower())))
                {
                    transcription transcription = Util.ConvertToTranscription(item);

                    transcription.TranscriptStatus = true;

                    TranscriptionRepository.Add(transcription);
                    TranscriptionRepository.Save();

                    if (!string.IsNullOrEmpty(item.Interviewer))
                    {
                        string[] splits = item.Interviewer.Split(';');

                        foreach (string str in splits)
                        {
                            if (!importInterviewerList.Contains(str.Trim()))
                            {
                                importInterviewerList.Add(str.Trim());
                            }
                        }
                    }

                    insertedRecordCount++;
                }
                else
                {
                    duplicateRecordCount++;
                }
            }

            foreach (string item in importInterviewerList.Except(interviewerList))
            {
                interviewer interviewer = new interviewer() { InterviewerName = item };

                InterviewerRepository.Add(interviewer);
                InterviewerRepository.Save();
            }


            Response = new ResponseModel()
            {
                ErrorMessage = string.Format(" {0} record(s) were \n successfully imported and \n found" +
                " {1} duplicate records.", insertedRecordCount, duplicateRecordCount),
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
