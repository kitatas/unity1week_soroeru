using Soroeru.OutGame.Domain.UseCase;
using Soroeru.OutGame.Presentation.Controller;
using Soroeru.OutGame.Presentation.Presenter;
using Soroeru.OutGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Soroeru.OutGame.Installer
{
    public sealed class OutGameInstaller : LifetimeScope
    {
        [SerializeField] private CreditView creditView = default;
        [SerializeField] private MenuView menuView = default;
        [SerializeField] private OptionView optionView = default;
        [SerializeField] private StageSelectView stageSelectView = default;
        [SerializeField] private TopView topView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<ScreenUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<CreditController>(Lifetime.Scoped);
            builder.Register<MenuController>(Lifetime.Scoped);
            builder.Register<OptionController>(Lifetime.Scoped);
            builder.Register<ScreenController>(Lifetime.Scoped);
            builder.Register<StageSelectController>(Lifetime.Scoped);
            builder.Register<TopController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<ScreenPresenter>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<CreditView>(creditView);
            builder.RegisterInstance<MenuView>(menuView);
            builder.RegisterInstance<OptionView>(optionView);
            builder.RegisterInstance<StageSelectView>(stageSelectView);
            builder.RegisterInstance<TopView>(topView);
        }
    }
}