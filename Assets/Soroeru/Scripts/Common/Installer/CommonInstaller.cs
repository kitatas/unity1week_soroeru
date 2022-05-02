using System.Collections.Generic;
using Soroeru.Common.Data.DataStore;
using Soroeru.Common.Domain.Repository;
using Soroeru.Common.Domain.UseCase;
using Soroeru.Common.Presentation.Controller;
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

            // Repository
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

            // MonoBehaviour
            var bgm = FindObjectOfType<BgmController>();
            var se = FindObjectOfType<SeController>();
            builder.RegisterInstance<BgmController>(bgm);
            builder.RegisterInstance<SeController>(se);

            autoInjectGameObjects = new List<GameObject>
            {
                bgm.gameObject,
                se.gameObject,
            };
        }
    }
}