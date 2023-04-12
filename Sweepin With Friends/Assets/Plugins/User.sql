CREATE TABLE [dbo].[User] (
    [UserID]       INT           NOT NULL,
    [Email]        VARCHAR (50)  NULL,
    [Username]     VARCHAR (30)  NULL,
    [AvatarURL]    VARCHAR (150) NULL,
    [Salt]         VARCHAR (25)  NULL,
    [PasswordHash] VARCHAR (300) NULL,
    [GameID]       INT           NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

