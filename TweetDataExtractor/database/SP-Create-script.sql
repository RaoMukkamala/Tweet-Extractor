USE [TwitterDatabase]
GO

/****** Object:  StoredProcedure [dbo].[InsertTweet]    Script Date: 20-05-2015 13:01:27 ******/
DROP PROCEDURE [dbo].[InsertTweet]
GO

/****** Object:  StoredProcedure [dbo].[InsertTweet]    Script Date: 20-05-2015 13:01:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Raghava
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[InsertTweet] 
	-- Add the parameters for the stored procedure here
	@TweetId bigint = 0, 
	@TweetDate datetime null,
	@Text nvarchar(175) null,
	@Truncated bit null,
	@RetweetCount smallint null
    ,@FavoriteCount smallint null
    ,@Retweeted bit null
    ,@Favorited bit null
    ,@TweetLang varchar(10) null
    ,@UserId int null
    ,@UserName nvarchar(100) null
    ,@UserLocation nvarchar(50) null
    ,@UserFollowerCount smallint null
    ,@UserFriendsCount smallint null
    ,@UserCreatedDate datetime null
    ,@UserFavoritesCount smallint null
    ,@UserTimeZone nvarchar(50) null
    ,@UserUTCOffset varchar(10) null
    ,@UserStatusesCount int null
    ,@UserLang varchar(10) null
	,@Hashtags nvarchar(100) null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	DECLARE @existingTweetId bigint;

	SELECT @existingTweetId = TweetId from dbo.TweetData WHERE TweetId = @TweetId;

	if(@existingTweetId is null)     
		Insert Into dbo.TweetData values(@TweetId,@TweetDate, @Text, @Truncated, @RetweetCount, @FavoriteCount, @Retweeted, @Favorited,
		@TweetLang, @UserId, @UserName, @UserLocation, @UserFollowerCount, @UserFriendsCount, @UserCreatedDate, @UserFavoritesCount, @UserTimeZone, @UserUTCOffset, @UserStatusesCount, @UserLang, @Hashtags);    


END

GO


