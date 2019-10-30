using Core;
using Core.Enums;
using EntityData;
using Model;
using Repository.Implementations;
using System;
using System.Collections.Generic;
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

        #region Repository

        public static repository ConvertToRepository(RepositoryModel model)
        {
            return new repository()
            {
                RepositoryName = model.RepositoryName,
            };
        }

        public static RepositoryModel ConvertToRepositoryModel(repository repository)
        {
            return new RepositoryModel()
            {
                Id = repository.Id,
                RepositoryName = repository.RepositoryName,
            };
        }

        public static repository ConvertToRepository(repository repository, RepositoryModel model)
        {
            repository.RepositoryName = model.RepositoryName;

            return repository;
        }

        #endregion

        #region Collection

        /// <summary>
        /// Converts to collection model.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static CollectionModel ConvertToCollectionModel(collection collection, string repositoryName)
        {
            return new CollectionModel()
            {
                Id = collection.Id,
                CollectionName = collection.CollectionName,

                RepositoryId = collection.RepositoryId,
                RepositoryName = repositoryName,
                Number = collection.Number,
                Dates = collection.Dates,
                Note = collection.Note,
                Subjects = collection.Subjects,
                Keywords = collection.Keywords,

                Description = collection.Description,
                ScopeAndContent = collection.ScopeAndContent,
                CustodialHistory = collection.CustodialHistory,
                Size = collection.Size,
                Acquisitioninfo = collection.Acquisitioninfo,
                Language = collection.Language,

                PreservationNote = collection.PreservationNote,
                Rights = collection.Rights,
                AccessRestrictions = collection.AccessRestrictions,
                PublicationRights = collection.PublicationRights,
                PreferredCitation = collection.PreferredCitation,
                RelatedCollection = collection.RelatedCollection,
                SeparatedMaterial = collection.SeparatedMaterial,
                OriginalLocation = collection.OriginalLocation,
                CopiesLocation = collection.CopiesLocation,
                PublicationNote = collection.PublicationNote,
                Creator = collection.Creator,
                Contributors = collection.Contributors,
                ProcessedBy = collection.ProcessedBy,
                Sponsors = collection.Sponsors,
                CreatedBy = collection.CreatedBy,
                CreatedDate = collection.CreatedDate,
                UpdatedBy = collection.UpdatedBy,
                UpdatedDate = collection.UpdatedDate,

            };
        }

        /// <summary>
        /// Converts to collection.
        /// </summary>
        /// <param name="collectionModel">The collection model.</param>
        /// <returns></returns>
        public static collection ConvertToCollection(CollectionModel collectionModel)
        {
            return new collection()
            {
                Id = (short)collectionModel.Id,
                CollectionName = collectionModel.CollectionName,
                RepositoryId = collectionModel.RepositoryId,
                Number = collectionModel.Number,
                Dates = collectionModel.Dates,
                Note = collectionModel.Note,
                Subjects = collectionModel.Subjects,
                Keywords = collectionModel.Keywords,

                Description = collectionModel.Description,
                ScopeAndContent = collectionModel.ScopeAndContent,
                CustodialHistory = collectionModel.CustodialHistory,
                Size = collectionModel.Size,
                Acquisitioninfo = collectionModel.Acquisitioninfo,
                Language = collectionModel.Language,

                PreservationNote = collectionModel.PreservationNote,
                Rights = collectionModel.Rights,
                AccessRestrictions = collectionModel.AccessRestrictions,
                PublicationRights = collectionModel.PublicationRights,
                PreferredCitation = collectionModel.PreferredCitation,
                RelatedCollection = collectionModel.RelatedCollection,
                SeparatedMaterial = collectionModel.SeparatedMaterial,
                OriginalLocation = collectionModel.OriginalLocation,
                CopiesLocation = collectionModel.CopiesLocation,
                PublicationNote = collectionModel.PublicationNote,
                Creator = collectionModel.Creator,
                Contributors = collectionModel.Contributors,
                ProcessedBy = collectionModel.ProcessedBy,
                Sponsors = collectionModel.Sponsors,
                CreatedBy = collectionModel.CreatedBy,
                CreatedDate = collectionModel.CreatedDate,
                UpdatedBy = collectionModel.UpdatedBy,
                UpdatedDate = collectionModel.UpdatedDate,
            };
        }

        /// <summary>
        /// Converts to collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="collectionModel">The collection model.</param>
        /// <returns></returns>
        internal static collection ConvertToCollection(collection collection, CollectionModel collectionModel)
        {
            collection.CollectionName = collectionModel.CollectionName;
            collection.RepositoryId = collectionModel.RepositoryId;
            collection.Number = collectionModel.Number;
            collection.Dates = collectionModel.Dates;
            collection.Note = collectionModel.Note;
            collection.Subjects = collectionModel.Subjects;
            collection.Keywords = collectionModel.Keywords;

            collection.Description = collectionModel.Description;
            collection.ScopeAndContent = collectionModel.ScopeAndContent;
            collection.CustodialHistory = collectionModel.CustodialHistory;
            collection.Size = collectionModel.Size;
            collection.Acquisitioninfo = collectionModel.Acquisitioninfo;
            collection.Language = collectionModel.Language;

            collection.PreservationNote = collectionModel.PreservationNote;
            collection.Rights = collectionModel.Rights;
            collection.AccessRestrictions = collectionModel.AccessRestrictions;
            collection.PublicationRights = collectionModel.PublicationRights;
            collection.PreferredCitation = collectionModel.PreferredCitation;
            collection.RelatedCollection = collectionModel.RelatedCollection;
            collection.SeparatedMaterial = collectionModel.SeparatedMaterial;
            collection.OriginalLocation = collectionModel.OriginalLocation;
            collection.CopiesLocation = collectionModel.CopiesLocation;
            collection.PublicationNote = collectionModel.PublicationNote;
            collection.Creator = collectionModel.Creator;
            collection.Contributors = collectionModel.Contributors;
            collection.ProcessedBy = collectionModel.ProcessedBy;
            collection.Sponsors = collectionModel.Sponsors;
            collection.CreatedBy = collectionModel.CreatedBy;
            collection.CreatedDate = collectionModel.CreatedDate;
            collection.UpdatedBy = collectionModel.UpdatedBy;
            collection.UpdatedDate = collectionModel.UpdatedDate;

            return collection;
        }

        #endregion

        #region Subseries

        /// <summary>
        /// Converts to subsery model.
        /// </summary>
        /// <param name="subsery">The subsery.</param>
        /// <returns></returns>
        public static SubseryModel ConvertToSubseryModel(subsery subsery, string collectionName)
        {
            return new SubseryModel()
            {
                Id = subsery.Id,
                CollectionId = subsery.CollectionId,
                CollectionName = collectionName,
                SubseryName = subsery.SubseriesName,

                Number = subsery.Number,
                Dates = subsery.Dates,
                Note = subsery.Note,
                Subjects = subsery.Subjects,
                Keywords = subsery.Keywords,

                Description = subsery.Description,
                ScopeAndContent = subsery.ScopeAndContent,
                CustodialHistory = subsery.CustodialHistory,
                Size = subsery.Size,
                Acquisitioninfo = subsery.Acquisitioninfo,
                Language = subsery.Language,

                PreservationNote = subsery.PreservationNote,
                Rights = subsery.Rights,
                AccessRestrictions = subsery.AccessRestrictions,
                PublicationRights = subsery.PublicationRights,
                PreferredCitation = subsery.PreferredCitation,
                RelatedCollection = subsery.RelatedCollection,
                SeparatedMaterial = subsery.SeparatedMaterial,
                OriginalLocation = subsery.OriginalLocation,
                CopiesLocation = subsery.CopiesLocation,
                PublicationNote = subsery.PublicationNote,
                Creator = subsery.Creator,
                Contributors = subsery.Contributors,
                ProcessedBy = subsery.ProcessedBy,
                Sponsors = subsery.Sponsors,
                CreatedBy = subsery.CreatedBy,
                CreatedDate = subsery.CreatedDate,
                UpdatedBy = subsery.UpdatedBy,
                UpdatedDate = subsery.UpdatedDate,
            };
        }

        public static subsery ConvertToSubsery(subsery subsery, SubseryModel subseryModel)
        {
            return new subsery()
            {
                Id = subsery.Id,
                CollectionId = subsery.CollectionId,
                SubseriesName = subseryModel.SubseryName,
                Number = subseryModel.Number,
                Dates = subseryModel.Dates,
                Note = subseryModel.Note,
                Subjects = subseryModel.Subjects,
                Keywords = subseryModel.Keywords,

                Description = subseryModel.Description,
                ScopeAndContent = subseryModel.ScopeAndContent,
                CustodialHistory = subseryModel.CustodialHistory,
                Size = subseryModel.Size,
                Acquisitioninfo = subseryModel.Acquisitioninfo,
                Language = subseryModel.Language,

                PreservationNote = subseryModel.PreservationNote,
                Rights = subseryModel.Rights,
                AccessRestrictions = subseryModel.AccessRestrictions,
                PublicationRights = subseryModel.PublicationRights,
                PreferredCitation = subseryModel.PreferredCitation,
                RelatedCollection = subseryModel.RelatedCollection,
                SeparatedMaterial = subseryModel.SeparatedMaterial,
                OriginalLocation = subseryModel.OriginalLocation,
                CopiesLocation = subseryModel.CopiesLocation,
                PublicationNote = subseryModel.PublicationNote,
                Creator = subseryModel.Creator,
                Contributors = subseryModel.Contributors,
                ProcessedBy = subseryModel.ProcessedBy,
                Sponsors = subseryModel.Sponsors,
                CreatedBy = subseryModel.CreatedBy,
                CreatedDate = subseryModel.CreatedDate,
                UpdatedBy = subseryModel.UpdatedBy,
                UpdatedDate = subseryModel.UpdatedDate,
            };
        }

        public static subsery ConvertToSubsery(SubseryModel subseryModel)
        {
            return new subsery()
            {
                Id = subseryModel.Id,
                CollectionId = subseryModel.CollectionId,
                SubseriesName = subseryModel.SubseryName,
                Number = subseryModel.Number,
                Dates = subseryModel.Dates,
                Note = subseryModel.Note,
                Subjects = subseryModel.Subjects,
                Keywords = subseryModel.Keywords,

                Description = subseryModel.Description,
                ScopeAndContent = subseryModel.ScopeAndContent,
                CustodialHistory = subseryModel.CustodialHistory,
                Size = subseryModel.Size,
                Acquisitioninfo = subseryModel.Acquisitioninfo,
                Language = subseryModel.Language,

                PreservationNote = subseryModel.PreservationNote,
                Rights = subseryModel.Rights,
                AccessRestrictions = subseryModel.AccessRestrictions,
                PublicationRights = subseryModel.PublicationRights,
                PreferredCitation = subseryModel.PreferredCitation,
                RelatedCollection = subseryModel.RelatedCollection,
                SeparatedMaterial = subseryModel.SeparatedMaterial,
                OriginalLocation = subseryModel.OriginalLocation,
                CopiesLocation = subseryModel.CopiesLocation,
                PublicationNote = subseryModel.PublicationNote,
                Creator = subseryModel.Creator,
                Contributors = subseryModel.Contributors,
                ProcessedBy = subseryModel.ProcessedBy,
                Sponsors = subseryModel.Sponsors,
                CreatedBy = subseryModel.CreatedBy,
                CreatedDate = subseryModel.CreatedDate,
                UpdatedBy = subseryModel.UpdatedBy,
                UpdatedDate = subseryModel.UpdatedDate,
            };
        }

        #endregion

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

        internal static UserTypeModel ConvertToUsertypeModel(usertype item)
        {
            return new UserTypeModel()
            {
                Id = item.Id,
                UserTypeName = item.UserTypeName,
                IsHorizontalMenu = item.IsHorizontalMenu,
            };
        }

        internal static PredefinedUserModel ConvertToInterviewerModel(predefineduser item)
        {
            return new PredefinedUserModel()
            {
                Id = item.Id,
                UserType = item.UserType,
                Name = item.Name
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

        #region Transcription

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

                InterviewDate = transcription.InterviewDate,
                InterviewDate1 = transcription.InterviewDate1,
                InterviewDate2 = transcription.InterviewDate2,

                InterviewerNote = transcription.InterviewerNote,
                IsAudioFormat = transcription.IsAudioFormat,
                IsVideoFormat = transcription.IsVideoFormat,
                IsRestriction = transcription.IsRestriction,
                IsDarkArchive = transcription.IsDarkArchive,
                Keywords = transcription.Keywords,
                RestrictionNote = transcription.RestrictionNote,
                DarkArchiveNote = transcription.DarkArchiveNote,
                ReleaseForm = transcription.ReleaseForm,
                Subject = transcription.Subject,
                Title = transcription.Title,
                ProjectCode = transcription.ProjectCode,
                AccessFileLocation = transcription.AccessFileLocation,
                AuditCheckCompleted = transcription.AuditCheckCompleted,
                ConvertToDigitalDate = transcription.ConvertToDigitalDate != null ? transcription.ConvertToDigitalDate : null,
                CoverageSpatial = transcription.CoverageSpatial,
                CoverageTemporal = transcription.CoverageTemporal,
                CreatedBy = transcription.CreatedBy,
                CreatedDate = transcription.CreatedDate.Date,
                DraftSentDate = transcription.DraftSentDate != null ? transcription.DraftSentDate : null,
                EditWithCorrectionCompleted = transcription.EditWithCorrectionCompleted,
                FileName = transcription.FileName,
                FinalEditCompleted = transcription.FinalEditCompleted,
                FinalSentDate = transcription.FinalSentDate != null ? transcription.FinalSentDate : null,
                FirstEditCompleted = transcription.FirstEditCompleted,
                ThirdEditCompleted = transcription.ThirdEditCompleted,
                Format = transcription.Format,
                InitialNote = transcription.InitialNote,
                IsAccessMediaStatus = transcription.IsAccessMediaStatus,
                IsBornDigital = transcription.IsBornDigital,
                IsConvertToDigital = transcription.IsConvertToDigital,
                IsOnline = transcription.IsOnline,
                IsPriority = transcription.IsPriority,
                IsRosetta = transcription.IsRosetta,
                IsRosettaForm = transcription.IsRosettaForm,
                Language = transcription.Language,
                TechnicalSpecification = transcription.TechnicalSpecification,
                MasterFileLocation = transcription.MasterFileLocation,
                OriginalMedium = transcription.OriginalMedium,
                OriginalMediumType = transcription.OriginalMediumType,
                Publisher = transcription.Publisher,
                ReasonForPriority = transcription.ReasonForPriority,
                RelationIsPartOf = transcription.RelationIsPartOf,
                Rights = transcription.Rights,
                ScopeAndContents = transcription.ScopeAndContents,
                SecondEditCompleted = transcription.SecondEditCompleted,
                TranscriberAssigned = transcription.TranscriberAssigned,
                Transcript = transcription.Transcript,
                TranscriptNote = transcription.TranscriptNote,
                TranscriptStatus = transcription.TranscriptStatus,
                Type = transcription.Type,
                TranscriptLocation = transcription.TranscriptLocation,
                UpdatedBy = transcription.UpdatedBy,
                UpdatedDate = transcription.UpdatedDate.Date,
                Identifier = transcription.Identifier,
                GeneralNote = transcription.GeneralNote,

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
                InterviewDate1 = transcriptionModel.InterviewDate1,
                InterviewDate2 = transcriptionModel.InterviewDate2,

                InterviewerNote = transcriptionModel.InterviewerNote,
                GeneralNote = transcriptionModel.GeneralNote,
                IsAudioFormat = transcriptionModel.IsAudioFormat,
                IsVideoFormat = transcriptionModel.IsVideoFormat,
                IsRestriction = transcriptionModel.IsRestriction,
                IsDarkArchive = transcriptionModel.IsDarkArchive,
                Keywords = transcriptionModel.Keywords,
                RestrictionNote = transcriptionModel.RestrictionNote,
                DarkArchiveNote = transcriptionModel.DarkArchiveNote,
                ReleaseForm = transcriptionModel.ReleaseForm,
                Subject = transcriptionModel.Subject,
                Title = transcriptionModel.Title,
                IsAccessMediaStatus = transcriptionModel.IsAccessMediaStatus,

                MetadataDraft = transcriptionModel.MetadataDraft,
                SentOut = transcriptionModel.SentOut,
                EquipmentNumber = transcriptionModel.EquipmentNumber,

                InterviewerDescription = GetSubString(transcriptionModel.InterviewerDescription),
                InterviewerKeywords = GetSubString(transcriptionModel.InterviewerKeywords),
                InterviewerSubjects = GetSubString(transcriptionModel.InterviewerSubjects),
                IsBornDigital = transcriptionModel.IsBornDigital,

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
                CoverageSpatial = transcriptionModel.CoverageSpatial,
                CoverageTemporal = transcriptionModel.CoverageTemporal,
                //EditWithCorrectionCompleted = "EditWithCorrectionCompleted",
                //FileName= "FileName",
                //FinalEditCompleted= "FinalEditCompleted",
                //FirstEditCompleted = "FirstEditCompleted",
                Format = transcriptionModel.Format,

                //InitialNote = "Initial Note",
                //IsAccessMediaStatus = true,
                //IsBornDigital = true,
                IsConvertToDigital = transcriptionModel.IsConvertToDigital,
                //IsRosetta = true,
                //IsRosettaForm = true,
                Language = transcriptionModel.Language,
                //MasterFileLocation= "MasterFileLocation",
                //OriginalMedium ="original mediunm",
                //OriginalMediumType = 1,
                Publisher = transcriptionModel.Publisher,
                RelationIsPartOf = transcriptionModel.RelationIsPartOf,
                Rights = transcriptionModel.Rights,
                ScopeAndContents = transcriptionModel.ScopeAndContents,
                //SecondEditCompleted = "SecondEditCompleted",

                //Transcript = "Transcript",
                //TranscriptLocation = 1,
                //TranscriptStatus = 1,
                Type = transcriptionModel.Type,
                IsOnline = transcriptionModel.IsOnline,

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
                AudioEquipmentUsed = interview.AudioEquipmentUsed,
                VideoEquipmentUsed = interview.VideoEquipmentUsed,

                InterviewDate = interview.InterviewDate,
                InterviewDate1 = interview.InterviewDate1,
                InterviewDate2 = interview.InterviewDate2,

                GeneralNote = interview.GeneralNote,
                InterviewerNote = interview.InterviewerNote,
                IsAudioFormat = interview.IsAudioFormat,
                IsRestriction = interview.IsRestriction,
                IsDarkArchive = interview.IsDarkArchive,
                Keywords = interview.Keywords,
                RestrictionNote = interview.RestrictionNote,
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
                InterviewDate1 = transcriptionModel.InterviewDate1,
                InterviewDate2 = transcriptionModel.InterviewDate2,

                InterviewerNote = transcriptionModel.InterviewerNote,
                GeneralNote = transcriptionModel.GeneralNote,

                IsAudioFormat = transcriptionModel.IsAudioFormat,
                IsRestriction = transcriptionModel.IsRestriction,
                IsDarkArchive = transcriptionModel.IsDarkArchive,
                Keywords = transcriptionModel.Keywords,
                RestrictionNote = transcriptionModel.RestrictionNote,
                DarkArchiveNote = transcriptionModel.DarkArchiveNote,
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

        #endregion

        #region User

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

        internal static List<PredefinedUserModel> ConvertToPredefinedUserModel(List<predefineduser> predefinedUsers)
        {
            List<PredefinedUserModel> list = new List<PredefinedUserModel>();

            foreach (predefineduser item in predefinedUsers)
            {
                list.Add(new PredefinedUserModel() { UserType = item.UserType, Name = item.Name });
            }

            return list;

        }

        #endregion
    }
}
