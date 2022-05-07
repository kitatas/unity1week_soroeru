using System;

namespace Soroeru.OutGame
{
    public static class CustomExtension
    {
        public static string ConvertStreamName(this ScreenType type)
        {
            return type switch
            {
                ScreenType.Top    => StreamConfig.NAME_TOP,
                ScreenType.Menu   => StreamConfig.NAME_MENU,
                ScreenType.Stage  => StreamConfig.NAME_STAGE,
                ScreenType.Option => StreamConfig.NAME_OPTION,
                ScreenType.Credit => StreamConfig.NAME_CREDIT,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public static ScreenType ConvertScreenType(this MenuItemType type)
        {
            return type switch
            {
                MenuItemType.Stage  => ScreenType.Stage,
                MenuItemType.Option => ScreenType.Option,
                MenuItemType.Credit => ScreenType.Credit,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}