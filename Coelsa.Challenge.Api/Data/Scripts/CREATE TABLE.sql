SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NOT NULL,
	[LastName] [varchar](30) NOT NULL,
	[Company] [varchar](75) NULL,
	[Email] [varchar](75) NOT NULL,
	[PhoneNumber] [varchar](20) NULL
) ON [PRIMARY]
GO

