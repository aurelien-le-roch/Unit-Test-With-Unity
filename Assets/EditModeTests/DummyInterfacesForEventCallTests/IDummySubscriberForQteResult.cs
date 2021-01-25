namespace QteMiningTest
{
    public interface IDummySubscriberForQteResult
    {
        void HandleQteResult(QteResult result);
        void HandleJobOver();
        void HandleQteReset(QteResult result);
    }
}