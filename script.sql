Create DATABASE DaLi_GameExpress
Go
USE [DaLi_GameExpress]
GO
/****** Object:  Table [dbo].[DeveloperStudio]    Script Date: 12.06.2019 00:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperStudio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NOT NULL,
	[Foundingdate] [date] NOT NULL,
 CONSTRAINT [PK_DeveloperStudio] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 12.06.2019 00:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NOT NULL,
	[Rating] [int] NULL,
	[Release] [date] NOT NULL,
	[PublisherID] [int] NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameDeveloperStudio]    Script Date: 12.06.2019 00:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameDeveloperStudio](
	[GameID] [int] NOT NULL,
	[DeveloperStudioID] [int] NOT NULL,
 CONSTRAINT [PK_GameDeveloperStudio] PRIMARY KEY CLUSTERED 
(
	[GameID] ASC,
	[DeveloperStudioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GamePlatform]    Script Date: 12.06.2019 00:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GamePlatform](
	[GameID] [int] NOT NULL,
	[PlatformID] [int] NOT NULL,
 CONSTRAINT [PK_GamePlatform] PRIMARY KEY CLUSTERED 
(
	[GameID] ASC,
	[PlatformID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Platform]    Script Date: 12.06.2019 00:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Platform](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NOT NULL,
 CONSTRAINT [PK_Platform] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 12.06.2019 00:12:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [text] NOT NULL,
	[Foundingdate] [date] NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[DeveloperStudio] ON 

INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (1, N'Studio MDHR', CAST(N'2013-10-25' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (2, N'Epic Games', CAST(N'1991-04-20' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (3, N'People Can Fly', CAST(N'2002-02-22' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (5, N'Nintendo EAD', CAST(N'1893-09-30' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (6, N'PopCap Games', CAST(N'2000-05-01' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (7, N'GameFreak', CAST(N'1989-04-26' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (8, N'Electronic Arts', CAST(N'1982-05-28' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (9, N'Bandai Namco', CAST(N'2006-03-31' AS Date))
INSERT [dbo].[DeveloperStudio] ([ID], [Name], [Foundingdate]) VALUES (11, N'Santa Monica Studios', CAST(N'1999-05-12' AS Date))
SET IDENTITY_INSERT [dbo].[DeveloperStudio] OFF
SET IDENTITY_INSERT [dbo].[Game] ON 

INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (1, N'Cuphead', 83, CAST(N'2013-10-23' AS Date), 1)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (2, N'Plants vs Zombies', 80, CAST(N'2005-05-05' AS Date), 2)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (3, N'DragonBall Xenoverse 2', 70, CAST(N'2016-10-25' AS Date), 3)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (4, N'God of War 4', 97, CAST(N'2018-04-20' AS Date), 7)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (5, N'Pokemon Let''s Go Pickachu', 76, CAST(N'2018-11-16' AS Date), 4)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (6, N'Super Mario Maker', 83, CAST(N'2015-09-11' AS Date), 4)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (7, N'Assassin’s Creed Odyssey', 86, CAST(N'2018-12-04' AS Date), 5)
INSERT [dbo].[Game] ([ID], [Name], [Rating], [Release], [PublisherID]) VALUES (8, N'Fortnite', 51, CAST(N'2017-07-21' AS Date), 6)
SET IDENTITY_INSERT [dbo].[Game] OFF
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (1, 1)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (2, 6)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (3, 9)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (4, 11)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (5, 5)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (5, 7)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (6, 5)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (7, 8)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (8, 2)
INSERT [dbo].[GameDeveloperStudio] ([GameID], [DeveloperStudioID]) VALUES (8, 3)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (1, 1)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (1, 2)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (1, 6)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (2, 1)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (2, 7)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (2, 9)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (2, 10)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (3, 1)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (3, 2)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (3, 4)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (3, 6)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (4, 4)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (5, 6)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (6, 7)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (6, 8)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (7, 1)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (7, 2)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (7, 4)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (8, 1)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (8, 2)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (8, 4)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (8, 6)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (8, 9)
INSERT [dbo].[GamePlatform] ([GameID], [PlatformID]) VALUES (8, 10)
SET IDENTITY_INSERT [dbo].[Platform] ON 

INSERT [dbo].[Platform] ([ID], [Name]) VALUES (1, N'Microsoft Windows 10')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (2, N'Microsoft Xbox One')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (3, N'Microsoft Xbox 360')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (4, N'Sony Playstation 4')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (5, N'Sony Playstation 3')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (6, N'Nintendo Switch')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (7, N'Nindento 3DS')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (8, N'Nintendo WiiU')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (9, N'Google Android')
INSERT [dbo].[Platform] ([ID], [Name]) VALUES (10, N'Apple IOS')
SET IDENTITY_INSERT [dbo].[Platform] OFF
SET IDENTITY_INSERT [dbo].[Publisher] ON 

INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (1, N'Studio MDHR', CAST(N'2013-10-25' AS Date))
INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (2, N'Electronic Art', CAST(N'1982-05-28' AS Date))
INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (3, N'Bandai Namco Entertainment', CAST(N'1955-06-01' AS Date))
INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (4, N'Nintendo', CAST(N'1889-09-23' AS Date))
INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (5, N'Ubisoft', CAST(N'1986-03-28' AS Date))
INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (6, N'Epic Games', CAST(N'1991-04-20' AS Date))
INSERT [dbo].[Publisher] ([ID], [Name], [Foundingdate]) VALUES (7, N'Sony Interactive Entertainment', CAST(N'1968-07-18' AS Date))
SET IDENTITY_INSERT [dbo].[Publisher] OFF
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_Publisher] FOREIGN KEY([PublisherID])
REFERENCES [dbo].[Publisher] ([ID])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_Publisher]
GO
ALTER TABLE [dbo].[GameDeveloperStudio]  WITH CHECK ADD  CONSTRAINT [FK_GameDeveloperStudio_DeveloperStudio] FOREIGN KEY([DeveloperStudioID])
REFERENCES [dbo].[DeveloperStudio] ([ID])
GO
ALTER TABLE [dbo].[GameDeveloperStudio] CHECK CONSTRAINT [FK_GameDeveloperStudio_DeveloperStudio]
GO
ALTER TABLE [dbo].[GameDeveloperStudio]  WITH CHECK ADD  CONSTRAINT [FK_GameDeveloperStudio_Game] FOREIGN KEY([GameID])
REFERENCES [dbo].[Game] ([ID])
GO
ALTER TABLE [dbo].[GameDeveloperStudio] CHECK CONSTRAINT [FK_GameDeveloperStudio_Game]
GO
ALTER TABLE [dbo].[GamePlatform]  WITH CHECK ADD  CONSTRAINT [FK_GamePlatform_Game] FOREIGN KEY([GameID])
REFERENCES [dbo].[Game] ([ID])
GO
ALTER TABLE [dbo].[GamePlatform] CHECK CONSTRAINT [FK_GamePlatform_Game]
GO
ALTER TABLE [dbo].[GamePlatform]  WITH CHECK ADD  CONSTRAINT [FK_GamePlatform_Platform] FOREIGN KEY([PlatformID])
REFERENCES [dbo].[Platform] ([ID])
GO
ALTER TABLE [dbo].[GamePlatform] CHECK CONSTRAINT [FK_GamePlatform_Platform]
GO