using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EFUK;
using Soroeru.Common;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.View;
using UniRx;
using UnityEngine;
using VContainer;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class OptionController : BaseScreen
    {
        [SerializeField] private RectTransform cursor = default;
        [SerializeField] private OptionItemView[] items = default;
        private ReactiveProperty<int> _index;

        public override ScreenType type => ScreenType.Option;
        private int index => _index.Value;

        private IInputUseCase _inputUseCase;
        private BgmController _bgmController;
        private SeController _seController;

        [Inject]
        private void Construct(IInputUseCase inputUseCase, BgmController bgmController, SeController seController)
        {
            _inputUseCase = inputUseCase;
            _bgmController = bgmController;
            _seController = seController;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            _index = new ReactiveProperty<int>(0);
            _index
                .Subscribe(x =>
                {
                    cursor.transform.localPosition = items[x].localPosition;
                })
                .AddTo(this);

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isBack)
                {
                    _seController.Play(SeType.Decision);
                    this.Delay(UiConfig.POP_UP_ANIMATION_TIME, () => _index.Value = 0);
                    return ScreenType.Menu;
                }

                var vertical = _inputUseCase.verticalDown;
                if (vertical > 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _index.Value = MathfExtension.RepeatDecrement(index, 0, items.GetLastIndex());
                }
                else if (vertical < 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _index.Value = MathfExtension.RepeatIncrement(index, 0, items.GetLastIndex());
                }

                var volume = GetVolumeController(items[index].type);
                var horizontal = _inputUseCase.horizontalDown;
                if (horizontal != 0 && volume != null)
                {
                    var before = volume.volume;
                    var value = Mathf.Clamp01(before + (horizontal * 0.1f));
                    if (value.Equal(before) == false)
                    {
                        _seController.Play(SeType.MoveCursor);
                        volume.SetVolume(value);
                        items[index].SetEffectValue(volume.volume);
                    }
                }

                await UniTask.Yield(token);
            }
        }

        private IVolumeController GetVolumeController(OptionType optionType)
        {
            switch (optionType)
            {
                case OptionType.Bgm:
                    return _bgmController;
                case OptionType.Se:
                    return _seController;
                default:
                    throw new ArgumentOutOfRangeException(nameof(optionType), optionType, null);
            }
        }
    }
}