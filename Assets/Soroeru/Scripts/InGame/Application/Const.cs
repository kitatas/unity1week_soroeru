namespace Soroeru.InGame
{
    public sealed class LayerConfig
    {
        public const string DEFAULT = "Default";
        public const string GROUND = "Ground";
        public const string PLAYER = "Player";
        public const string DAMAGED = "Damaged";
        public const string DROP = "Drop";
        public const string COIN = "Coin";
    }

    public sealed class PlayerConfig
    {
        public const float INVINCIBLE_TIME = 1.5f;

        public const float KNOCK_BACK_TIME = 0.5f;
        public const float KNOCK_BACK_POWER = 80.0f;
        public const float KNOCK_UP_POWER = 2.0f;
    }

    public sealed class SlotConfig
    {
        public const float REEL_ROTATE_SPEED = 1.0f;
        public const float REEL_ROTATE_INTERVAL = 5.0f;
    }

    public sealed class ItemConfig
    {
        public const float DROP_COIN_LIFE_TIME = 2.0f;
    }

    public sealed class SlotItemConfig
    {
        public const int BOMB_IGNITE_COUNT = 5;
        public const float BOMB_EXPLODE_SIZE = 1.5f;
    }
}