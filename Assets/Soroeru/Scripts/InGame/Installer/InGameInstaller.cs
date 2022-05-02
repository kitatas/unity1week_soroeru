using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Domain.Repository;
using Soroeru.InGame.Domain.UseCase;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Soroeru.InGame.Installer
{
    public sealed class InGameInstaller : LifetimeScope
    {
        [SerializeField] private PlayerData playerData = default;

        // Player's Component
        [SerializeField] private Rigidbody2D rigidbody2d = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<PlayerData>(playerData);

            // Repository
            builder.Register<PlayerRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<KeyboardInputUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerMoveUseCase>(Lifetime.Scoped).WithParameter(rigidbody2d);
            builder.Register<PlayerSpriteUseCase>(Lifetime.Scoped).WithParameter(spriteRenderer);
        }
    }
}