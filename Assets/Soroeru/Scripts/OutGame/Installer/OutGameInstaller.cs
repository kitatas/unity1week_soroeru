using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.Controller;
using Soroeru.OutGame.Presentation.Presenter;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Soroeru.OutGame.Installer
{
    public sealed class OutGameInstaller : LifetimeScope
    {
        [SerializeField] private CreditController creditController = default;
        [SerializeField] private MenuController menuController = default;
        [SerializeField] private OptionController optionController = default;
        [SerializeField] private StageSelectController stageSelectController = default;
        [SerializeField] private TopController topController = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<KeyboardInputUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<ScreenUseCase>(Lifetime.Scoped);

            // Controller
            builder.RegisterInstance<CreditController>(creditController);
            builder.RegisterInstance<MenuController>(menuController);
            builder.RegisterInstance<OptionController>(optionController);
            builder.Register<ScreenController>(Lifetime.Scoped);
            builder.RegisterInstance<StageSelectController>(stageSelectController);
            builder.RegisterInstance<TopController>(topController);

            // Presenter
            builder.RegisterEntryPoint<ScreenPresenter>(Lifetime.Scoped);
        }
    }
}