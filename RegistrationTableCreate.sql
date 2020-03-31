USE [UserProfile]
GO

/****** Object:  Table [dbo].[Registrations]    Script Date: 3/31/2020 5:01:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Registrations](
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[EmailAddress] [nvarchar](50) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[ZipCode] [nvarchar](50) NULL,
	[Birthday] [datetime] NULL,
	[DLPhoto] [varbinary](max) NULL,
	[AdditionalInfo] [nvarchar](max) NULL,
	[DLPhotoPath] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


