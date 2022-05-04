using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Data.Entity;
using Soroeru.InGame.Domain.Factory;
using Soroeru.InGame.Domain.Repository;
using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.Presenter;
using Soroeru.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Soroeru.InGame.Installer
{
    public sealed class InGameInstaller : LifetimeScope
    {
        [SerializeField] private AttackTable attackTable = default;
        [SerializeField] private EquipTable equipTable = default;
        [SerializeField] private ItemTable itemTable = default;
        [SerializeField] private PlayerData playerData = default;
        [SerializeField] private SlotItemTable slotItemTable = default;

        [SerializeField] private CoinCountView coinCountView = default;
        [SerializeField] private PlayerEquipView playerEquipView = default;
        [SerializeField] private SlotView slotView = default;

        // Player's Component
        [SerializeField] private Animator animator = default;
        [SerializeField] private Rigidbody2D rigidbody2d = default;
        [SerializeField] private Transform playerTransform = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<AttackTable>(attackTable);
            builder.RegisterInstance<EquipTable>(equipTable);
            builder.RegisterInstance<ItemTable>(itemTable);
            builder.RegisterInstance<PlayerData>(playerData);
            builder.RegisterInstance<SlotItemTable>(slotItemTable);

            // Entity
            builder.Register<CoinCountEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<CoinFactory>(Lifetime.Scoped);

            // Repository
            builder.Register<AttackRepository>(Lifetime.Scoped);
            builder.Register<EquipRepository>(Lifetime.Scoped);
            builder.Register<ItemRepository>(Lifetime.Scoped);
            builder.Register<PlayerRepository>(Lifetime.Scoped);
            builder.Register<SlotItemRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<CoinCountUseCase>(Lifetime.Scoped);
            builder.Register<CoinUseCase>(Lifetime.Scoped);
            builder.Register<KeyboardInputUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerAnimatorUseCase>(Lifetime.Scoped).WithParameter(animator);
            builder.Register<PlayerAttackUseCase>(Lifetime.Scoped).WithParameter(playerTransform);
            builder.Register<PlayerEquipUseCase>(Lifetime.Scoped);
            builder.Register<PlayerMoveUseCase>(Lifetime.Scoped).WithParameter(rigidbody2d);
            builder.Register<PlayerRayUseCase>(Lifetime.Scoped).WithParameter(playerTransform);
            builder.Register<PlayerSpriteUseCase>(Lifetime.Scoped).WithParameter(spriteRenderer);
            builder.Register<RoleUseCase>(Lifetime.Scoped);
            builder.Register<SlotItemUseCase>(Lifetime.Scoped).WithParameter(playerTransform);

            // Presenter
            builder.RegisterEntryPoint<CoinCountPresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PlayerEquipPresenter>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<CoinCountView>(coinCountView);
            builder.RegisterInstance<PlayerEquipView>(playerEquipView);
            builder.RegisterInstance<SlotView>(slotView);
        }
    }
}