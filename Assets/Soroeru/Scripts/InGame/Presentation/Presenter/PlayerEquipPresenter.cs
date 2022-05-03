using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Soroeru.InGame.Presentation.Presenter
{
    public sealed class PlayerEquipPresenter : IInitializable
    {
        private readonly PlayerEquipUseCase _equipUseCase;
        private readonly PlayerEquipView _equipView;

        public PlayerEquipPresenter(PlayerEquipUseCase equipUseCase, PlayerEquipView equipView)
        {
            _equipUseCase = equipUseCase;
            _equipView = equipView;
        }

        public void Initialize()
        {
            _equipUseCase.equipSprite
                .Subscribe(_equipView.SetSprite)
                .AddTo(_equipView);

            _equipUseCase.equipLifeTime
                .Subscribe(_equipView.SetLifeTime)
                .AddTo(_equipView);
        }
    }
}