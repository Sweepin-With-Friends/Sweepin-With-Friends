CREATE TABLE [dbo].[Stats] (
    [StatID]       INT      NOT NULL,
    [TotalWins]    INT      NULL,
    [TotalLosses]  INT      NULL,
    [WinLossRatio] INT      NULL,
    [BestTimeSolo] TIME (7) NULL,
    [BestSoloAvg]  INT      NULL,
    [GamesPlayed]  INT      NULL,
    CONSTRAINT [PK_Stats] PRIMARY KEY CLUSTERED ([StatID] ASC)
);

