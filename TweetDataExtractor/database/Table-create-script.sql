USE [TwitterDatabase]
GO

/****** Object:  Table [dbo].[TweetData]    Script Date: 20-05-2015 12:59:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TweetData](
	[TweetId] [bigint] NOT NULL,
	[TweetDate] [datetime] NULL,
	[Text] [nvarchar](175) NULL,
	[Truncated] [bit] NULL,
	[RetweetCount] [smallint] NULL,
	[FavoriteCount] [smallint] NULL,
	[Retweeted] [bit] NULL,
	[Favorited] [bit] NULL,
	[TweetLang] [varchar](10) NULL,
	[UserId] [int] NULL,
	[UserName] [nvarchar](100) NULL,
	[UserLocation] [nvarchar](50) NULL,
	[UserFollowerCount] [smallint] NULL,
	[UserFriendsCount] [smallint] NULL,
	[UserCreatedDate] [datetime] NULL,
	[UserFavoritesCount] [smallint] NULL,
	[UserTimeZone] [nvarchar](50) NULL,
	[UserUTCOffset] [varchar](10) NULL,
	[UserStatusesCount] [int] NULL,
	[UserLang] [varchar](10) NULL,
	[Hashtags] [nvarchar](100) NULL,
 CONSTRAINT [PK_TweetData] PRIMARY KEY CLUSTERED 
(
	[TweetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


