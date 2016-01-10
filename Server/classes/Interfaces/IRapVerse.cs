namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapVerse<out T>
    {
        void DeleteVerse(int verseId);
        void AddVerse(string verseTitle, string verseContent);
        T GetVerses(int userId);
    }
}