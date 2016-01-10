namespace Common.Models
{
    public class PMModel
    {
        #region Properties

        public string Details { get; set; }
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public string To { get; set; }
        public string SentBy { get; set; }
        public string Subject { get; set; }
        public int DateSent { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
        public bool IsReply { get; set; }
        public bool IsInOutBox { get; set; }
        public bool IsRead { get; set; }

        #endregion
    }
}