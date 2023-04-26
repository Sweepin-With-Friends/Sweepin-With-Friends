CREATE TABLE [dbo].[UserStat] (
    [UserID] INT NOT NULL,
    [StatID] INT NOT NULL,
    CONSTRAINT [PK_UserStat] PRIMARY KEY CLUSTERED ([UserID] ASC, [StatID] ASC),
    CONSTRAINT [FK_UserStat_Stats] FOREIGN KEY ([StatID]) REFERENCES [dbo].[Stats] ([StatID]),
    CONSTRAINT [FK_UserStat_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

