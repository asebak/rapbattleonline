namespace Common.Models
{
    public class MusicVoteModel
    {
        #region Properties

        public int PageUserId { get; set; }
        public int RatingValue { get; set; }
        public int UserId { get; set; }
        public int MusicId { get; set; }

        #endregion
    }
}