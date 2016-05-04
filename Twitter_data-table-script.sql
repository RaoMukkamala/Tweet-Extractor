-- Updated Table. Modified by Raghava on 2016/05/01: changed the fields to latest version
GO

/****** Object:  Table [dbo].[TweetData]    Script Date: 5/1/2016 6:39:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TweetData](
	[TweetId] [bigint] NOT NULL,
	[TweetDate] [datetime] NULL,
	[Text] [nvarchar](500) NULL,
	[Truncated] [bit] NULL,
	[RetweetCount] [int] NULL,
	[FavoriteCount] [int] NULL,
	[Retweeted] [bit] NULL,
	[Favorited] [bit] NULL,
	[TweetLang] [varchar](10) NULL,
	[UserId] [bigint] NULL,
	[UserName] [nvarchar](200) NULL,
	[UserLocation] [nvarchar](250) NULL,
	[UserFollowerCount] [int] NULL,
	[UserFriendsCount] [int] NULL,
	[UserCreatedDate] [datetime] NULL,
	[UserFavoritesCount] [int] NULL,
	[UserTimeZone] [nvarchar](50) NULL,
	[UserUTCOffset] [varchar](10) NULL,
	[UserStatusesCount] [int] NULL,
	[UserLang] [varchar](10) NULL,
	[Hashtags] [nvarchar](250) NULL,
	[RetweetStatusBit] [bit] NULL,
	[OriginalTweetId] [bigint] NULL,
	[OriginalTweetUserId] [bigint] NULL,
	[OriginalTweetDate] [datetime] NULL
 CONSTRAINT [PK_TweetData] PRIMARY KEY CLUSTERED 
(
	[TweetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


