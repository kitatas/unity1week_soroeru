namespace Soroeru.InGame
{
    public sealed class LayerConfig
    {
        public const string GROUND = "Ground";
        public const string PLAYER = "Player";
        public const string DAMAGED = "Damaged";
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
}