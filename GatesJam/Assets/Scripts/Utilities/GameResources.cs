using IboshEngine.Runtime.Utilities.Singleton;

namespace GatesJam.Utilities
{
    public class GameResources : IboshSingleton<GameResources>
    {
        public Player.Player TopPlayer;
        public Player.Player BottomPlayer;
    }
}