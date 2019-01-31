using Core;
using Core.Enums;
using EntityData;
using Model;
using Repository.Implementations;
using System;
using System.Linq;

namespace BusinessServices.Services
{
    /// <summary>
    /// Util class functinalities
    /// </summary>
    internal class Util
    {
        #region Proerties

        /// <summary>
        /// The collection repository
        /// </summary>
        private static CollectionRepository collectionRepository = new CollectionRepository();

        /// <summary>
        /// The subsery repository
        /// </summary>
        private static SubseryRepository subseryRepository = new SubseryRepository();

        /// <summary>
        /// The collection list
        /// </summary>
        private static IQueryable<collection> collectionList = null;

        /// <summary>
        /// Gets the collection list instance.
        /// </summary>
        /// <value>
        /// The collection list instance.
        /// </value>
        public static IQueryable<collection> collectionListInstance
        {
            get
            {
                if (collectionList == null)
                {
                    collectionList = collectionRepository.GetAll();
                }

                return collectionList;
            }
        }

        /// <summary>
        /// The subseries list
        /// </summary>
        private static IQueryable<subsery> subseriesList = null;

        /// <summary>
        /// Gets the subseries list instance.
        /// </summary>
        /// <value>
        /// The subseries list instance.
        /// </value>
        public static IQueryable<subsery> SubseriesListInstance
        {
            get
            {
                if (subseriesList == null)
                {
                    subseriesList = subseryRepository.GetAll();
                }

                return subseriesList;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Util" /> class.
        /// </summary>
        public Util()
        {
            collectionRepository = new CollectionRepository();
            subseryRepository = new SubseryRepository();

        }

        #endregion

        /// <summary>
        /// Converts to collection model.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static CollectionModel ConvertToCollectionModel(collection collection)
        {
            return new CollectionModel()
            {
                Id = collection.Id,
                CollectionName = collection.CollectionName
            };
        }

        /// <summary>
        /// Converts to subsery model.
        /// </summary>
        /// <param name="subsery">The subsery.</param>
        /// <returns></returns>
        public static SubseryModel ConvertToSubseryModel(subsery subsery)
        {
            return new SubseryModel()
            {
                Id = subsery.Id,
                CollectionId = subsery.CollectionId,
                SubseryName = subsery.SubseriesName
            };
        }

        /// <summary>
        /// Converts to keyword model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        internal static KeywordModel ConvertToKeywordModel(keyword item)
        {
            return new KeywordModel()
            {
                Id = item.Id,
                Name = item.KeywordName
            };
        }

        /// <summary>
        /// Converts to subject model.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        internal static SubjectModel ConvertToSubjectModel(subject item)
        {
            return new SubjectModel()
            {
                Id = item.Id,
                Name = item.SubjectName
            };
        }

        /// <summary>
        /// Converts to transcription model.
        /// </summary>
        /// <param name="transcription">The transcription.</param>
        /// <returns></returns>
        public static TranscriptionModel ConvertToTranscriptionModel(transcription transcription)
        {
            return new TranscriptionModel()
            {
                CollectionId = transcription.CollectionId,
                Description = transcription.Description,
                Interviewee = transcription.Interviewee,
                Id = transcription.Id,
                Interviewer = transcription.Interviewer,
                Place = transcription.Place,
                SubseriesId = transcription.SubseriesId,
                AudioEquipmentUsed = transcription.AudioEquipmentUsed,
                VideoEquipmentUsed = transcription.VideoEquipmentUsed,
                InterviewDate = transcription.InterviewDate.Date,
                InterviewerNote = transcription.InterviewerNote,
                IsAudioFormat = transcription.IsAudioFormat,
                IsVideoFormat = transcription.IsVideoFormat,
                IsRestriction = transcription.IsRestriction,
                Keywords = transcription.Keywords,
                LegalNote = transcription.LegalNote,
                ReleaseForm = transcription.ReleaseForm,
                Subject = transcription.Subject,
                Title = transcription.Title,
                ProjectCode = transcription.ProjectCode,
                AccessFileLocation = transcription.AccessFileLocation,
                AuditCheckCompleted = transcription.AuditCheckCompleted,
                AuditCheckCompletedDate = transcription.AuditCheckCompletedDate != null ? transcription.AuditCheckCompletedDate : null,
                ConvertToDigitalDate = transcription.ConvertToDigitalDate != null ? transcription.ConvertToDigitalDate : null,
                CoverageSpatial = transcription.CoverageSpatial,
                CoverageTemporal = transcription.CoverageTemporal,
                CreatedBy = transcription.CreatedBy,
                CreatedDate = transcription.CreatedDate.Date,
                DraftSentDate = transcription.DraftSentDate != null ? transcription.DraftSentDate : null,
                EditWithCorrectionCompleted = transcription.EditWithCorrectionCompleted,
                EditWithCorrectionDate = transcription.EditWithCorrectionDate != null ? transcription.EditWithCorrectionDate : null,
                FileName = transcription.FileName,
                FinalEditCompleted = transcription.FinalEditCompleted,
                FinalEditDate = transcription.FinalEditDate != null ? transcription.FinalEditDate : null,
                FinalSentDate = transcription.FinalSentDate != null ? transcription.FinalSentDate : null,
                FirstEditCompleted = transcription.FirstEditCompleted,
                FirstEditCompletedDate = transcription.FirstEditCompletedDate != null ? transcription.FirstEditCompletedDate : null,
                Format = transcription.Format,
                InitialNote = transcription.InitialNote,
                IsAccessMediaStatus = transcription.IsAccessMediaStatus,
                IsBornDigital = transcription.IsBornDigital,
                IsConvertToDigital = transcription.IsConvertToDigital,
                IsInContentDm = transcription.IsInContentDm,
                IsPriority = transcription.IsPriority,
                IsRosetta = transcription.IsRosetta,
                IsRosettaForm = transcription.IsRosettaForm,
                Language = transcription.Language,
                MasterFileLocation = transcription.MasterFileLocation,
                OriginalMedium = transcription.OriginalMedium,
                OriginalMediumType = transcription.OriginalMediumType,
                Publisher = transcription.Publisher,
                ReasonForPriority = transcription.ReasonForPriority,
                RelationIsPartOf = transcription.RelationIsPartOf,
                Rights = transcription.Rights,
                ScopeAndContents = transcription.ScopeAndContents,
                SecondEditCompleted = transcription.SecondEditCompleted,
                SecondEditCompletedDate = transcription.SecondEditCompletedDate != null ? transcription.SecondEditCompletedDate : null,
                TranscriberAssigned = transcription.TranscriberAssigned,
                TranscriberCompleted = transcription.TranscriberCompleted,
                Transcript = transcription.Transcript,
                TranscriptLocation = transcription.TranscriptLocation,
                TranscriptNote = transcription.TranscriptNote,
                TranscriptStatus = transcription.TranscriptStatus,
                Type = transcription.Type,
                TranscriberLocation = transcription.TranscriberLocation,
                UpdatedBy = transcription.UpdatedBy,
                UpdatedDate = transcription.UpdatedDate.Date,
                Identifier = transcription.Identifier,

                InterviewerDescription = transcription.InterviewerDescription,
                InterviewerKeywords = transcription.InterviewerKeywords,
                InterviewerSubjects = transcription.InterviewerSubjects,
                SentOut = transcription.SentOut,
                EquipmentNumber = transcription.EquipmentNumber,
                MetadataDraft = transcription.MetadataDraft,

                CollectionName = collectionListInstance.First(c => c.Id == transcription.CollectionId).CollectionName,
                SubseriesName = SubseriesListInstance.First(s => s.Id == transcription.SubseriesId).SubseriesName,
            };
        }

        internal static UserTypeModel ConvertToUsertypeModel(usertype item)
        {
            return new UserTypeModel()
            {
                Id = item.Id,
                UserTypeName = item.UserTypeName,
                IsHorizontalMenu = item.IsHorizontalMenu,
            };
        }

        internal static InterviewerModel ConvertToInterviewerModel(interviewer item)
        {
            return new InterviewerModel()
            {
                Id = item.Id,
                Name = item.InterviewerName
            };
        }

        /// <summary>
        /// Converts to transcription.
        /// </summary>
        /// <param name="transcriptionModel">The transcription model.</param>
        /// <returns></returns>
        public static transcription ConvertToTranscription(TranscriptionModel transcriptionModel)
        {
            return new transcription()
            {
                CollectionId = (short)transcriptionModel.CollectionId,
                Description = transcriptionModel.Description,
                Interviewee = transcriptionModel.Interviewee,
                Interviewer = transcriptionModel.Interviewer,
                Place = transcriptionModel.Place,
                SubseriesId = transcriptionModel.SubseriesId,
                AudioEquipmentUsed = transcriptionModel.AudioEquipmentUsed,
                VideoEquipmentUsed = transcriptionModel.VideoEquipmentUsed,
                InterviewDate = transcriptionModel.InterviewDate,
                InterviewerNote = transcriptionModel.InterviewerNote,
                IsAudioFormat = transcriptionModel.IsAudioFormat,
                IsRestriction = transcriptionModel.IsRestriction,
                Keywords = transcriptionModel.Keywords,
                LegalNote = transcriptionModel.LegalNote,
                ReleaseForm = transcriptionModel.ReleaseForm,
                Subject = transcriptionModel.Subject,
                Title = transcriptionModel.Title,

                MetadataDraft = transcriptionModel.MetadataDraft,
                SentOut = transcriptionModel.SentOut,
                EquipmentNumber = transcriptionModel.EquipmentNumber,

                InterviewerDescription = GetSubString(transcriptionModel.InterviewerDescription),
                InterviewerKeywords = GetSubString(transcriptionModel.InterviewerKeywords),
                InterviewerSubjects = GetSubString(transcriptionModel.InterviewerSubjects),

                //AuditCheckCompletedDate = DateTime.MinValue,//DBNull.Value, //transcriptionModel.CreatedDate,
                //EditWithCorrectionDate = DateTime.MinValue,
                //ConvertToDigitalDate = DateTime.MinValue,
                //DraftSentDate = DateTime.MinValue,
                //FinalEditDate = DateTime.MinValue,
                //FinalSentDate = DateTime.MinValue,
                //FirstEditCompletedDate = DateTime.MinValue,
                //SecondEditCompletedDate = DateTime.MinValue,
                //TranscriberCompleted = DateTime.MinValue,

                //AccessFileLocation = "AccessFileLocation",
                //AuditCheckCompleted = "AuditCheckCompleted",
                //CoverageSpatial = "CoverageSpatial",
                //CoverageTemporal= "CoverageTemporal",
                //EditWithCorrectionCompleted = "EditWithCorrectionCompleted",
                //FileName= "FileName",
                //FinalEditCompleted= "FinalEditCompleted",
                //FirstEditCompleted = "FirstEditCompleted",
                //Format = "Format",

                //InitialNote = "Initial Note",
                //IsAccessMediaStatus = true,
                //IsBornDigital = true,
                //IsConvertToDigital = true,
                //IsInContentDm = true,
                //IsRosetta = true,
                //IsRosettaForm = true,
                //Language = "Language",
                //MasterFileLocation= "MasterFileLocation",
                //OriginalMedium ="original mediunm",
                //OriginalMediumType = 1,
                //Publisher = "Publisher",
                //RelationIsPartOf = "RelationIsPartOf",
                //Rights = "roghts",
                //ScopeAndContents = "scope and contents",
                //SecondEditCompleted = "SecondEditCompleted",

                //Transcript = "Transcript",
                //TranscriptLocation = 1,
                //TranscriptStatus = 1,
                //Type = "Type",


                ProjectCode = transcriptionModel.ProjectCode,
                IsPriority = transcriptionModel.IsPriority,
                ReasonForPriority = transcriptionModel.ReasonForPriority,
                TranscriberAssigned = transcriptionModel.TranscriberAssigned,
                TranscriptNote = transcriptionModel.TranscriptNote,

                CreatedBy = transcriptionModel.CreatedBy,
                CreatedDate = transcriptionModel.CreatedDate,
                UpdatedBy = transcriptionModel.UpdatedBy,
                UpdatedDate = transcriptionModel.UpdatedDate
            };
        }

        /// <summary>
        /// Gets the sub string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string GetSubString(string value)
        {

            if (!string.IsNullOrEmpty(value))
            {
                return value.Length > 1000 ? value.Substring(1000) : value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Converts to transcription.
        /// </summary>
        /// <param name="transcriptionModel">The transcription model.</param>
        /// <param name="interview">The interview.</param>
        /// <param name="editMode">The edit mode.</param>
        /// <returns></returns>
        public static transcription ConvertToTranscription(TranscriptionModel transcriptionModel, transcription interview, int editMode)
        {

            return new transcription()
            {
                CollectionId = (short)interview.CollectionId,
                Description = transcriptionModel.Description,
                Interviewee = interview.Interviewee,
                Interviewer = interview.Interviewer,
                Place = interview.Place,
                SubseriesId = interview.SubseriesId,
                AudioEquipmentUsed = interview.AudioEquipmentUsed,
                VideoEquipmentUsed = interview.VideoEquipmentUsed,
                InterviewDate = interview.InterviewDate,
                InterviewerNote = interview.InterviewerNote,
                IsAudioFormat = interview.IsAudioFormat,
                IsRestriction = interview.IsRestriction,
                Keywords = interview.Keywords,
                LegalNote = interview.LegalNote,
                ReleaseForm = interview.ReleaseForm,
                Subject = interview.Subject,
                Title = interview.Title,



                //AuditCheckCompletedDate = DateTime.MinValue,//DBNull.Value, //transcriptionModel.CreatedDate,
                //EditWithCorrectionDate = DateTime.MinValue,
                //ConvertToDigitalDate = DateTime.MinValue,
                //DraftSentDate = DateTime.MinValue,
                //FinalEditDate = DateTime.MinValue,
                //FinalSentDate = DateTime.MinValue,
                //FirstEditCompletedDate = DateTime.MinValue,
                //SecondEditCompletedDate = DateTime.MinValue,
                //TranscriberCompleted = DateTime.MinValue,

                //AccessFileLocation = "AccessFileLocation",
                //AuditCheckCompleted = "AuditCheckCompleted",
                //CoverageSpatial = "CoverageSpatial",
                //CoverageTemporal= "CoverageTemporal",
                //EditWithCorrectionCompleted = "EditWithCorrectionCompleted",
                //FileName= "FileName",
                //FinalEditCompleted= "FinalEditCompleted",
                //FirstEditCompleted = "FirstEditCompleted",
                //Format = "Format",

                //InitialNote = "Initial Note",
                //IsAccessMediaStatus = true,
                //IsBornDigital = true,
                //IsConvertToDigital = true,
                //IsInContentDm = true,
                //IsRosetta = true,
                //IsRosettaForm = true,
                //Language = "Language",
                //MasterFileLocation= "MasterFileLocation",
                //OriginalMedium ="original mediunm",
                //OriginalMediumType = 1,
                //Publisher = "Publisher",
                //RelationIsPartOf = "RelationIsPartOf",
                //Rights = "roghts",
                //ScopeAndContents = "scope and contents",
                //SecondEditCompleted = "SecondEditCompleted",

                //Transcript = "Transcript",
                //TranscriptLocation = 1,
                //TranscriptStatus = 1,
                //Type = "Type",


                ProjectCode = transcriptionModel.ProjectCode,
                IsPriority = transcriptionModel.IsPriority,
                ReasonForPriority = transcriptionModel.ReasonForPriority,
                TranscriberAssigned = transcriptionModel.TranscriberAssigned,
                TranscriptNote = transcriptionModel.TranscriptNote,

                CreatedBy = transcriptionModel.CreatedBy,
                CreatedDate = transcriptionModel.CreatedDate,
                UpdatedBy = transcriptionModel.UpdatedBy,
                UpdatedDate = transcriptionModel.UpdatedDate
            };
        }

        /// <summary>
        /// Converts to transcription.
        /// </summary>
        /// <param name="transcriptionModel">The transcription model.</param>
        /// <returns></returns>
        public static transcription ConvertToTranscription2(TranscriptionModel transcriptionModel)
        {
            return new transcription()
            {
                CollectionId = (short)transcriptionModel.CollectionId,
                Description = transcriptionModel.Description,
                Interviewee = transcriptionModel.Interviewee,
                Id = transcriptionModel.Id,
                Interviewer = transcriptionModel.Interviewer,
                Place = transcriptionModel.Place,
                SubseriesId = transcriptionModel.SubseriesId,
                AudioEquipmentUsed = transcriptionModel.AudioEquipmentUsed,
                VideoEquipmentUsed = transcriptionModel.VideoEquipmentUsed,
                InterviewDate = transcriptionModel.InterviewDate,
                InterviewerNote = transcriptionModel.InterviewerNote,
                IsAudioFormat = transcriptionModel.IsAudioFormat,
                IsRestriction = transcriptionModel.IsRestriction,
                Keywords = transcriptionModel.Keywords,
                LegalNote = transcriptionModel.LegalNote,
                ReleaseForm = transcriptionModel.ReleaseForm,
                Subject = transcriptionModel.Subject,
                Title = transcriptionModel.Title,
                ProjectCode = transcriptionModel.ProjectCode,
                CreatedBy = transcriptionModel.CreatedBy,
                CreatedDate = transcriptionModel.CreatedDate,
                UpdatedBy = transcriptionModel.UpdatedBy,
                UpdatedDate = transcriptionModel.UpdatedDate
            };
        }

        /// <summary>
        /// Converts to user model.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static UserModel ConvertToUserModel(user user)
        {
            return new UserModel()
            {
                UserId = user.UserId,
                Name = user.Name,
                Username = user.Username,
                UserType = (byte)user.UserType,
                //UserTypeName = ((WellKnownUserType)user.UserType).ToString()
            };
        }

        /// <summary>
        /// Converts to user.
        /// </summary>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        internal static user ConvertToUser(UserModel userModel)
        {
            return new user()
            {
                UserId = userModel.UserId,
                Name = userModel.Name,
                Username = userModel.Username,
                UserType = userModel.UserType,
                Password = Encryption.Encrypt(Encryption.convertToUNSecureString(userModel.Password)),
            };
        }

        /// <summary>
        /// Converts to user.
        /// </summary>
        /// <param name="daUser">The da user.</param>
        /// <param name="userModel">The user model.</param>
        /// <returns></returns>
        internal static user ConvertToUser(user daUser, UserModel userModel)
        {
            daUser.Name = userModel.Name;
            //daUser.Email = userModel.Email;

            return daUser;
        }
    }
}
