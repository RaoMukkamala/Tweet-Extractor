-- Updated by Raghava on 2016/05/01. Mapped the fields to the latest version.
GO
/****** Object:  StoredProcedure [dbo].[InsertTweet]    Script Date: 5/1/2016 6:52:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Raghava
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[InsertTweet] 
	-- Add the parameters for the stored procedure here
	@TweetId bigint = 0, 
	@TweetDate datetime null,
	@Text nvarchar(500) null,
	@Truncated bit null,
	@RetweetCount int null
    ,@FavoriteCount int null
    ,@Retweeted bit null
    ,@Favorited bit null
    ,@TweetLang varchar(10) null
    ,@UserId bigint null
    ,@UserName nvarchar(200) null
    ,@UserLocation nvarchar(250) null
    ,@UserFollowerCount int null
    ,@UserFriendsCount int null
    ,@UserCreatedDate datetime null
    ,@UserFavoritesCount int null
    ,@UserTimeZone nvarchar(50) null
    ,@UserUTCOffset varchar(10) null
    ,@UserStatusesCount int null
    ,@UserLang varchar(10) null
	,@Hashtags nvarchar(250) null
    ,@RetweetStatusBit bit null
    ,@OriginalTweetId bigint null
    ,@OriginalTweetUserId bigint null
    ,@OriginalTweetDate datetime null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	DECLARE @existingTweetId bigint;

	SELECT @existingTweetId = TweetId from dbo.TweetData WHERE TweetId = @TweetId;

	if(@existingTweetId is null)     
		Insert Into dbo.TweetData 
	  ([TweetId]
      ,[TweetDate]
      ,[Text]
      ,[Truncated]
      ,[RetweetCount]
      ,[FavoriteCount]
      ,[Retweeted]
      ,[Favorited]
      ,[TweetLang]
      ,[UserId]
      ,[UserName]
      ,[UserLocation]
      ,[UserFollowerCount]
      ,[UserFriendsCount]
      ,[UserCreatedDate]
      ,[UserFavoritesCount]
      ,[UserTimeZone]
      ,[UserUTCOffset]
      ,[UserStatusesCount]
      ,[UserLang]
      ,[Hashtags]
      ,[RetweetStatusBit]
      ,[OriginalTweetId]
      ,[OriginalTweetUserId]
      ,[OriginalTweetDate])
		VALUES(@TweetId,@TweetDate, @Text, @Truncated, @RetweetCount, @FavoriteCount, @Retweeted, @Favorited,
		@TweetLang, @UserId, @UserName, @UserLocation, @UserFollowerCount, @UserFriendsCount, @UserCreatedDate, 
		@UserFavoritesCount, @UserTimeZone, @UserUTCOffset, @UserStatusesCount, @UserLang, @Hashtags
		,@RetweetStatusBit ,@OriginalTweetId ,@OriginalTweetUserId ,@OriginalTweetDate );    


END

