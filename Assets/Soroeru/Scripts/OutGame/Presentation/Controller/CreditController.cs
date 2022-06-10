using System.Threading;
using Cysharp.Threading.Tasks;
using Soroeru.Common;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.OutGame.Presentation.View;
using UnityEngine;

namespace Soroeru.OutGame.Presentation.Controller
{
    public sealed class CreditController : BaseScreen
    {
        public override ScreenType type => ScreenType.Credit;

        private readonly IInputUseCase _inputUseCase;
        private readonly SeController _seController;
        private readonly CreditView _creditView;

        public CreditController(IInputUseCase inputUseCase, SeController seController, CreditView creditView)
        {
            _inputUseCase = inputUseCase;
            _seController = seController;
            _creditView = creditView;
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<ScreenType> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_inputUseCase.isBack)
                {
                    _seController.Play(SeType.Decision);
                    _creditView.ResetPosition();
                    return ScreenType.Menu;
                }

                if (_inputUseCase.vertical != 0.0f)
                {
                    var deltaTime = Time.deltaTime;
                    _creditView.Tick(_inputUseCase.vertical * deltaTime);
                }

                await UniTask.Yield(token);
            }
        }
    }
}