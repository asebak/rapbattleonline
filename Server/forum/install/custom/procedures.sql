-- drop procedures if exists
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_post_newsfeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_post_newsfeed]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_newsfeed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_newsfeed]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_music]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_music]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_usersmusic]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_usersmusic]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_music_fromtitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_music_fromtitle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_music_specifictitle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_music_specifictitle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_featured_profile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_featured_profile]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_featured_music]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_featured_music]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_music]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_music]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_music_file]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_music_file]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_post_newscomment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_post_newscomment]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_randomprofiles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_randomprofiles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_submit_contactmsg]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_submit_contactmsg]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_contactmsg]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_contactmsg]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_feature_profile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_feature_profile]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_feature_music]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_feature_music]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_featuredmusic]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_featuredmusic]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_report_musictrack]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_report_musictrack]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_musictrack_ratingenabled]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_musictrack_ratingenabled]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_musictrack_report]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_musictrack_report]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_musictrack_ratingvalue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_musictrack_ratingvalue]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_musicrating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_musicrating]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_userid_from_name]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_userid_from_name]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_post_profilecomment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_post_profilecomment]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_profilecomments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_profilecomments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_hoodname]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_hoodname]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_hoodmembers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_hoodmembers]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_hoodmembers_invited]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_hoodmembers_invited]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_allhoods]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_allhoods]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_randomhoods]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_randomhoods]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_join_hood]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_join_hood]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_invite_hood_user]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_invite_hood_user]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_remove_hoodmember]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_remove_hoodmember]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_first_hoodmember]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_first_hoodmember]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_remove_hood]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_remove_hood]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_hoodmember_toadmin]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_hoodmember_toadmin]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_hooddetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_hooddetails]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_post_hoodcomment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_post_hoodcomment]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_hoodcomments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_hoodcomments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_writtenbattle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_writtenbattle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_users_writtenbattles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_users_writtenbattles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_recent_writtenbattles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_recent_writtenbattles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_all_writtenbattles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_all_writtenbattles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_join_writtenbattle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_join_writtenbattle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_writtenbattle_winner]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_writtenbattle_winner]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_writtenbattle_user1verse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_writtenbattle_user1verse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_writtenbattle_user2verse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_writtenbattle_user2verse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_writtenbattle_rating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_writtenbattle_rating]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_writtenbattle_ratingenabled]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_writtenbattle_ratingenabled]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_writtenbattle_votes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_writtenbattle_votes]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_writtenbattle_votesstatistics]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_writtenbattle_votesstatistics]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_post_writtenbattlecomment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_post_writtenbattlecomment]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_post_audiobattlecomment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_post_audiobattlecomment]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattle_votes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattle_votes]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattle_votesstatistics]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattle_votesstatistics]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_users_audiobattles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_users_audiobattles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_join_audiobattle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_join_audiobattle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_audiobattle_recording1]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_audiobattle_recording1]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_audiobattle_recording2]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_audiobattle_recording2]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_audiobattle_winner]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_audiobattle_winner]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_audiobattle_rating]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_audiobattle_rating]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattle_ratingenabled]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattle_ratingenabled]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_recent_audiobattles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_recent_audiobattles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattles]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_create_tournament]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_create_tournament]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_tournament]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_tournament]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_all_tournaments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_all_tournaments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_tournamentstate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_tournamentstate]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_tournament_users]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_tournament_users]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_profileheader]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_profileheader]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_profilebio]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_profilebio]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_user_siteprofile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_user_siteprofile]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_upload_audioverse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_upload_audioverse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_upload_writtenverse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_upload_writtenverse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_update_writtenverse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_update_writtenverse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_writtenverse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_writtenverse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_audioverse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_audioverse]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_user_audioverses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_user_audioverses]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_user_writtenverses]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_user_writtenverses]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audioverse_details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audioverse_details]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_users_music]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_users_music]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_all_contactmsgs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_all_contactmsgs]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_all_musicreports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_all_musicreports]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_newsfeed_comments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_newsfeed_comments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_user_profilecomments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_user_profilecomments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_hood_comments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_hood_comments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_writtenbattle_ratings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_writtenbattle_ratings]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_writtenbattle_comments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_writtenbattle_comments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattle_comments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattle_comments]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audiobattle_ratings]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audiobattle_ratings]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_news]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_news]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_music]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_music]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_usershoods]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_usershoods]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_writtenbattle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_writtenbattle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_audiobattle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_audiobattle]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_tournament_writtenmatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_tournament_writtenmatch]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_tournament_audiomatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_tournament_audiomatch]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_written_tournamentmatches]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_written_tournamentmatches]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_audio_tournamentmatches]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_audio_tournamentmatches]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_pm_count]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_pm_count]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_last_tournamententry_number]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_last_tournamententry_number]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_tournament_user]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_tournament_user]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_hood]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_hood]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_featuredprofile]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_featuredprofile]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_delete_feeditem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_delete_feeditem]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_add_feeditem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_add_feeditem]
GO
IF  exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_get_feed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rap_get_feed]
GO
--For Posting news--
CREATE PROCEDURE [dbo].[rap_post_newsfeed]
(
    @UserID int,
    @Title nvarchar(50),
    @DatePosted datetime,
    @Information nvarchar(MAX)
)
AS
INSERT INTO [dbo].[rap_NewsFeed](UserID,Title,DatePosted,Information,NumberOfComments)
VALUES
(@UserID,@Title,@DatePosted,@Information,0) 
GO

--Get all news feed--
CREATE PROCEDURE [dbo].[rap_get_newsfeed] AS
BEGIN  SELECT *
FROM [dbo].[rap_NewsFeed] ORDER BY NewsFeedID DESC
END
GO

--Get All music--
CREATE PROCEDURE [dbo].[rap_get_music] AS
BEGIN SELECT
a.MusicID,
a.UserID,
a.RatingValue,
b.Title,
b.Link,
b.Picture,
b.CanDownload,
b.DateAdded
FROM
[dbo].[rap_MusicRating] a
inner join [dbo].[rap_Music] b on a.MusicID = b.MusicID order by a.RatingValue ASC
END
GO

-- Get users Music--
CREATE PROCEDURE [dbo].[rap_get_usersmusic]
(
    @UserID int
)
AS
BEGIN SELECT *
FROM
[dbo].[rap_Music] 
WHERE
UserID = @UserID
END
GO

-- Get similar title music--
CREATE PROCEDURE [dbo].[rap_get_music_fromtitle]
(
    @Title nvarchar(max)
)
AS
BEGIN SELECT *
FROM
[dbo].[rap_Music] 
WHERE
Title like @Title
END
GO

-- Get all title music--
CREATE PROCEDURE [dbo].[rap_get_music_specifictitle]
(
    @Title nvarchar(max)
)
AS
BEGIN SELECT *
FROM
[dbo].[rap_Music] 
WHERE
Title = @Title
END
GO

-- Get all featured profiles--
CREATE PROCEDURE [dbo].[rap_get_featured_profile]
AS
BEGIN SELECT *
FROM
[dbo].[rap_ProfileFeatured] 
END
GO

-- Get all Featured music --
CREATE PROCEDURE [dbo].[rap_get_featured_music] AS
BEGIN SELECT
a.Picture,
a.CanDownload,
a.Link,
a.Title,
a.UserID,
a.MusicID,
a.DateAdded,
b.FeaturedUntil
FROM
[dbo].[rap_Music] a
inner join [dbo].[rap_MusicFeatured]  b on a.MusicID = b.MusicID
END
GO

--For adding music--
CREATE PROCEDURE [dbo].[rap_add_music]
(
    @ID int,
    @Title nvarchar(50),
    @Link nvarchar(255),
    @Picture nvarchar(255),
	@Rating float,
	@CanDownload bit,
	@DateAdded datetime,
	@MusicID int output
)
AS
INSERT INTO [dbo].[rap_Music](UserID,Title,Link,Rating,Picture,CanDownload,DateAdded)
VALUES
(@ID,@Title,@Link,@Rating,@Picture,@CanDownload,@DateAdded)
SET @MusicID = SCOPE_IDENTITY();
GO

-- Gets a specific music file--
CREATE PROCEDURE [dbo].[rap_get_music_file]
(
@MusicID int
)
AS
BEGIN SELECT *
FROM
[dbo].[rap_Music] 
WHERE
MusicID = @MusicID
END
GO
-- For posting a news comment--
CREATE PROCEDURE [dbo].[rap_post_newscomment]
(
    @NewsFeedID int,
    @UserID int,
    @Comment nvarchar(max),
    @DatePosted datetime
)
AS
UPDATE [dbo].[rap_NewsFeed] SET NumberOfComments = NumberOfComments + 1 WHERE NewsFeedID = @NewsFeedID
INSERT INTO [dbo].[rap_NewsFeedComments](NewsFeedID,UserID,Comment,DatePosted)
VALUES
(@NewsFeedID,@UserID,@Comment,@DatePosted)
GO

-- Gets a random users--
CREATE PROCEDURE [dbo].[rap_get_randomprofiles]
AS
BEGIN SELECT TOP 10 PERCENT UserID
FROM
[dbo].[rap_User] ORDER BY NEWID()
END
GO

-- For creating a contact message
CREATE PROCEDURE [dbo].[rap_submit_contactmsg]
(
    @UserID int,
    @Title nvarchar(50),
    @Content nvarchar(max)
)
AS
INSERT INTO [dbo].[rap_Contact](Title,Content,UserID)
VALUES
(@Title,@Content,@UserID)
GO

-- For deleting a contact message--
CREATE PROCEDURE [dbo].[rap_delete_contactmsg]
(
@ContactID int
)
AS
BEGIN
delete from [dbo].[rap_Contact]
 WHERE 
 ContactID = @ContactID
END
GO

-- For featuring a user profile--
CREATE PROCEDURE [dbo].[rap_feature_profile]
(
    @UserID int,
    @FeaturedUntil datetime
)
AS
INSERT INTO [dbo].[rap_ProfileFeatured](UserID,FeaturedUntil)
VALUES
(@UserID,@FeaturedUntil)
GO

-- For deleting a featured profile--
CREATE PROCEDURE [dbo].[rap_delete_featuredprofile]
(
@UserID int
)
AS
BEGIN
delete from [dbo].[rap_ProfileFeatured]
 WHERE 
 UserID = @UserID
END
GO

-- For featuring a music track--
CREATE PROCEDURE [dbo].[rap_feature_music]
(
    @MusicID int,
    @FeaturedUntil datetime
)
AS
INSERT INTO [dbo].[rap_MusicFeatured](MusicID,FeaturedUntil)
VALUES
(@MusicID,@FeaturedUntil)
GO

-- For deleting a featured music track--
CREATE PROCEDURE [dbo].[rap_delete_featuredmusic]
(
@MusicID int
)
AS
BEGIN
delete from [dbo].[rap_MusicFeatured]
 WHERE 
 MusicID = @MusicID
END
GO

-- For reporting a music track--
CREATE PROCEDURE [dbo].[rap_report_musictrack]
(
    @UserID int,
    @MusicID int,
	@Content nvarchar(max),
	@Confirmed bit
)
AS
INSERT INTO [dbo].[rap_MusicReports](UserID,MusicID,Information,Confirmed)
VALUES
(@UserID,@MusicID,@Content,@Confirmed)
GO

-- For deleting a music track report--
CREATE PROCEDURE [dbo].[rap_delete_musictrack_report]
(
@MusicID int,
@UserID int
)
AS
BEGIN
delete from [dbo].[rap_MusicReports]
 WHERE 
 MusicID = @MusicID AND UserID = @UserID
END
GO

-- Gets the music track rating is enabled for a user--
CREATE PROCEDURE [dbo].[rap_musictrack_ratingenabled]
(
@MusicID int,
@UserID int
)
AS
BEGIN 
SELECT RatingEnabled
FROM
[dbo].[rap_MusicRating] 
WHERE
MusicID = @MusicID AND UserID = @UserID
END
GO

-- Gets the music track rating value--
CREATE PROCEDURE [dbo].[rap_musictrack_ratingvalue]
(
@MusicID int
)
AS
BEGIN 
SELECT RatingValue
FROM
[dbo].[rap_MusicRating] 
WHERE
MusicID = @MusicID
END
GO

-- For adding a rating to a music track--
CREATE PROCEDURE [dbo].[rap_add_musicrating]
(
    @UserID int,
    @MusicID int,
	@RatingEnabled bit,
	@RatingValue int
)
AS
INSERT INTO [dbo].[rap_MusicRating](UserID,MusicID,RatingEnabled,RatingValue)
VALUES
(@UserID,@MusicID,@RatingEnabled,@RatingValue)
GO

-- Gets the userid from displayname--
CREATE PROCEDURE [dbo].[rap_userid_from_name]
(
@DisplayName nvarchar(50)
)
AS
BEGIN 
SELECT UserID
FROM
[dbo].[rap_User] 
WHERE
DisplayName = @DisplayName
END
GO

-- For posting a comment on a users profile--
CREATE PROCEDURE [dbo].[rap_post_profilecomment]
(
    @UserID int,
    @CommenterID int,
    @Comment nvarchar(max),
    @DatePosted datetime
)
AS
INSERT INTO [dbo].[rap_UserComments](UserID,CommenterID,Comment,DatePosted)
VALUES
(@UserID,@CommenterID,@Comment,@DatePosted)
GO

-- Gets a users profile comments--
CREATE PROCEDURE [dbo].[rap_get_profilecomments]
(
@UserID int
)
AS
BEGIN 
SELECT *
FROM
[dbo].[rap_UserComments] 
WHERE
UserID = @UserID
END
GO

-- Gets a specific hood name--
CREATE PROCEDURE [dbo].[rap_get_hoodname]
(
@Name nvarchar(50)
)
AS
BEGIN 
SELECT Name
FROM
[dbo].[rap_Hood] 
WHERE
Name = @Name
END
GO

-- Gets all the users that belong to a hood--
CREATE PROCEDURE [dbo].[rap_get_hoodmembers]
(
@HoodID int
)
AS
BEGIN 
SELECT *
FROM
[dbo].[rap_Hood], [dbo].[rap_HoodUsers] 
WHERE
[dbo].[rap_Hood].HoodID = @HoodID AND [dbo].[rap_HoodUsers].HoodID = @HoodID
END
GO

-- Get Hood Members Invited --
CREATE PROCEDURE [dbo].[rap_get_hoodmembers_invited]
(
@HoodID int
)
AS
BEGIN 
SELECT UserID
FROM
[dbo].[rap_HoodInvite]
WHERE
[dbo].[rap_HoodInvite].HoodID = @HoodID
END
GO

-- Gets all the hoods--
CREATE PROCEDURE [dbo].[rap_get_allhoods]
AS
BEGIN 
SELECT *
FROM
[dbo].[rap_Hood] a
inner join [dbo].[rap_HoodUsers] b
 on a.HoodID = b.HoodID
END
GO

-- Gets random hoods--
CREATE PROCEDURE [dbo].[rap_get_randomhoods]
AS
BEGIN 
SELECT TOP 10 PERCENT * 
FROM [dbo].[rap_Hood] a inner join [dbo].[rap_HoodUsers] b
on a.HoodID = b.HoodID ORDER BY NEWID()
END
GO

-- For joining a hood--
CREATE PROCEDURE [dbo].[rap_join_hood]
(
    @UserID int,
    @HoodID int,
	@IsAdmin bit
)
AS
INSERT INTO [dbo].[rap_HoodUsers](UserID, HoodID, IsAdmin)
VALUES
(@UserID,@HoodID,@IsAdmin)
GO

-- For Inviting a User to join a hood -- 
CREATE PROCEDURE [dbo].[rap_invite_hood_user]
(
    @UserID int,
    @HoodID int
)
AS
INSERT INTO [dbo].[rap_HoodInvite](UserID, HoodID)
VALUES
(@UserID,@HoodID)
GO

-- For removing a hood member--
CREATE PROCEDURE [dbo].[rap_remove_hoodmember]
(
@UserID int,
@HoodID int
)
AS
BEGIN
delete from [dbo].[rap_HoodUsers]
 WHERE 
UserID = @UserID AND HoodID = @HoodID
END
GO

-- Gets first  hoods member--
CREATE PROCEDURE [dbo].[rap_get_first_hoodmember]
(
@HoodID int
)
AS
BEGIN 
SELECT TOP 1 UserID
FROM [dbo].[rap_HoodUsers] 
WHERE
HoodID = @HoodID
END
GO

-- For removing a hood--
CREATE PROCEDURE [dbo].[rap_remove_hood]
(
@HoodID int
)
AS
BEGIN
delete from [dbo].[rap_Hood]
 WHERE 
HoodID = @HoodID
END
GO

-- For updating a hood member to admin--
CREATE PROCEDURE [dbo].[rap_hoodmember_toadmin]
(
@HoodID int,
@UserID int,
@IsAdmin bit
)
AS
BEGIN
UPDATE [dbo].[rap_HoodUsers]
SET IsAdmin = @IsAdmin 
WHERE 
HoodID = @HoodID AND UserID = @UserID
END
GO

-- For updating the hood details--
CREATE PROCEDURE [dbo].[rap_update_hooddetails]
(
@HoodID int,
@Description nvarchar(max),
@IsPublic bit
)
AS
BEGIN
UPDATE [dbo].[rap_Hood]
SET IsPublic = @IsPublic, Details = @Description
 WHERE 
 HoodID = @HoodID
END
GO

-- For posting a comment on a hoods profile--
CREATE PROCEDURE [dbo].[rap_post_hoodcomment]
(
    @HoodID int,
    @UserID int,
    @Comment nvarchar(max),
    @DatePosted datetime
)
AS
INSERT INTO [dbo].[rap_HoodComments](HoodID,UserID,Comment,DatePosted)
VALUES
(@HoodID,@UserID,@Comment,@DatePosted)
GO

-- For deleting all of a hoods comments--
CREATE PROCEDURE [dbo].[rap_delete_hoodcomments]
(
    @HoodID int
)
AS
DELETE FROM [dbo].[rap_HoodComments]
WHERE
HoodID = @HoodID
GO

-- Gets a specific written battle--
CREATE PROCEDURE [dbo].[rap_get_writtenbattle]
(
@WrittenBattleID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_WrittenBattle] 
WHERE
WrittenBattleID = @WrittenBattleID
END
GO

-- Gets all the users written battles--
CREATE PROCEDURE [dbo].[rap_get_users_writtenbattles]
(
@UserID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_WrittenBattle] 
WHERE
UserID1 = @UserID OR UserID2 = @UserID ORDER BY WrittenBattleID DESC
END
GO

-- Gets all the recent written battles--
CREATE PROCEDURE [dbo].[rap_get_recent_writtenbattles]
AS
BEGIN 
SELECT TOP 10 *
FROM [dbo].[rap_WrittenBattle] 
ORDER BY
 WrittenBattleID DESC
END
GO

-- Gets all the written battles--
CREATE PROCEDURE [dbo].[rap_get_all_writtenbattles]
AS
BEGIN 
SELECT *
FROM [dbo].[rap_WrittenBattle] 
END
GO

-- For updating and joining a written battle--
CREATE PROCEDURE [dbo].[rap_join_writtenbattle]
(
@UserID int,
@BattleID int
)
AS
BEGIN
UPDATE [dbo].[rap_WrittenBattle]
SET UserID2 = @UserID WHERE WrittenBattleID = @BattleID
END
GO

-- For updating a written battle winner--
CREATE PROCEDURE [dbo].[rap_update_writtenbattle_winner]
(
@WinnerID int,
@BattleID int,
@User1Overall float,
@User2Overall float
)
AS
BEGIN
UPDATE [dbo].[rap_WrittenBattle]
SET WinnerID = @WinnerID, User1Overall = @User1Overall, User2Overall = @User2Overall
  WHERE WrittenBattleID = @BattleID
END
GO

-- For updating a written battle content(user1)
CREATE PROCEDURE [dbo].[rap_update_writtenbattle_user1verse]
(
@BattleID int,
@Verse nvarchar(max)
)
AS
BEGIN
UPDATE [dbo].[rap_WrittenBattle]
SET User1Verse = @Verse
 WHERE WrittenBattleID = @BattleID
END
GO

-- For updating a written battle content(user2)--
CREATE PROCEDURE [dbo].[rap_update_writtenbattle_user2verse]
(
@BattleID int,
@Verse nvarchar(max)
)
AS
BEGIN
UPDATE [dbo].[rap_WrittenBattle]
SET User2Verse = @Verse
 WHERE WrittenBattleID = @BattleID
END
GO

-- For placing a rating on a written battle--
CREATE PROCEDURE [dbo].[rap_add_writtenbattle_rating]
(
@UserID int,
@WrittenBattleID int,
@RatingEnabled bit,
@User1Wordplay int,
@User1Metaphores int,
@User1Flow int,
@User1Multis int,
@User1Punchlines int, 
@User2Wordplay int,
@User2Metaphores int,
@User2Flow int,
@User2Multis int,
@User2Punchlines int
)
AS
INSERT INTO [dbo].[rap_WrittenBattleRating](UserID,WrittenBattleID,RatingEnabled,User1Wordplay,User1Metaphores,User1Flow,User1Multis,User1Punchlines,User2Wordplay,User2Metaphores,User2Flow,User2Multis,User2Punchlines)
VALUES
(@UserID,@WrittenBattleID,@RatingEnabled,@User1Wordplay,@User1Metaphores,@User1Flow,@User1Multis,@User1Punchlines, @User2Wordplay,@User2Metaphores,@User2Flow,@User2Multis,@User2Punchlines)
GO

-- Gets if the voting is enabled for a written battle--
CREATE PROCEDURE [dbo].[rap_get_writtenbattle_ratingenabled]
(
@UserID int,
@ID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_WrittenBattleRating]
WHERE
 UserID = @UserID AND WrittenBattleID = @ID
END
GO

-- Gets all the votes for a specific written battle--
CREATE PROCEDURE [dbo].[rap_get_writtenbattle_votes]
(
@ID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_WrittenBattleRating] a
inner join [dbo].[rap_WrittenBattle] b on
a.WrittenBattleID = b.WrittenBattleID WHERE
b.WrittenBattleID = @ID ORDER BY a.WrittenBattleRatingID DESC
END
GO

-- Gets all the votes  for statistics written battle--
CREATE PROCEDURE [dbo].[rap_get_writtenbattle_votesstatistics]
(
@UserID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_WrittenBattleRating] a
inner join [dbo].[rap_WrittenBattle] b on
a.WrittenBattleID = b.WrittenBattleID WHERE
b.UserID1 = @UserID or b.UserID2 = @UserID
END
GO

-- For posting a written battle comment--
CREATE PROCEDURE [dbo].[rap_post_writtenbattlecomment]
(
    @WrittenBattleID int,
    @UserID int,
    @Comment nvarchar(max),
    @DatePosted datetime
)
AS
INSERT INTO [dbo].[rap_WrittenBattleComments](WrittenBattleID,UserID,Comment,DatePosted)
VALUES
(@WrittenBattleID,@UserID,@Comment,@DatePosted)
GO

-- For posting a audio battle comment--
CREATE PROCEDURE [dbo].[rap_post_audiobattlecomment]
(
    @AudioBattleID int,
    @UserID int,
    @Comment nvarchar(max),
    @DatePosted datetime
)
AS
INSERT INTO [dbo].[rap_AudioBattleComments](AudioBattleID,UserID,Comment,DatePosted)
VALUES
(@AudioBattleID,@UserID,@Comment,@DatePosted)
GO

-- Gets all the votes for a specific audio battle--
CREATE PROCEDURE [dbo].[rap_get_audiobattle_votes]
(
@AudioBattleID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_AudioBattleRating] a
inner join [dbo].[rap_AudioBattle] b on
a.AudioBattleID = b.AudioBattleID WHERE
b.AudioBattleID = @AudioBattleID ORDER BY a.AudioBattleRatingID DESC
END
GO

-- Gets all the votes  for statistics audio battle--
CREATE PROCEDURE [dbo].[rap_get_audiobattle_votesstatistics]
(
@UserID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_AudioBattleRating] a
inner join [dbo].[rap_AudioBattle] b on
a.AudioBattleID = b.AudioBattleID WHERE
b.UserID1 = @UserID or b.UserID2 = @UserID
END
GO

-- Gets users audio battle--
CREATE PROCEDURE [dbo].[rap_get_users_audiobattles]
(
@UserID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_AudioBattle] 
WHERE UserID1 = @UserID OR UserID2 = @UserID ORDER BY AudioBattleID DESC
END
GO

-- Gets a specific audio battle--
CREATE PROCEDURE [dbo].[rap_get_audiobattle]
(
@ID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_AudioBattle] 
WHERE AudioBattleID = @ID
END
GO

-- For updating and joining a audio battle--
CREATE PROCEDURE [dbo].[rap_join_audiobattle]
(
@UserID int,
@BattleID int
)
AS
BEGIN
UPDATE [dbo].[rap_AudioBattle]
SET UserID2 = @UserID WHERE AudioBattleID = @BattleID
END
GO

-- For updating a audio battle content(user1)--
CREATE PROCEDURE [dbo].[rap_update_audiobattle_recording1]
(
@BattleID int,
@UserID int,
@FileLocation nvarchar(max),
@Beat int
)
AS
BEGIN
UPDATE [dbo].[rap_AudioBattle]
SET User1Recording = @FileLocation, Beat = @Beat
 WHERE AudioBattleID = @BattleID AND UserID1 = @UserID
END
GO

-- For updating a audio battle content(user2)--
CREATE PROCEDURE [dbo].[rap_update_audiobattle_recording2]
(
@BattleID int,
@UserID int,
@FileLocation nvarchar(max),
@Beat int
)
AS
BEGIN
UPDATE [dbo].[rap_AudioBattle]
SET User2Recording = @FileLocation, Beat = @Beat 
WHERE AudioBattleID = @BattleID AND UserID2 = @UserID
END
GO

-- For updating a audio battle winner--
CREATE PROCEDURE [dbo].[rap_update_audiobattle_winner]
(
@WinnerID int,
@BattleID int,
@User1Overall float,
@User2Overall float
)
AS
BEGIN
UPDATE [dbo].[rap_AudioBattle]
SET WinnerID = @WinnerID, User1Overall = @User1Overall, User2Overall = @User2Overall
  WHERE AudioBattleID = @BattleID
END
GO

-- For placing a rating on a audio battle--
CREATE PROCEDURE [dbo].[rap_add_audiobattle_rating]
(
@UserID int,
@AudioBattleID int,
@RatingEnabled bit,
@User1Wordplay int,
@User1Metaphores int,
@User1Flow int,
@User1Multis int,
@User1Punchlines int, 
@User2Wordplay int,
@User2Metaphores int,
@User2Flow int,
@User2Multis int,
@User2Punchlines int
)
AS
INSERT INTO [dbo].[rap_AudioBattleRating](UserID,AudioBattleID,RatingEnabled,User1Wordplay,User1Metaphores,User1Flow,User1Multis,User1Punchlines,User2Wordplay,User2Metaphores,User2Flow,User2Multis,User2Punchlines)
VALUES
(@UserID,@AudioBattleID,@RatingEnabled,@User1Wordplay,@User1Metaphores,@User1Flow,@User1Multis,@User1Punchlines, @User2Wordplay,@User2Metaphores,@User2Flow,@User2Multis,@User2Punchlines)
GO

-- Gets if the voting is enabled for a audio battle--
CREATE PROCEDURE [dbo].[rap_get_audiobattle_ratingenabled]
(
@UserID int,
@ID int
)
AS
BEGIN 
SELECT *
FROM [dbo].[rap_AudioBattleRating]
WHERE
 UserID = @UserID AND AudioBattleID = @ID
END
GO

-- Gets all the recent audio battles--
CREATE PROCEDURE [dbo].[rap_get_recent_audiobattles]
AS
BEGIN 
SELECT TOP 10 *
FROM [dbo].[rap_AudioBattle] 
ORDER BY
 AudioBattleID DESC
END
GO

-- Gets all the audio battles--
CREATE PROCEDURE [dbo].[rap_get_audiobattles]
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_AudioBattle] 
END
GO

-- For creating a tournament--
CREATE PROCEDURE [dbo].[rap_create_tournament]
(
    @Challengers int,
    @State int,
    @Type int,
    @Started datetime,
	@Rounds int
)
AS
INSERT INTO [dbo].[rap_Tournament](Contestants,TournamentState,BattleType,DateStarted,TotalRounds)
VALUES
(@Challengers,@State,@Type,@Started,@Rounds)
GO

-- Gets a specific tournament--
CREATE PROCEDURE [dbo].[rap_get_tournament]
(
@TournamentID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_Tournament] 
WHERE TournamentID = @TournamentID
END
GO

-- Gets all tournament--
CREATE PROCEDURE [dbo].[rap_get_all_tournaments]
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_Tournament] ORDER BY TournamentID DESC
END
GO

-- For updating tournament state--
CREATE PROCEDURE [dbo].[rap_update_tournamentstate]
(
@TournamentID int,
@TournamentState int
)
AS
BEGIN
UPDATE [dbo].[rap_Tournament]
SET TournamentState = @TournamentState 
WHERE TournamentID = @TournamentID
END
GO

-- Gets all users associated with a tournament--
CREATE PROCEDURE [dbo].[rap_get_tournament_users]
(
@TournamentID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_TournamentUser] 
WHERE TournamentID = @TournamentID
END
GO

-- For uploading a profile header--
CREATE PROCEDURE [dbo].[rap_update_profileheader]
(
    @UserID int,
    @HeaderImage nvarchar(max)
)
AS
INSERT INTO [dbo].[rap_SiteProfile](UserID, HeaderImage)
VALUES
(@UserID,@HeaderImage)
GO

-- For updating a user's bio --
CREATE PROCEDURE [dbo].[rap_update_profilebio]
(
    @UserID int,
    @Bio nvarchar(1000)
)
AS
INSERT INTO [dbo].[rap_SiteProfile](UserID, Bio)
VALUES
(@UserID,@Bio)
GO

-- Gets users site profile--
CREATE PROCEDURE [dbo].[rap_get_user_siteprofile]
(
@UserID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_SiteProfile] 
WHERE UserID = @UserID
END
GO

--inserts an audio verse--
CREATE PROCEDURE [dbo].[rap_upload_audioverse]
(
@UserID int,
@Title nvarchar(50),
@AudioPath nvarchar(max)
)
AS
INSERT INTO [dbo].[rap_VersesAudio](UserID, Title, AudioPath)
VALUES(@UserID,@Title,@AudioPath)
GO

--inserts an written verse--
CREATE PROCEDURE [dbo].[rap_upload_writtenverse]
(
@UserID int,
@Title nvarchar(50),
@Verse nvarchar(max)
)
AS
INSERT INTO [dbo].[rap_VersesWritten](UserID, Title, Verse)
VALUES(@UserID,@Title,@Verse)
GO
--updates written verse--[rap_update_writtenverse]
CREATE PROCEDURE [dbo].[rap_update_writtenverse]
(
@UserID int,
@VersesWrittenID int, 
@Verse nvarchar(max)
)
AS
BEGIN
UPDATE [dbo].[rap_VersesWritten]
SET Verse = @Verse
WHERE 
UserID = @UserID AND VersesWrittenID  = @VersesWrittenID
END
GO

-- For deleting a written verse--
CREATE PROCEDURE [dbo].[rap_delete_writtenverse]
(
@VersesWrittenID int
)
AS
BEGIN
delete from [dbo].[rap_VersesWritten]
 WHERE 
 VersesWrittenID = @VersesWrittenID
END
GO

-- For deleting a audio verse--
CREATE PROCEDURE [dbo].[rap_delete_audioverse]
(
@VersesAudioID int
)
AS
BEGIN
delete from [dbo].[rap_VersesAudio]
 WHERE 
VersesAudioID = @VersesAudioID
END
GO

-- Gets users audio verses--
CREATE PROCEDURE [dbo].[rap_get_user_audioverses]
(
@UserID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_VersesAudio] 
WHERE UserID = @UserID
END
GO

-- Gets users written verses--
CREATE PROCEDURE [dbo].[rap_get_user_writtenverses]
(
@UserID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_VersesWritten] 
WHERE UserID = @UserID
END
GO

-- Gets details of an audio verses--
CREATE PROCEDURE [dbo].[rap_get_audioverse_details]
(
@VersesAudioID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_VersesAudio] 
WHERE VersesAudioID = @VersesAudioID
END
GO

-- Gets details of an audio verses--
CREATE PROCEDURE [dbo].[rap_get_users_music]
(
@UserID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_Music] 
WHERE UserID = @UserID
ORDER BY MusicID DESC
END
GO

-- Gets all contact messages--
CREATE PROCEDURE [dbo].[rap_get_all_contactmsgs]
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_Contact] 
ORDER BY ContactID DESC
END
GO

-- Gets all music report messages--
CREATE PROCEDURE [dbo].[rap_get_all_musicreports]
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_MusicReports] 
END
GO

-- Gets news feed comments--
CREATE PROCEDURE [dbo].[rap_get_newsfeed_comments]
(
@NewsID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_NewsFeedComments] 
WHERE
NewsFeedID = @NewsID
END
GO

-- Gets users comments--
CREATE PROCEDURE [dbo].[rap_get_user_profilecomments]
(
@UserID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_UserComments] 
WHERE
UserID = @UserID
END
GO

-- Gets hood comments--
CREATE PROCEDURE [dbo].[rap_get_hood_comments]
(
@HoodID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_HoodComments] WHERE HoodID = @HoodID
END
GO

--Get written battle ratings--
CREATE PROCEDURE [dbo].[rap_get_writtenbattle_ratings]
(
@WrittenBattleID int
)
 AS
BEGIN SELECT *
FROM
[dbo].[rap_WrittenBattleRating] a
inner join [dbo].[rap_WrittenBattle] b on a.WrittenBattleID = b.WrittenBattleID
WHERE
b.WrittenBattleID = @WrittenBattleID ORDER BY a.WrittenBattleRatingID DESC
END
GO

-- Gets written battle comments--
CREATE PROCEDURE [dbo].[rap_get_writtenbattle_comments]
(
@WrittenBattleID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_WrittenBattleComments] WHERE WrittenBattleID = @WrittenBattleID
END
GO

-- Gets audio battle comments--
CREATE PROCEDURE [dbo].[rap_get_audiobattle_comments]
(
@AudioBattleID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_AudioBattleComments] WHERE AudioBattleID = @AudioBattleID
END
GO

--Get audio battle ratings--
CREATE PROCEDURE [dbo].[rap_get_audiobattle_ratings]
(
@AudioBattleID int
)
 AS
BEGIN SELECT *
FROM
[dbo].[rap_AudioBattleRating] a
inner join [dbo].[rap_AudioBattle] b on a.AudioBattleID = b.AudioBattleID
WHERE
b.AudioBattleID = @AudioBattleID ORDER BY a.AudioBattleRatingID DESC
END
GO

-- deletes a news feed item --
CREATE PROCEDURE [dbo].[rap_delete_news]
(
@NewsFeedID int
)
AS
BEGIN
delete from [dbo].[rap_NewsFeedComments]
 WHERE 
 NewsFeedID = @NewsFeedID

 delete from [dbo].[rap_NewsFeed]
 WHERE 
 NewsFeedID = @NewsFeedID
END
GO

-- deletes a music item --
CREATE PROCEDURE [dbo].[rap_delete_music]
(
@MusicID int
)
AS
BEGIN
delete from [dbo].[rap_MusicRating]
WHERE MusicID = @MusicID
delete from [dbo].[rap_MusicFeatured]
WHERE MusicID = @MusicID
delete from [dbo].[rap_Music]
WHERE MusicID = @MusicID
END
GO

-- Gets all the hood ids user belongs to--
CREATE PROCEDURE [dbo].[rap_get_usershoods]
(
@UserID int
)
AS
BEGIN 
SELECT  a.HoodID
FROM [dbo].[rap_Hood] a inner join [dbo].[rap_HoodUsers] b  on a.HoodID = b.HoodID
WHERE b.UserID = @UserID
GROUP BY a.HoodID
END
GO

-- creates a written battle --
CREATE PROCEDURE [dbo].[rap_add_writtenbattle]
(
@UserID1 int,
@UserID2 int,
@IsPublic bit,
@EndDate datetime,
@NumberOfBars int,
@WrittenBattleID int output
)
AS
INSERT INTO [dbo].[rap_WrittenBattle](UserID1, UserID2, IsPublic, EndDate, NumberOfBars)
VALUES(@UserID1,@UserID2,@IsPublic,@EndDate,@NumberOfBars) 
SET @WrittenBattleID = SCOPE_IDENTITY();
GO

-- creates a audio battle --
CREATE PROCEDURE [dbo].[rap_add_audiobattle]
(
@UserID1 int,
@UserID2 int,
@IsPublic bit,
@EndDate datetime,
@RecordingLength int,
@AudioBattleID int output
)
AS
INSERT INTO [dbo].[rap_AudioBattle](UserID1, UserID2, IsPublic, EndDate, RecordingLength)
VALUES
(@UserID1,@UserID2,@IsPublic,@EndDate,@RecordingLength)
 SET @AudioBattleID = SCOPE_IDENTITY();
GO

-- creates a tournament written battle --
CREATE PROCEDURE [dbo].[rap_add_tournament_writtenmatch]
(
@TournamentID int,
@MatchRound int,
@MatchPosition int,
@BattleID int
)
AS
INSERT INTO [dbo].[rap_TournamentMatch](TournamentID,MatchRound,MatchPosition,WrittenBattleID)
VALUES(@TournamentID,@MatchRound,@MatchPosition,@BattleID)
GO

-- creates a tournament audio battle --
CREATE PROCEDURE [dbo].[rap_add_tournament_audiomatch]
(
@TournamentID int,
@MatchRound int,
@MatchPosition int,
@BattleID int
)
AS
INSERT INTO [dbo].[rap_TournamentMatch](TournamentID,MatchRound,MatchPosition,AudioBattleID)
VALUES(@TournamentID,@MatchRound,@MatchPosition,@BattleID)
GO

-- Gets all the written tournament matches--
CREATE PROCEDURE [dbo].[rap_get_written_tournamentmatches]
(
@TournamentID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_TournamentMatch], [dbo].[rap_WrittenBattle]
 WHERE [dbo].[rap_TournamentMatch].TournamentID = @TournamentID
 AND [dbo].[rap_TournamentMatch].WrittenBattleID = [dbo].[rap_WrittenBattle].WrittenBattleID
END
GO

--Gets all the audio tournament matches --
CREATE PROCEDURE [dbo].[rap_get_audio_tournamentmatches]
(
@TournamentID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_TournamentMatch], [dbo].[rap_AudioBattle]
WHERE [dbo].[rap_TournamentMatch].TournamentID = @TournamentID
 AND [dbo].[rap_TournamentMatch].AudioBattleID = [dbo].[rap_AudioBattle].AudioBattleID
END
GO

-- Gets the private message count --
CREATE PROCEDURE [dbo].[rap_get_pm_count]
(
@UserID int
)
AS
BEGIN 
SELECT  COUNT(1)  
FROM [dbo].[rap_UserPMessage]
WHERE UserID = @UserID AND IsRead = 0 AND IsDeleted = 0 AND IsArchived = 0
END
GO

--Create Hood --
CREATE PROCEDURE [dbo].[rap_add_hood]
(
@Name nvarchar(50),
@Picture nvarchar(255),
@Details nvarchar(500),
@IsPublic bit,
@DateCreated datetime,
@HoodID int output
)
AS
INSERT INTO [dbo].[rap_Hood](Name,Picture,Details,IsPublic,DateCreated)
VALUES(@Name,@Picture,@Details,@IsPublic,@DateCreated)
SET @HoodID = SCOPE_IDENTITY();
GO

--Get last tournament user--
CREATE PROCEDURE [dbo].[rap_get_last_tournamententry_number]
(
@TournamentID int
)
AS
BEGIN 
SELECT  TOP 1 EntryNumber
FROM [dbo].[rap_TournamentUser]
WHERE TournamentID = @TournamentID ORDER BY EntryNumber DESC
END
GO

--add a tournament user --
CREATE PROCEDURE [dbo].[rap_add_tournament_user]
(
@TournamentID int,
@EntryNumber int,
@UserID int
)
AS
INSERT INTO [dbo].[rap_TournamentUser](TournamentID,EntryNumber,UserID) 
VALUES(@TournamentID,@EntryNumber,@UserID)
GO

--Get the users feed--
CREATE PROCEDURE [dbo].[rap_get_feed]
(
@UserID int
)
AS
BEGIN 
SELECT  *
FROM [dbo].[rap_Feed]
WHERE ToID = @UserID AND IsDeleted = 0 ORDER BY FeedID DESC
END
GO

--add a social feed item --
CREATE PROCEDURE [dbo].[rap_add_feeditem]
(
@ToID int,
@FromID int,
@TypeID int,
@ObjectID int,
@IsDeleted bit,
@Created datetime
)
AS
INSERT INTO [dbo].[rap_Feed](ToID,FromID,TypeID,ObjectID,IsDeleted,Created)
VALUES(@ToID,@FromID,@TypeID,@ObjectID,@IsDeleted,@Created)
GO

-- updated the column IsDeleted to be 1 --
CREATE PROCEDURE [dbo].[rap_delete_feeditem]
(
@FeedID int,
@IsDeleted bit
)
AS
BEGIN
UPDATE [dbo].[rap_Feed]
SET IsDeleted = @IsDeleted 
WHERE 
FeedID = @FeedID
END
GO