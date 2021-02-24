public interface IHaveMover
{
    IMover Mover { get; }
    void ChangeMover(IMover newMover);
}