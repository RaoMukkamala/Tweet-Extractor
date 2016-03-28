-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Raghava
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE InsertTweet 
	-- Add the parameters for the stored procedure here
	@TweetId bigint = 0, 
	@TweetDate datetime,
	@Text nvarchar(175),
	@Truncated bit,
	@RetweetCount smallint
    ,@FavoriteCount smallint
    ,@Retweeted bit
    ,@Favorited bit
    ,@TweetLang varchar(10)
    ,@UserId int
    ,@UserName nvarchar(100)
    ,@UserLocation nvarchar(50)
    ,@UserFollowerCount smallint
    ,@UserFriendsCount smallint
    ,@UserCreatedDate datetime
    ,@UserFavoritesCount smallint
    ,@UserTimeZone nvarchar(50)
    ,@UserUTCOffset varchar(10)
    ,@UserStatusesCount int
    ,@UserLang varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
	Insert Into dbo.TweetData values(@TweetId,@TweetDate, @Text, @Truncated, @RetweetCount, @FavoriteCount, @Retweeted, @Favorited,
	@TweetLang, @UserId, @UserName, @UserLocation, @UserFollowerCount, @UserFriendsCount, @UserCreatedDate, @UserFavoritesCount, @UserTimeZone, @UserUTCOffset, @UserStatusesCount, @UserLang);    


END
GO
