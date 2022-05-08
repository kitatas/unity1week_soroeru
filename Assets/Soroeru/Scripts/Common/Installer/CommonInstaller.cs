using System.Collections.Generic;
using Soroeru.Common.Data.DataStore;
using Soroeru.Common.Data.Entity;
using Soroeru.Common.Domain.Repository;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
using Soroeru.Common.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Soroeru.Common
{
    public sealed class CommonInstaller : LifetimeScope
    {
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            // Entity
            builder.Register<SceneEntity>(Lifetime.Singleton);

            // Repository
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<KeyboardInputUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimeUseCase>(Lifetime.Singleton);

            // Controller
            builder.RegisterEntryPoint<PauseController>();
            builder.Register<SceneLoader>(Lifetime.Singleton);

            // MonoBehaviour
            var bgm = FindObjectOfType<BgmController>();
            var se = FindObjectOfType<SeController>();
            var pause = FindObjectOfType<PauseView>();
            builder.RegisterInstance<BgmController>(bgm);
            builder.RegisterInstance<SeController>(se);
            builder.RegisterInstance<PauseView>(pause);
            builder.RegisterInstance<TransitionMaskView>(FindObjectOfType<TransitionMaskView>());

            autoInjectGameObjects = new List<GameObject>
            {
                bgm.gameObject,
                se.gameObject,
                pause.gameObject,
            };
        }
    }
}