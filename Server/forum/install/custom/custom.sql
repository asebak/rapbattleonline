--SQL QUERYS that will run once installed--

--Creates User Profile Commenting Table--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_UserComments]') and type in (N'U'))
CREATE TABLE [dbo].[rap_UserComments]
(
UserCommentsID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
CommenterID INT NOT NULL,
Comment NVARCHAR(MAX) NOT NULL,
DatePosted DATETIME NOT NULL,
PRIMARY KEY(UserCommentsID),
--CONSTRAINT fk_per_rap_userscommentsuserid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
--CONSTRAINT fk_per_rap_userscommentscommenterid FOREIGN KEY (CommenterID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the Music Tracks to be stored in the DB--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_Music]') and type in (N'U'))
CREATE TABLE [dbo].[rap_Music]
(
MusicID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
Title NVARCHAR (50) NOT NULL,
Link NVARCHAR(255) NOT NULL,
Rating FLOAT,
Picture NVARCHAR (255) NOT NULL,
CanDownload BIT NOT NULL,
DateAdded DATETIME NOT NULL,
PRIMARY KEY (MusicID),
CONSTRAINT fk_per_rap_music FOREIGN KEY (UserID) REFERENCES rap_User(UserID)
)
go

--Creates the Music Rating for each music track Table--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_MusicRating]') and type in (N'U'))
CREATE TABLE [dbo].[rap_MusicRating]
(
MusicRatingID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
MusicID INT NOT NULL,
RatingEnabled BIT NOT NULL,
RatingValue INT NOT NULL,
PRIMARY KEY (MusicRatingID),
CONSTRAINT fk_per_rap_musicrating FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_musicratingmusic FOREIGN KEY (MusicID) REFERENCES [dbo].[rap_Music](MusicID)
)
go

--Creates the Music Reports for each track a person reports--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_MusicReports]') and type in (N'U'))
CREATE TABLE [dbo].[rap_MusicReports]
(
ReportID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
MusicID INT NOT NULL,
Information NVARCHAR(255) NOT NULL,
Confirmed BIT,
PRIMARY KEY (ReportID),
CONSTRAINT fk_per_rap_reportuser FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_reportmusic FOREIGN KEY (MusicID) REFERENCES [dbo].[rap_Music](MusicID)
)
go

--Creates the News Feed DB--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_NewsFeed]') and type in (N'U'))
CREATE TABLE [dbo].[rap_NewsFeed]
(
NewsFeedID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
Title NVARCHAR(50) NOT NULL,
DatePosted DATETIME NOT NULL,
Information NVARCHAR(MAX),
NumberOfComments INT,
PRIMARY KEY (NewsFeedID),
CONSTRAINT fk_per_newsfeed FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go
--Creates the News Feed Comments DB--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_NewsFeedComments]') and type in (N'U'))
CREATE TABLE [dbo].[rap_NewsFeedComments]
(
NewsFeedCommentsID INT IDENTITY(1,1) NOT NULL,
NewsFeedID INT NOT NULL,
UserID INT NOT NULL,
Comment NVARCHAR(MAX) NOT NULL,
DatePosted DATETIME NOT NULL,
PRIMARY KEY (NewsFeedCommentsID),
CONSTRAINT fk_per_newscomments_feed FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_newsfeedcomments FOREIGN KEY (NewsFeedID) REFERENCES [dbo].[rap_NewsFeed](NewsFeedID)
)
go

--Creates the Contact Us Submission DB--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_Contact]') and type in (N'U'))
CREATE TABLE [dbo].[rap_Contact]
(
ContactID INT IDENTITY(1,1) NOT NULL,
Title NVARCHAR(50) NOT NULL,
Content NVARCHAR(MAX) NOT NULL,
UserID INT NOT NULL,
PRIMARY KEY (ContactID),
CONSTRAINT fk_per_contact_user FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the table for all the featured profiles--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_ProfileFeatured]') and type in (N'U'))
CREATE TABLE [dbo].[rap_ProfileFeatured]
(
ProfileFeaturedID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
FeaturedUntil DATETIME NOT NULL,
PRIMARY KEY (ProfileFeaturedID),
CONSTRAINT fk_per_rap_profilefeatured FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the table for all the featured tracks--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_MusicFeatured]') and type in (N'U'))
CREATE TABLE [dbo].[rap_MusicFeatured]
(
MusicFeaturedID INT IDENTITY(1,1) NOT NULL,
MusicID INT NOT NULL,
FeaturedUntil DATETIME NOT NULL,
PRIMARY KEY (MusicFeaturedID),
CONSTRAINT fk_per_rap_musicfeatured FOREIGN KEY (MusicID) REFERENCES [dbo].[rap_Music](MusicID)
)
go

--Creates the table for all Hoods--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_Hood]') and type in (N'U'))
CREATE TABLE [dbo].[rap_Hood]
(
HoodID INT IDENTITY(1,1) NOT NULL,
Name NVARCHAR(50) NOT NULL,
Picture NVARCHAR(255) NOT NULL,
Details NVARCHAR(500),
IsPublic  BIT NOT NULL,
DateCreated DATETIME NOT NULL,
PRIMARY KEY(HoodID)
)
go

--Creates the table for private hood invites--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_HoodInvite]') and type in (N'U'))
CREATE TABLE [dbo].[rap_HoodInvite]
(
HoodInviteID INT IDENTITY(1,1) NOT NULL,
HoodID INT NOT NULL,
UserID INT NOT NULL,
PRIMARY KEY(HoodInviteID),
CONSTRAINT fk_per_rap_hoodinvite_hoodid FOREIGN KEY (HoodID) REFERENCES [dbo].[rap_Hood](HoodID)
)
go

--Creates the table to store users linked to a hood--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_HoodUsers]') and type in (N'U'))
CREATE TABLE [dbo].[rap_HoodUsers]
(
HoodUsersID INT IDENTITY(1,1) NOT NULL,
HoodID INT NOT NULL,
UserID INT NOT NULL,
IsAdmin BIT NOT NULL,
PRIMARY KEY(HoodUsersID),
CONSTRAINT fk_per_rap_hooduserid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_hoodhoodid FOREIGN KEY (HoodID) REFERENCES [dbo].[rap_Hood](HoodID)
)
go

--Creates the table to store user comments linked to a hood--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_HoodComments]') and type in (N'U'))
CREATE TABLE [dbo].[rap_HoodComments]
(
HoodCommentsID INT IDENTITY(1,1) NOT NULL,
HoodID INT NOT NULL,
UserID INT NOT NULL,
Comment NVARCHAR(MAX) NOT NULL,
DatePosted DATETIME NOT NULL,
PRIMARY KEY(HoodCommentsID),
CONSTRAINT fk_per_rap_hoodcommenthoodid FOREIGN KEY (HoodID) REFERENCES [dbo].[rap_Hood](HoodID),
CONSTRAINT fk_per_rap_hoodcommentuserid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the written battle table--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_WrittenBattle]') and type in (N'U'))
CREATE TABLE [dbo].[rap_WrittenBattle]
(
WrittenBattleID INT IDENTITY(1,1) NOT NULL,
UserID1 INT NOT NULL,
UserID2 INT,
IsPublic BIT NOT NULL,
WinnerID INT,
User1Overall FLOAT,
User2Overall FLOAT,
EndDate DATETIME NOT NULL, 
NumberOfBars INT NOT NULL,
User1Verse NVARCHAR(MAX),
User2Verse NVARCHAR(MAX),
PRIMARY KEY(WrittenBattleID),
CONSTRAINT fk_per_rap_writtenbattle_userid1 FOREIGN KEY (UserID1)  REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_writtenbattle_userid2 FOREIGN KEY (UserID2)  REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_writtenbattle_winnerid FOREIGN KEY (WinnerID)  REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the table for voting in a rap battle
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_WrittenBattleRating]') and type in (N'U'))
CREATE TABLE [dbo].[rap_WrittenBattleRating]
(
WrittenBattleRatingID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
WrittenBattleID INT NOT NULL,
RatingEnabled BIT,
User1Wordplay INT NOT NULL,
User1Metaphores INT NOT NULL,
User1Flow INT NOT NULL,
User1Multis INT NOT NULL,
User1Punchlines INT NOT NULL,
User2Wordplay INT NOT NULL,
User2Metaphores INT NOT NULL,
User2Flow INT NOT NULL,
User2Multis INT NOT NULL,
User2Punchlines INT NOT NULL,
PRIMARY KEY(WrittenBattleRatingID),
CONSTRAINT fk_per_rap_writtenbattlerating_userid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_writtenbattlerating_writtenbattleid FOREIGN KEY (WrittenBattleID) REFERENCES [dbo].[rap_WrittenBattle](WrittenBattleID)
)
go

--Creates table for writtenbattle comments--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_WrittenBattleComments]') and type in (N'U'))
CREATE TABLE [dbo].[rap_WrittenBattleComments]
(
WrittenBattleCommentsID INT IDENTITY(1,1) NOT NULL,
WrittenBattleID INT NOT NULL,
UserID INT NOT NULL,
Comment NVARCHAR(MAX) NOT NULL,
DatePosted DATETIME NOT NULL,
PRIMARY KEY(WrittenBattleCommentsID),
CONSTRAINT fk_per_rap_writtenbattlecomments_writtenbattleid FOREIGN KEY (WrittenBattleID) REFERENCES [dbo].[rap_WrittenBattle](WrittenBattleID),
CONSTRAINT fk_per_rap_writtenbattlecomments_userid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the table for audiobattles--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_AudioBattle]') and type in (N'U'))
CREATE TABLE [dbo].[rap_AudioBattle]
(
AudioBattleID INT IDENTITY(1,1) NOT NULL,
UserID1 INT NOT NULL,
UserID2 INT,
IsPublic BIT NOT NULL,
WinnerID INT,
User1Overall FLOAT,
User2Overall FLOAT,
EndDate DATETIME NOT NULL,
RecordingLength INT NOT NULL,
User1Recording NVARCHAR(255),
User2Recording NVARCHAR(255),
Beat NVARCHAR(MAX),
PRIMARY KEY(AudioBattleID),
CONSTRAINT fk_per_rap_audiobattle_userid1 FOREIGN KEY (UserID1)  REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_audiobattle_userid2 FOREIGN KEY (UserID2)  REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_audiobattle_winnerid FOREIGN KEY (WinnerID)  REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the table for audiobattle ratings--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_AudioBattleRating]') and type in (N'U'))
CREATE TABLE [dbo].[rap_AudioBattleRating]
(
AudioBattleRatingID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
AudioBattleID INT NOT NULL,
RatingEnabled BIT,
User1Wordplay INT NOT NULL,
User1Metaphores INT NOT NULL,
User1Flow INT NOT NULL,
User1Multis INT NOT NULL,
User1Punchlines INT NOT NULL,
User2Wordplay INT NOT NULL,
User2Metaphores INT NOT NULL,
User2Flow INT NOT NULL,
User2Multis INT NOT NULL,
User2Punchlines INT NOT NULL,
PRIMARY KEY(AudioBattleRatingID),
CONSTRAINT fk_per_rap_audiobattlerating_userid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_rap_audiobattlerating_audiobattleid FOREIGN KEY (AudioBattleID) REFERENCES [dbo].[rap_AudioBattle](AudioBattleID)
)
go

--Creates the table for audiobattle comments--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_AudioBattleComments]') and type in (N'U'))
CREATE TABLE [dbo].[rap_AudioBattleComments]
(
AudioBattleCommentsID INT IDENTITY(1,1) NOT NULL,
AudioBattleID INT NOT NULL,
UserID INT NOT NULL,
Comment NVARCHAR(MAX) NOT NULL,
DatePosted DATETIME NOT NULL,
PRIMARY KEY(AudioBattleCommentsID),
CONSTRAINT fk_per_rap_audiobattlecomments_audiobattleid FOREIGN KEY (AudioBattleID) REFERENCES [dbo].[rap_AudioBattle](AudioBattleID),
CONSTRAINT fk_per_rap_audiobattlecomments_userid FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates the table for a tournament--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_Tournament]') and type in (N'U'))
CREATE TABLE [dbo].[rap_Tournament]
(
TournamentID INT IDENTITY(1,1) NOT NULL,
Contestants INT NOT NULL,
TournamentState INT NOT NULL,
BattleType INT NOT NULL,
DateStarted DATETIME NOT NULL,
TotalRounds INT NOT NULL,
PRIMARY KEY(TournamentID)
)
go

--Creates the table for a tournament match--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_TournamentMatch]') and type in (N'U'))
CREATE TABLE [dbo].[rap_TournamentMatch]
(
MatchID INT IDENTITY(1,1) NOT NULL,
TournamentID INT NOT NULL,
MatchRound INT NOT NULL,
MatchPosition INT NOT NULL,
WrittenBattleID INT,
AudioBattleID INT,
PRIMARY KEY(MatchID),
CONSTRAINT fk_per_tournamentid FOREIGN KEY (TournamentID) REFERENCES [dbo].[rap_Tournament](TournamentID),
CONSTRAINT fk_per_tournament_writtenbattleid FOREIGN KEY (WrittenBattleID) REFERENCES [dbo].[rap_WrittenBattle](WrittenBattleID),
CONSTRAINT fk_per_tournamentid_audiobattleid FOREIGN KEY (AudioBattleID) REFERENCES [dbo].[rap_AudioBattle](AudioBattleID)
)
go

--Creates the table for an entry of a tournament user--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_TournamentUser]') and type in (N'U'))
CREATE TABLE [dbo].[rap_TournamentUser]
(
TournamentUserID INT IDENTITY(1,1) NOT NULL,
TournamentID INT NOT NULL,
EntryNumber INT NOT NULL,
UserID INT NOT NULL,
PRIMARY KEY(TournamentUserID),
CONSTRAINT fk_per_tournamentid_user FOREIGN KEY (TournamentID) REFERENCES [dbo].[rap_Tournament](TournamentID),
CONSTRAINT fk_per_tournamentuser_id FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates a table to maintain the state of a users profile on main site not forum --
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_SiteProfile]') and type in (N'U'))
CREATE TABLE [dbo].[rap_SiteProfile]
(
SiteProfileID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
HeaderImage NVARCHAR(255),
Bio NVARCHAR(1000),
PRIMARY KEY(SiteProfileID),
CONSTRAINT fk_per_siteprofile_id FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates a table to maintain written verses --
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_VersesWritten]') and type in (N'U'))
CREATE TABLE [dbo].[rap_VersesWritten]
(
VersesWrittenID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
Title NVARCHAR(50) NOT NULL,
Verse NVARCHAR(MAX) NOT NULL,
PRIMARY KEY(VersesWrittenID),
CONSTRAINT fk_per_verseswritten_id FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates a table to maintain written verses --
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_VersesAudio]') and type in (N'U'))
CREATE TABLE [dbo].[rap_VersesAudio]
(
VersesAudioID INT IDENTITY(1,1) NOT NULL,
UserID INT NOT NULL,
Title NVARCHAR(50) NOT NULL,
AudioPath NVARCHAR(255) NOT NULL,
PRIMARY KEY(VersesAudioID),
CONSTRAINT fk_per_versesaudio_id FOREIGN KEY (UserID) REFERENCES [dbo].[rap_User](UserID)
)
go

--Creates a table to maintain Feed about users friends and hood members [audio battles, written battles, Music Added, Verses Added, Win/Loss Battle, etc]--
if not exists (select top 1 1 from sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rap_Feed]') and type in (N'U'))
CREATE TABLE [dbo].[rap_Feed]
(
FeedID INT IDENTITY(1,1) NOT NULL,
ToID INT NOT NULL,
FromID INT NOT NULL,
TypeID INT NOT NULL,
ObjectID INT NOT NULL,
IsDeleted BIT NOT NULL,
Created DATETIME NOT NULL,
PRIMARY KEY(FeedID),
CONSTRAINT fk_per_feed_toid FOREIGN KEY (ToID) REFERENCES [dbo].[rap_User](UserID),
CONSTRAINT fk_per_feed_fromid FOREIGN KEY (FromID) REFERENCES [dbo].[rap_User](UserID)
)
go