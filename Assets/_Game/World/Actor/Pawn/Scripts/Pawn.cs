namespace LOK1game
{
    public abstract class Pawn : Actor, IPawn
    {
        public abstract void OnInput(object sender);

        public abstract void OnPocces(PlayerControllerBase sender);
    }
}