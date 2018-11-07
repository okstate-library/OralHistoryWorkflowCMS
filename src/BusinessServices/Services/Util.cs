using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using EntityData;
using Model;
using Repository.Implementations;

namespace BusinessServices.Services
{
    /// <summary>
    /// Util class functinalities
    /// </summary>
    internal class Util
    {
        /// <summary>
        /// The collection repository
        /// </summary>
        static CollectionRepository collectionRepository = new CollectionRepository();

        /// <summary>
        /// The subsery repository
        /// </summary>
        static SubseryRepository subseryRepository = new SubseryRepository();


        private static IQueryable<collection> collectionList = null;
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

        private static IQueryable<subsery> subseriesList = null;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Util"/> class.
        /// </summary>
        public Util()
        {
            collectionRepository = new CollectionRepository();
            subseryRepository = new SubseryRepository();

        }

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
                EquipmentUsed = transcription.EquipmentUsed,
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
                AuditCheckCompletedDate = transcription.AuditCheckCompletedDate != null ? transcription.AuditCheckCompletedDate : null ,
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
                
                CollectionName = collectionListInstance.First(c => c.Id == transcription.CollectionId).CollectionName,
                SubseriesName = SubseriesListInstance.First(s => s.Id == transcription.SubseriesId).SubseriesName,
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
                EquipmentUsed = transcriptionModel.EquipmentUsed,
                InterviewDate = transcriptionModel.InterviewDate,
                InterviewerNote = transcriptionModel.InterviewerNote,
                IsAudioFormat = transcriptionModel.IsAudioFormat,
                IsRestriction = transcriptionModel.IsRestriction,
                Keywords = transcriptionModel.Keywords,
                LegalNote = transcriptionModel.LegalNote,
                ReleaseForm = transcriptionModel.ReleaseForm,
                Subject = transcriptionModel.Subject,
                Title = transcriptionModel.Title,

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
                EquipmentUsed = interview.EquipmentUsed,
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
                EquipmentUsed = transcriptionModel.EquipmentUsed,
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
                UserTypeName = ((WellKnownUserType)user.UserType).ToString()
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
           return daUser;
        }
    }
}
