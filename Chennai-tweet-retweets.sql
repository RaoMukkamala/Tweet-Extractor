


--select count(UserId) from TwitterUsers where UserId in 

select distinct [OriginalTweetUserId] OriginalTweetUsers FROM [ChennaiFloods].[dbo].[TweetData]

select distinct [UserId] RetweetUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetUserId] is not null


select 
[TweetId]
,[UserId]
,[OriginalTweetId]
,[OriginalTweetUserId]

FROM [ChennaiFloods].[dbo].[TweetData]
where [OriginalTweetId] is not null



select distinct
[UserId]
--,[OriginalTweetUserId]

FROM [ChennaiFloods].[dbo].[TweetData]
where [OriginalTweetId] is not null
--order by [OriginalTweetUserId]


select 
[OriginalTweetUserId],
COUNT([UserId]) RetweetCount
FROM [ChennaiFloods].[dbo].[TweetData]
where [OriginalTweetId] is not null
GROUP BY [OriginalTweetUserId]
order by RetweetCount desc


select * from TwitterUsers
where UserId in (53414786,3073336914)


select 
[OriginalTweetUserId]
,
UserId
, COUNT([TweetId]) RetweetCount
FROM [ChennaiFloods].[dbo].[TweetData]
where [OriginalTweetId] is not null --and [OriginalTweetUserId] = 53414786
GROUP BY [OriginalTweetUserId],UserId
order by RetweetCount desc



-- Original Users

select distinct [OriginalTweetUserId] OriginalTweetUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetId] is not null and 


select distinct [UserId] RetweetedUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetId] is not null


SELECT UserId,UserName, [UserFollowerCount], [UserStatusesCount] FROM TwitterUsers where 
UserId in 
	(SELECT DISTINCT [OriginalTweetUserId] OriginalTweetUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetId] is not null) 
	or
UserId in	(select distinct [UserId] RetweetedUsers FROM [ChennaiFloods].[dbo].[TweetData] where [OriginalTweetId] is not null)
	



select [OriginalTweetUserId],UserId, COUNT([TweetId]) as RetweetSCount FROM [ChennaiFloods].[dbo].[TweetData] S
where [OriginalTweetId] is not null  GROUP BY [OriginalTweetUserId],UserId order by RetweetSCount desc

