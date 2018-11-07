CREATE TABLE [dbo].[Collections] (
    [Id]             SMALLINT      IDENTITY (1, 1) NOT NULL,
    [CollectionName] VARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Interviews] (
    [Id]                  INT           IDENTITY (1, 1) NOT NULL,
    [CollectionId]        SMALLINT      NOT NULL,
    [SubseriesId]         INT           NOT NULL,
    [Title]               VARCHAR (250) NOT NULL,
    [Interviewee]         VARCHAR (250) NOT NULL,
    [Interviewer]         VARCHAR (250) NOT NULL,
    [InterviewDate]       DATE          NOT NULL,
    [Place]               VARCHAR (250) NULL,
    [Description]         VARCHAR (500) NULL,
    [Keywords]            VARCHAR (500) NULL,
    [Subject]             VARCHAR (500) NULL,
    [ReleaseForm]         BIT           DEFAULT ((0)) NOT NULL,
    [IsRestriction]       BIT           DEFAULT ((0)) NOT NULL,
    [LegalNote]           VARCHAR (500) NULL,
    [IsAudioFormat]       BIT           DEFAULT ((0)) NOT NULL,
    [EquipmentUsed]       VARCHAR (250) NULL,
    [InterviewerNote]     VARCHAR (500) NULL,
    [IsTranscriptCreated] BIT           DEFAULT ((0)) NOT NULL,
    [CreatedBy]           INT           NOT NULL,
    [CreatedDate]         DATETIME      NOT NULL,
    [UpdatedBy]           INT           NOT NULL,
    [UpdatedDate]         DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Subjects] (
    [Id]          SMALLINT      IDENTITY (1, 1) NOT NULL,
    [SubjectName] VARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Subseries] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [CollectionId]  SMALLINT      NOT NULL,
    [SubseriesName] VARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Subseries_Collection] FOREIGN KEY ([CollectionId]) REFERENCES [dbo].[Collections] ([Id])
);

CREATE TABLE [dbo].[Transcriptions] (
    [Id]                          INT           IDENTITY (1, 1) NOT NULL,
    [ProjectCode]                 VARCHAR (50)  NOT NULL,
    [IsPriority]                  BIT           DEFAULT ((0)) NOT NULL,
    [ReasonForPriority]           VARCHAR (250) NULL,
    [InitialNote]                 VARCHAR (250) NULL,
    [CollectionId]                SMALLINT      NOT NULL,
    [SubseriesId]                 INT           NOT NULL,
    [Interviewee]                 VARCHAR (250) NOT NULL,
    [Interviewer]                 VARCHAR (250) NOT NULL,
    [InterviewDate]               DATETIME2 (7) NOT NULL,
    [Title]                       VARCHAR (250) NOT NULL,
    [Description]                 VARCHAR (500) NULL,
    [Keywords]                    VARCHAR (500) NULL,
    [Subject]                     VARCHAR (500) NULL,
    [ScopeAndContents]            VARCHAR (500) NULL,
    [Format]                      VARCHAR (250) NULL,
    [Type]                        VARCHAR (250) NULL,
    [Publisher]                   VARCHAR (250) NULL,
    [RelationIsPartOf]            VARCHAR (250) NULL,
    [CoverageSpatial]             VARCHAR (250) NULL,
    [CoverageTemporal]            VARCHAR (250) NULL,
    [Rights]                      VARCHAR (500) NULL,
    [Language]                    VARCHAR (100) NULL,
    [Identifier]                  VARCHAR (250) NULL,
    [Transcript]                  VARCHAR (250) NULL,
    [FileName]                    VARCHAR (250) NULL,
    [IsInContentDm]               BIT           DEFAULT ((0)) NOT NULL,
    [IsRosetta]                   BIT           DEFAULT ((0)) NOT NULL,
    [IsRosettaForm]               BIT           DEFAULT ((0)) NOT NULL,
    [IsRestriction]               BIT           DEFAULT ((0)) NOT NULL,
    [LegalNote]                   VARCHAR (500) NULL,
    [EquipmentUsed]               VARCHAR (250) NULL,
    [InterviewerNote]             VARCHAR (250) NULL,
    [ReleaseForm]                 BIT           DEFAULT ((0)) NOT NULL,
    [Place]                       VARCHAR (250) NULL,
    [TranscriberAssigned]         VARCHAR (250) NULL,
    [TranscriberCompleted]        DATETIME2 (7) NULL,
    [AuditCheckCompleted]         VARCHAR (250) NULL,
    [AuditCheckCompletedDate]     DATETIME2 (7) NULL,
    [FirstEditCompleted]          VARCHAR (250) NULL,
    [FirstEditCompletedDate]      DATETIME2 (7) NULL,
    [SecondEditCompleted]         VARCHAR (250) NULL,
    [SecondEditCompletedDate]     DATETIME2 (7) NULL,
    [DraftSentDate]               DATETIME2 (7) NULL,
    [EditWithCorrectionCompleted] VARCHAR (250) NULL,
    [EditWithCorrectionDate]      DATETIME2 (7) NULL,
    [FinalEditCompleted]          VARCHAR (250) NULL,
    [FinalEditDate]               DATETIME2 (7) NULL,
    [FinalSentDate]               DATETIME2 (7) NULL,
    [TranscriptStatus]            BIT           DEFAULT ((0)) NOT NULL,
    [TranscriptLocation]          TINYINT       NOT NULL,
    [TranscriptNote]              VARCHAR (500) NULL,
    [IsAudioFormat]               BIT           DEFAULT ((0)) NOT NULL,
    [IsBornDigital]               BIT           DEFAULT ((0)) NOT NULL,
    [OriginalMediumType]          TINYINT       DEFAULT ((0)) NOT NULL,
    [OriginalMedium]              VARCHAR (50)  NULL,
    [IsConvertToDigital]          BIT           DEFAULT ((0)) NOT NULL,
    [ConvertToDigitalDate]        DATETIME2 (7) NULL,
    [IsAccessMediaStatus]         BIT           DEFAULT ((0)) NOT NULL,
    [MasterFileLocation]          VARCHAR (500) NULL,
    [AccessFileLocation]          VARCHAR (500) NULL,
    [TranscriberLocation]         VARCHAR (500) NULL,
    [CreatedBy]                   INT           NOT NULL,
    [CreatedDate]                 DATETIME2 (7) NOT NULL,
    [UpdatedBy]                   INT           NOT NULL,
    [UpdatedDate]                 DATETIME2 (7) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[User] (
    [UserId]   INT          IDENTITY (1, 1) NOT NULL,
    [UserType] TINYINT      NULL,
    [Name]     VARCHAR (50) NULL,
    [Username] VARCHAR (50) NULL,
    [Password] VARCHAR (50) NULL,
    [Email]    VARCHAR (50) NULL,
    [Mobile]   VARCHAR (50) NULL,
    [RowId]    VARCHAR (50) NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserId] ASC)
);
