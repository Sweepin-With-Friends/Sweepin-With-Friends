CREATE TABLE [dbo].[StatRecord] (
    [StatID] INT NOT NULL,
    [GameID] INT NOT NULL,
    CONSTRAINT [PK_StatRecord] PRIMARY KEY CLUSTERED ([StatID] ASC, [GameID] ASC),
    CONSTRAINT [FK_StatRecord_Record] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Record] ([GameID]),
    CONSTRAINT [FK_StatRecord_Stats] FOREIGN KEY ([StatID]) REFERENCES [dbo].[Stats] ([StatID])
);

