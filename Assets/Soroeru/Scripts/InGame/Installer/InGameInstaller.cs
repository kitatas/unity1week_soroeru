using Soroeru.InGame.Data.DataStore;
using Soroeru.InGame.Data.Entity;
using Soroeru.InGame.Domain.Factory;
using Soroeru.InGame.Domain.Repository;
using Soroeru.InGame.Domain.UseCase;
using Soroeru.InGame.Presentation.Controller;
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
        [SerializeField] private BuffTable buffTable = default;
        [SerializeField] private EnemyTable enemyTable = default;
        [SerializeField] private EquipTable equipTable = default;
        [SerializeField] private ItemTable itemTable = default;
        [SerializeField] private PlayerData playerData = default;
        [SerializeField] private SlotItemTable slotItemTable = default;

        [SerializeField] private CameraView cameraView = default;
        [SerializeField] private CoinCountView coinCountView = default;
        [SerializeField] private GoalView goalView = default;
        [SerializeField] private PlayerEquipView playerEquipView = default;
        [SerializeField] private PlayerView playerView = default;
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
            builder.RegisterInstance<BuffTable>(buffTable);
            builder.RegisterInstance<EnemyTable>(enemyTable);
            builder.RegisterInstance<EquipTable>(equipTable);
            builder.RegisterInstance<ItemTable>(itemTable);
            builder.RegisterInstance<PlayerData>(playerData);
            builder.RegisterInstance<SlotItemTable>(slotItemTable);

            // Entity
            builder.Register<CoinCountEntity>(Lifetime.Scoped);

            // Factory
            builder.Register<CoinFactory>(Lifetime.Scoped);
            builder.Register<EnemyFactory>(Lifetime.Scoped);

            // Repository
            builder.Register<AttackRepository>(Lifetime.Scoped);
            builder.Register<BuffRepository>(Lifetime.Scoped);
            builder.Register<EnemyRepository>(Lifetime.Scoped);
            builder.Register<EquipRepository>(Lifetime.Scoped);
            builder.Register<ItemRepository>(Lifetime.Scoped);
            builder.Register<PlayerRepository>(Lifetime.Scoped);
            builder.Register<SlotItemRepository>(Lifetime.Scoped);

            // UseCase
            builder.Register<BuffUseCase>(Lifetime.Scoped);
            builder.Register<CoinCountUseCase>(Lifetime.Scoped);
            builder.Register<CoinUseCase>(Lifetime.Scoped);
            builder.Register<EnemyUseCase>(Lifetime.Scoped);
            builder.Register<KeyboardInputUseCase>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<PlayerAnimatorUseCase>(Lifetime.Scoped).WithParameter(animator);
            builder.Register<PlayerAttackUseCase>(Lifetime.Scoped).WithParameter(playerTransform);
            builder.Register<PlayerEquipUseCase>(Lifetime.Scoped);
            builder.Register<PlayerMoveUseCase>(Lifetime.Scoped).WithParameter(rigidbody2d);
            builder.Register<PlayerRayUseCase>(Lifetime.Scoped).WithParameter(playerTransform);
            builder.Register<PlayerSpriteUseCase>(Lifetime.Scoped).WithParameter(spriteRenderer);
            builder.Register<SlotItemUseCase>(Lifetime.Scoped).WithParameter(playerTransform);

            // Controller
            builder.RegisterEntryPoint<CameraController>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PlayerController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<CoinCountPresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<PlayerEquipPresenter>(Lifetime.Scoped);

            // View
            builder.RegisterInstance<CameraView>(cameraView);
            builder.RegisterInstance<CoinCountView>(coinCountView);
            builder.RegisterInstance<GoalView>(goalView);
            builder.RegisterInstance<PlayerEquipView>(playerEquipView);
            builder.RegisterInstance<PlayerView>(playerView);
            builder.RegisterInstance<SlotView>(slotView);
        }
    }
}