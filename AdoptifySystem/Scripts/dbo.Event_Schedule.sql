CREATE TABLE [dbo].[Event_Schedule] (
    [EventID]     INT            IDENTITY (1, 1) NOT NULL,
    [Subject]     NVARCHAR (100) NULL,
    [Description] NVARCHAR (300) NULL,
    [Start]       DATETIME       NULL,
    [EventEnd]    DATETIME       NULL,
    [ThemeColour] NCHAR (10)     NULL,
    [IsFullDay]   BIT            NULL,
    CONSTRAINT [PK_Event_Schedule] PRIMARY KEY CLUSTERED ([EventID] ASC)
);

