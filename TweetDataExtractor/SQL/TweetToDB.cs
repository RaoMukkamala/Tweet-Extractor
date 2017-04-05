using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using TweetDataExtractor.Json;

namespace TweetDataExtractor.SQL
{
    public class TweetToDb
    {
        private readonly TweetObject _tweetobj;

        public TweetToDb(TweetObject tweetobj)
        {
            _tweetobj = tweetobj;
        }

        public void InsertTweetToDb(StreamWriter dataErrorWriter)
        {
            var connectionString = ConfigManager.ConfigurationManagerInstance.DbConnString;


            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = "[dbo].[InsertTweet]",
                        CommandType = CommandType.StoredProcedure,
                        Connection = connection,
                        CommandTimeout = 0
                    };


                    connection.Open();

                    var paramarray = GetParameters();

                    command.Parameters.AddRange(paramarray);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                var msg = string.Format("{0}: Failed to persist to database. Error messgae:{1}", _tweetobj.id,
                    ex.Message);

                dataErrorWriter.WriteLine(msg);
            }
        }

        private SqlParameter[] GetParameters()
        {
            var tempDateTime = new DateTime(1900, 1, 1);

            var paramsToStore = new SqlParameter[25];

            paramsToStore[4] = new SqlParameter("@RetweetCount", SqlDbType.Int)
            {
                Value = _tweetobj.retweet_count
            };


            paramsToStore[5] = new SqlParameter("@FavoriteCount", SqlDbType.Int)
            {
                Value = _tweetobj.favorite_count
            };


            paramsToStore[12] = new SqlParameter("@UserFollowerCount", SqlDbType.Int)
            {
                Value = _tweetobj.user.followers_count
            };


            paramsToStore[13] = new SqlParameter("@UserFriendsCount", SqlDbType.Int)
            {
                Value = _tweetobj.user.friends_count
            };



            paramsToStore[15] = new SqlParameter("@UserFavoritesCount", SqlDbType.Int)
            {
                Value = _tweetobj.user.favourites_count
            };

            paramsToStore[9] = new SqlParameter("@UserId", SqlDbType.BigInt) {Value = _tweetobj.user.id};


            paramsToStore[18] = new SqlParameter("@UserStatusesCount", SqlDbType.Int)
            {
                Value = _tweetobj.user.statuses_count
            };

            paramsToStore[1] = new SqlParameter("@TweetDate", SqlDbType.DateTime)
            {
                Value = Utilities.TryParseTwitterDateTimeString(_tweetobj.created_at, tempDateTime)
            };


            paramsToStore[14] = new SqlParameter("@UserCreatedDate", SqlDbType.DateTime)
            {
                Value = Utilities.TryParseTwitterDateTimeString(_tweetobj.user.created_at, tempDateTime)
            };

            paramsToStore[3] = new SqlParameter("@Truncated", SqlDbType.Bit)
            {
                Value = Utilities.ConvertToBitValue(_tweetobj.truncated.ToString())
            };


            paramsToStore[6] = new SqlParameter("@Retweeted", SqlDbType.Bit)
            {
                Value = Utilities.ConvertToBitValue(_tweetobj.retweeted.ToString())
            };


            paramsToStore[7] = new SqlParameter("@Favorited", SqlDbType.Bit)
            {
                Value = Utilities.ConvertToBitValue(_tweetobj.favorited.ToString())
            };


            paramsToStore[0] = new SqlParameter("@TweetId", SqlDbType.BigInt) {Value = _tweetobj.id};


            // Raghava 20170202: sometimes we dont get lang from the tweet, then it fails, so fix for it
            var lang = _tweetobj.lang ?? string.Empty;

            paramsToStore[8] = new SqlParameter("@TweetLang", SqlDbType.VarChar) {Value = lang, Size = 10};


            // to convert into hours.
            paramsToStore[17] = new SqlParameter("@UserUTCOffset", SqlDbType.VarChar)
            {
                Value = Utilities.ConvertUTCOffset(_tweetobj.user.utc_offset),
                Size = 10
            };

            paramsToStore[19] = new SqlParameter("@UserLang", SqlDbType.VarChar)
            {
                Value = _tweetobj.user.lang,
                Size = 10
            };


            paramsToStore[2] = new SqlParameter("@Text", SqlDbType.NVarChar)
            {
             
                Size = 500
            };

            if (string.IsNullOrEmpty(_tweetobj.text))
            {
                paramsToStore[2].Value = DBNull.Value;
            }
            else
            {
                paramsToStore[2].Value = _tweetobj.text;
            }


            paramsToStore[10] = new SqlParameter("@UserName", SqlDbType.NVarChar)
            {
                Size = 200
            };

            if (string.IsNullOrEmpty(_tweetobj.user.name))
            {
                paramsToStore[10].Value = DBNull.Value;
            }
            else
            {
                paramsToStore[10].Value = _tweetobj.user.name;
            }



            paramsToStore[11] = new SqlParameter("@UserLocation", SqlDbType.NVarChar)
            {
                Size = 250
            };

            if (string.IsNullOrEmpty(_tweetobj.user.location))
            {
                paramsToStore[11].Value = DBNull.Value;
            }
            else
            {
                paramsToStore[11].Value = _tweetobj.user.location;
            }


            paramsToStore[16] = new SqlParameter("@UserTimeZone", SqlDbType.NVarChar)
            {
                Size = 50
            };


            if (string.IsNullOrEmpty(_tweetobj.user.time_zone))
            {
                paramsToStore[16].Value = DBNull.Value;
            }
            else
            {
                paramsToStore[16].Value = _tweetobj.user.time_zone;
            }


            paramsToStore[20] = new SqlParameter("@Hashtags", SqlDbType.NVarChar)
            {
                Size = 250
            };

            var hashtags = Utilities.ExtractHashTags(_tweetobj);

            if (string.IsNullOrEmpty(hashtags))
            {
                paramsToStore[20].Value = DBNull.Value;
            }
            else
            {
                paramsToStore[20].Value = hashtags;
            }


            paramsToStore[21] = new SqlParameter("@RetweetStatusBit", SqlDbType.Bit)
            {
                Value = (_tweetobj.retweeted_status == null) ? 0 : 1
            };


            paramsToStore[22] = new SqlParameter("@OriginalTweetId", SqlDbType.BigInt)
            {
                Value = (_tweetobj.retweeted_status != null) ? (object)_tweetobj.retweeted_status.id : DBNull.Value
            };


            paramsToStore[23] = new SqlParameter("@OriginalTweetUserId", SqlDbType.BigInt)
            {
                Value = (_tweetobj.retweeted_status != null) ? (object)_tweetobj.retweeted_status.user.id : DBNull.Value
            };


            paramsToStore[24] = new SqlParameter("@OriginalTweetDate", SqlDbType.DateTime);


            if (_tweetobj.retweeted_status != null)
            {
                paramsToStore[24].Value = Utilities.TryParseTwitterDateTimeString(_tweetobj.retweeted_status.created_at, tempDateTime);

            }
            else
            {
                paramsToStore[24].Value = DBNull.Value;
            }


            return paramsToStore;

            /*

            try
   {
   SqlParameter[] paramsToStore = new SqlParameter[20];
 
   paramsToStore[4] = new SqlParameter("@RetweetCount", SqlDbType.SmallInt);
   paramsToStore[4].Value = ?;
   paramsToStore[5] = new SqlParameter("@FavoriteCount", SqlDbType.SmallInt);
   paramsToStore[5].Value = ?;
   paramsToStore[12] = new SqlParameter("@UserFollowerCount", SqlDbType.SmallInt);
   paramsToStore[12].Value = ?;
   paramsToStore[13] = new SqlParameter("@UserFriendsCount", SqlDbType.SmallInt);
   paramsToStore[13].Value = ?;
   paramsToStore[15] = new SqlParameter("@UserFavoritesCount", SqlDbType.SmallInt);
   paramsToStore[15].Value = ?;
   paramsToStore[9] = new SqlParameter("@UserId", SqlDbType.Int);
   paramsToStore[9].Value = ?;
   paramsToStore[18] = new SqlParameter("@UserStatusesCount", SqlDbType.Int);
   paramsToStore[18].Value = ?;
   paramsToStore[1] = new SqlParameter("@TweetDate", SqlDbType.DateTime);
   paramsToStore[1].Value = ?;
   paramsToStore[14] = new SqlParameter("@UserCreatedDate", SqlDbType.DateTime);
   paramsToStore[14].Value = ?;
   paramsToStore[3] = new SqlParameter("@Truncated", SqlDbType.Bit);
   paramsToStore[3].Value = ?;
   paramsToStore[6] = new SqlParameter("@Retweeted", SqlDbType.Bit);
   paramsToStore[6].Value = ?;
   paramsToStore[7] = new SqlParameter("@Favorited", SqlDbType.Bit);
   paramsToStore[7].Value = ?;
   paramsToStore[0] = new SqlParameter("@TweetId", SqlDbType.BigInt);
   paramsToStore[0].Value = ?;
   paramsToStore[8] = new SqlParameter("@TweetLang", SqlDbType.VarChar);
   paramsToStore[8].Value = ?;
   paramsToStore[8].Size=10;
   paramsToStore[17] = new SqlParameter("@UserUTCOffset", SqlDbType.VarChar);
   paramsToStore[17].Value = ?;
   paramsToStore[17].Size=10;
   paramsToStore[19] = new SqlParameter("@UserLang", SqlDbType.VarChar);
   paramsToStore[19].Value = ?;
   paramsToStore[19].Size=10;
   paramsToStore[2] = new SqlParameter("@Text", SqlDbType.NVarChar);
   paramsToStore[2].Value = ?;
   paramsToStore[2].Size=350;
   paramsToStore[10] = new SqlParameter("@UserName", SqlDbType.NVarChar);
   paramsToStore[10].Value = ?;
   paramsToStore[10].Size=200;
   paramsToStore[11] = new SqlParameter("@UserLocation", SqlDbType.NVarChar);
   paramsToStore[11].Value = ?;
   paramsToStore[11].Size=100;
   paramsToStore[16] = new SqlParameter("@UserTimeZone", SqlDbType.NVarChar);
   paramsToStore[16].Value = ?;
   paramsToStore[16].Size=100;
 
   SqlHelper.ExecuteNonQuery(conn.Connection, CommandType.StoredProcedure,"InsertTweet", paramsToStore);
 
   }
catch(Exception excp)
   {
   }
finally
   {
   conn.Connection.Dispose();
   conn.Connection.Close();
   }

*/
        }
    }
}