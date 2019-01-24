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
    /// Defines business logic functionalities of getting find and replace uow.
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

            var predicate = PredicateBuilder.True<transcription>();

            if (!string.IsNullOrEmpty(Request.FindReplaceModel.FindWord))
            {
                predicate = predicate.And(p =>
                                                p.Description.Contains(Request.FindReplaceModel.FindWord) ||
                                                p.Keywords.Contains(Request.FindReplaceModel.FindWord) ||
                                                p.Title.Contains(Request.FindReplaceModel.FindWord) ||
                                                p.Subject.Contains(Request.FindReplaceModel.FindWord)
                                              );
            }

            List<transcription> allTranscription = TranscriptionRepository.GetAll().ToList();

            IEnumerable<transcription> dataset3 = allTranscription.Where<transcription>(predicate.Compile());

            List<transcription> all = dataset3.ToList();

            foreach (transcription item in all)
            {
                if (Request.FindReplaceModel.Field == Core.Enums.WellKnownFindAndReplaceType.None)
                {
                    item.Title = item.Title.Replace(Request.FindReplaceModel.FindWord,
                        Request.FindReplaceModel.ReplaceWord);

                    item.Description = item.Description.Replace(Request.FindReplaceModel.FindWord,
                        Request.FindReplaceModel.ReplaceWord);

                    item.Keywords = item.Keywords.Replace(Request.FindReplaceModel.FindWord,
                        Request.FindReplaceModel.ReplaceWord);

                    item.Subject = item.Subject.Replace(Request.FindReplaceModel.FindWord,
                        Request.FindReplaceModel.ReplaceWord);
                }
                else
                {
                    switch (Request.FindReplaceModel.Field)
                    {
                        case Core.Enums.WellKnownFindAndReplaceType.Title:
                            item.Title = item.Title.Replace(Request.FindReplaceModel.FindWord,
                                    Request.FindReplaceModel.ReplaceWord);
                            break;
                        case Core.Enums.WellKnownFindAndReplaceType.Description:

                            item.Description = item.Description.Replace(Request.FindReplaceModel.FindWord,
                                    Request.FindReplaceModel.ReplaceWord);
                            break;
                        case Core.Enums.WellKnownFindAndReplaceType.Keywords:
                            item.Keywords = item.Keywords.Replace(Request.FindReplaceModel.FindWord,
                                    Request.FindReplaceModel.ReplaceWord);
                            break;
                        case Core.Enums.WellKnownFindAndReplaceType.Subject:
                            item.Subject = item.Subject.Replace(Request.FindReplaceModel.FindWord,
                                    Request.FindReplaceModel.ReplaceWord);
                            break;
                        default:
                            break;
                    }
                }

                TranscriptionRepository.Edit(item);
                TranscriptionRepository.Save();
            }

            Response = new ResponseModel()
            {
                IsOperationSuccess = true,
                ErrorMessage = all.Count.ToString(),
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
