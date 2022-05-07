using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Doozy.Runtime.Signals;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class ScreenController
    {
        private readonly List<BaseScreen> _screens;

        public ScreenController(TopController top, MenuController menu, StageSelectController stageSelect,
            OptionController option, CreditController credit)
        {
            _screens = new List<BaseScreen>
            {
                top,
                menu,
                stageSelect,
                option,
                credit
            };
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            foreach (var screenView in _screens)
            {
                screenView.InitAsync(token).Forget();
            }

            await UniTask.Yield(token);
        }

        public async UniTask<ScreenType> TickAsync(ScreenType type, CancellationToken token)
        {
            var currentView = _screens.Find(x => x.type == type);
            if (currentView == null)
            {
                return ScreenType.None;
            }

            Signal.Send(StreamConfig.CATEGORY, type.ConvertStreamName());
            await UniTask.Delay(TimeSpan.FromSeconds(UiConfig.POP_UP_ANIMATION_TIME + 0.1f), cancellationToken: token);

            return await currentView.TickAsync(token);
        }
    }
}