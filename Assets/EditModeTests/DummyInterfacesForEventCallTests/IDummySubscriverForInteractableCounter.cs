namespace interaclableTest
{
    public interface IDummySubscriverForInteractableCounter
    {
        void HandleCounterChange(int max,int current);
        void HandleMaxCounterHit();
    }
}