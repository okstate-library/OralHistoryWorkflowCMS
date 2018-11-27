using EntityData;
using Model;
using Model.Transfer;
using Repository;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Servcices
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BusinessServices.UnitOfWork" />
    public class FindReplaceUow : UnitOfWork
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
        /// Gets or sets the transcription repository.
        /// </summary>
        /// <value>
        /// The transcription repository.
        /// </value>
        public TranscriptionRepository TranscriptionRepository
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
        /// Initializes a new instance of the <see cref="FindReplaceUow" /> class.
        /// </summary>
        public FindReplaceUow()
            : this(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="" /> class.
        /// </summary>
        /// <param name="isReadOnly"><c>true</c> if this Unit of Work is read only;
        /// otherwise,
        /// <c>false</c>.</param>
        private FindReplaceUow(bool isReadOnly)
            : base(isReadOnly)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <seealso cref="BusinessServices.WellKnownServiceErrors" />
        private class WellKnownErrors : WellKnownServiceErrors
        {
            // <summary>
            /// <summary>
            /// The invalid username well known error
            /// </summary>
            public static readonly Tuple<int, string> InvalidUsernameWellKnownError =
                new Tuple<int, string>(1, "Invalid username.");

            /// <summary>
            /// The invalid password well known error
            /// </summary>
            public static readonly Tuple<int, string> InvalidPasswordWellKnownError =
                new Tuple<int, string>(2, "Invalid password.");
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
            List<TranscriptionModel> newlist = new List<TranscriptionModel>();

            var predicate = PredicateBuilder.True<transcription>();

            if (!string.IsNullOrEmpty(this.Request.FindReplaceModel.FindWord))
            {
                predicate = predicate.And(p =>
                                                p.Description.Contains(this.Request.FindReplaceModel.FindWord) ||
                                                p.Keywords.Contains(this.Request.FindReplaceModel.FindWord) ||
                                                p.Title.Contains(this.Request.FindReplaceModel.FindWord) ||
                                                p.Subject.Contains(this.Request.FindReplaceModel.FindWord)
                                              );
            }

            List<transcription> allTranscription = this.TranscriptionRepository.GetAll().ToList();

            IEnumerable<transcription> dataset3 = allTranscription.Where<transcription>(predicate.Compile());

            List<transcription> all = dataset3.ToList();

            foreach (transcription item in all)
            {
                if (this.Request.FindReplaceModel.Field == Core.Enums.WellKnownFindAndReplaceType.None)
                {
                    item.Title = item.Title.Replace(this.Request.FindReplaceModel.FindWord,
                      this.Request.FindReplaceModel.ReplaceWord);

                    item.Description = item.Description.Replace(this.Request.FindReplaceModel.FindWord,
                  this.Request.FindReplaceModel.ReplaceWord);

                    item.Keywords = item.Keywords.Replace(this.Request.FindReplaceModel.FindWord,
                        this.Request.FindReplaceModel.ReplaceWord);

                    item.Subject = item.Subject.Replace(this.Request.FindReplaceModel.FindWord,
                        this.Request.FindReplaceModel.ReplaceWord);
                }
                else
                {
                    switch (this.Request.FindReplaceModel.Field)
                    {
                        case Core.Enums.WellKnownFindAndReplaceType.Title:
                            item.Title = item.Title.Replace(this.Request.FindReplaceModel.FindWord,
                                    this.Request.FindReplaceModel.ReplaceWord);
                            break;
                        case Core.Enums.WellKnownFindAndReplaceType.Description:

                            item.Description = item.Description.Replace(this.Request.FindReplaceModel.FindWord,
                                    this.Request.FindReplaceModel.ReplaceWord);
                            break;
                        case Core.Enums.WellKnownFindAndReplaceType.Keywords:
                            item.Keywords = item.Keywords.Replace(this.Request.FindReplaceModel.FindWord,
                                    this.Request.FindReplaceModel.ReplaceWord);
                            break;
                        case Core.Enums.WellKnownFindAndReplaceType.Subject:
                            item.Subject = item.Subject.Replace(this.Request.FindReplaceModel.FindWord,
                                    this.Request.FindReplaceModel.ReplaceWord);
                            break;
                        default:
                            break;
                    }
                }

                this.TranscriptionRepository.Edit(item);
                this.TranscriptionRepository.Save();
            }

            this.Response = new ResponseModel()
            {
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
