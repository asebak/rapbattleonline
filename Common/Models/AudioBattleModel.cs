#region Using

using System;
using Common.Types.Attributes;

#endregion

namespace Common.Models
{
    public class AudioBattleModel
    {
        #region Properties

        [NotNull]
        public int BattleId { get; set; }

        [NotNull]
        public int PageUserId { get; set; }

        [NotNull]
        public int UserId1 { get; set; }

        [NotNull]
        public string User1DisplayName { get; set; }

        [CanBeNull]
        public string User2DisplayName { get; set; }

        [CanBeNull]
        public int? UserId2 { get; set; }

        [NotNull]
        public TimeSpan Length { get; set; }

        [NotNull]
        public bool IsPublic { get; set; }

        [NotNull]
        public DateTime EndDate { get; set; }

        [CanBeNull]
        public float? User1Overall { get; set; }

        [CanBeNull]
        public float? User2Overall { get; set; }

        [CanBeNull]
        public int? WinnerId { get; set; }

        [CanBeNull]
        public int? Beat { get; set; }

        [CanBeNull]
        public string User1Avatar { get; set; }

        [CanBeNull]
        public string User2Avatar { get; set; }

        [CanBeNull]
        public string User1Audio { get; set; }

        [CanBeNull]
        public string User2Audio { get; set; }

        [CanBeNull]
        public string RecordedFileLocation { get; set; }

        [NotNull]
        public bool CanJoin { get; set; }

        [NotNull]
        public bool Player1Visible { get; set; }

        [NotNull]
        public bool Player2Visible { get; set; }

        [NotNull]
        public bool Recorder1Visible { get; set; }

        [NotNull]
        public bool Recorder2Visible { get; set; }

        [NotNull]
        public bool EnableBeats { get; set; }

        [NotNull]
        public bool ShowBeats { get; set; }

        #endregion
    }

    public class AudioBattleDspModel : AudioBattleModel
    {
        #region Properties

        [NotNull] public bool RequiresDspEngineering { get; set; }

        #endregion
    }
}