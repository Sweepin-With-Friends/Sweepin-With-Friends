CREATE TABLE [dbo].[Record] (
    [GameID]         INT      NOT NULL,
    [GameType]       INT      NULL,
    [PlayerOneScore] INT      NULL,
    [WinLoss]        BIT      NULL,
    [WinTime]        TIME (7) NULL,
    CONSTRAINT [PK_Record] PRIMARY KEY CLUSTERED ([GameID] ASC),
    CONSTRAINT [FK_Record_Game] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Game] ([GameID])
);

