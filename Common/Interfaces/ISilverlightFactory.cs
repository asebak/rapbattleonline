namespace Common.Interfaces
{
    public interface ISilverlightFactory
    {
        void Build<T>(params T[] args);
    }
}