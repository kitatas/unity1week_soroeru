using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EFUK;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.View;
using UniRx;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class OptionController : BaseScreen
    {
        public override ScreenType type => ScreenType.Option;

        private readonly IInputUseCase _inputUseCase;
        private readonly ItemIndexUseCase _indexUseCase;
        private readonly BgmController _bgmController;
        private readonly SeController _seController;
        private readonly OptionView _optionView;

        public OptionController(IInputUseCase inputUseCase, ItemIndexUseCase indexUseCase, BgmController bgmController,
            SeController seController, OptionView optionView)
        {
            _inputUseCase = inputUseCase;
            _indexUseCase = indexUseCase;
            _bgmController = bgmController;
            _seController = seController;
            _optionView = optionView;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            _indexUseCase.index
                .Subscribe(_optionView.SetCursorPosition)
                .AddTo(_optionView);

            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isBack)
                {
                    _seController.Play(SeType.Decision);
                    _optionView.ResetView(() => _indexUseCase.ResetValue());
                    return ScreenType.Menu;
                }

                var vertical = _inputUseCase.verticalDown;
                if (vertical > 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _indexUseCase.RepeatDecrement(_optionView.itemLastIndex);
                }
                else if (vertical < 0)
                {
                    _seController.Play(SeType.MoveCursor);
                    _indexUseCase.RepeatIncrement(_optionView.itemLastIndex);
                }

                var volume = GetVolumeController(_optionView.GetCurrentType(_indexUseCase.value));
                var horizontal = _inputUseCase.horizontalDown;
                if (horizontal != 0 && volume != null)
                {
                    var before = volume.volume;
                    var value = Mathf.Clamp01(before + (horizontal * 0.1f));
                    if (value.Equal(before) == false)
                    {
                        _seController.Play(SeType.MoveCursor);
                        volume.SetVolume(value);
                        _optionView.SetVolume(_indexUseCase.value, volume.volume);
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