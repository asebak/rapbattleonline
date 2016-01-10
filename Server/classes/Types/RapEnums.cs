namespace FreestyleOnline.classes.Types
{

    #region Enums

    /// <summary>
    ///     Enum for Resource Files
    /// </summary>
    public enum RapResource
    {
        Languages,
        Pictures,
        Music,
        ForumIcons,
        MusicTracks,
        MusicTracksPictures,
        RapBattleAudio,
        Beats,
        Exceptions,
        HoodPictures,
        HeaderPictures,
        AudioVerses
    };

    /// <summary>
    ///     enum for an alert for rap cypher
    /// </summary>
    public enum RapAlert
    {
        Error,
        Normal
    };

    /// <summary>
    ///     Enum for a rap tournament progress status
    /// </summary>
    public enum RapTournamentStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Over = 2
    };

    /// <summary>
    ///     For social Meed Feed Type
    /// </summary>
    public enum RapSocialFeedType
    {
        MusicAdd = 0,
        MusicVote = 1,
        AudioBattleStart = 2,
        AudioBattleVote = 3,
        WrittenBattleStart = 4,
        WrittenBattleVote = 5,
        HoodJoin = 6,
        HoodAdd = 7,
        TournamentJoined = 8,
        WrittenVerseAdd = 9,
        AudioVerseAdd = 10,
        TournamentAdd = 11
    };

    #endregion
}