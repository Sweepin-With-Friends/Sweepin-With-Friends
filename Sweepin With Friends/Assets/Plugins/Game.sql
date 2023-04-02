CREATE TABLE [dbo].[Game] (
    [GameID]              INT      NOT NULL,
    [GameType]            INT      NOT NULL,
    [PlayerOneScore]      INT      NOT NULL,
    [PlayerTwoScore]      INT      NOT NULL,
    [BoardSizeDifficulty] INT      NOT NULL,
    [RankChange]          INT      NOT NULL,
    [WinLoss]             BIT      NOT NULL,
    [WinTime]             TIME (7) NOT NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([GameID] ASC)
);

