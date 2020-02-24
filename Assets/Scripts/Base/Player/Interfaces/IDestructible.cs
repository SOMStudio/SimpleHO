namespace Base.Player.Interfaces
{
    public interface IDestructible
    {
        int Id { get; }
        void Damage(int value);
    }
}
