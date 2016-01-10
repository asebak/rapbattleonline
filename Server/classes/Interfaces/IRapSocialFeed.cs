using FreestyleOnline.classes.Types;

namespace FreestyleOnline.classes.Interfaces
{
    internal interface IRapSocialFeed<out T>
    {
        T Get(int userId);
        void Submit(int from, int objectId, RapSocialFeedType t);
        void Delete(int socialFeedId);
    }
}