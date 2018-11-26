using BusinessServices.Servcices;
using Model.Transfer;

namespace BusinessServices
{
    /// <summary>
    /// Defines the internal services.
    /// </summary>
    public class InternalService
    {

        /// <summary>
        /// Gets the transcription.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <returns></returns>
        public ResponseModel GetTranscription(RequestModel requestModel)
        {
            GetTranscriptionUow uow = new GetTranscriptionUow();

            uow.Request = requestModel;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Users the sign in.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <returns></returns>
        public ResponseModel UserSignIn(RequestModel requestModel)
        {
            UserSigninUow uow = new UserSigninUow();

            uow.Request = requestModel;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Gets the transcriptions.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <returns></returns>
        public ResponseModel GetTranscriptions(RequestModel requestModel)
        {
            GetTranscriptionsUow uow = new GetTranscriptionsUow();

            uow.Request = requestModel;

            uow.DoWork();

            return uow.Response;
        }
               
        /// <summary>
        /// Modifies the transcription.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <returns></returns>
        public ResponseModel ModifyTranscription(RequestModel requestModel)
        {
            ModifyTranscriptionUow uow = new ModifyTranscriptionUow();

            uow.Request = requestModel;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Initializes the browse form.
        /// </summary>
        /// <returns></returns>
        public ResponseModel InitializeBrowseForm()
        {
            InitializeBrowseFormUow uow = new InitializeBrowseFormUow();

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Gets the transcriptions for browse.
        /// </summary>
        /// <param name="requestModel">The request model.</param>
        /// <returns></returns>
        public ResponseModel GetTranscriptionsForBrowse(RequestModel requestModel)
        {
            GetTranscriptionsForBrowseUow uow = new GetTranscriptionsForBrowseUow();

            uow.Request = requestModel;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Gets the main form initialize.
        /// </summary>
        /// <returns></returns>
        public ResponseModel GetSystemInitialize()
        {
            SystemInitializeUow uow = new SystemInitializeUow();

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Gets the post initialize main form.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel GetPostInitializeMainForm(RequestModel request)
        {
            PostInitializeMainFormUow uow = new PostInitializeMainFormUow();

            uow.Request = request;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel GetUsers(RequestModel request)
        {
            GetUsersUow uow = new GetUsersUow();

            uow.Request = request;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Resets the user password.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel ResetUserPassword(RequestModel request)
        {
            UserResetUow uow = new UserResetUow();

            uow.Request = request;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel GetUser(RequestModel request)
        {
            GetUserUow uow = new GetUserUow();

            uow.Request = request;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Modifies the user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel ModifyUser(RequestModel request)
        {
            ModifyUserUow uow = new ModifyUserUow();

            uow.Request = request;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Imports the transcription.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Returns the number of records inserted to the data base.
        /// </returns>
        public ResponseModel ImportTranscription(RequestModel request)
        {
            ImportTranscriptionUow uow = new ImportTranscriptionUow();

            uow.Request = request;

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Resets the database.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel ResetDatabase(RequestModel request)
        {
            ResetDatabaseUow uow = new ResetDatabaseUow
            {
                Request = request
            };

            uow.DoWork();

            return uow.Response;
        }

        /// <summary>
        /// Finds the replace.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ResponseModel FindReplace(RequestModel request)
        {
            FindReplaceUow uow = new FindReplaceUow
            {
                Request = request
            };

            uow.DoWork();

            return uow.Response;
        }

    }

}
