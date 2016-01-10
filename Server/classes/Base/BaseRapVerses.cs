namespace FreestyleOnline.classes.Base
{
    public abstract class BaseRapVerses : RapClass
    {
        #region Properties

        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual int VerseId { get; set; }
        public virtual int UserId { get; set; }

        #endregion
    }
}