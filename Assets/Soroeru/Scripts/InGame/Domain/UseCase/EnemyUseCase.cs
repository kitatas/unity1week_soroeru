using System;
using Soroeru.InGame.Domain.Factory;
using Soroeru.InGame.Domain.Repository;
using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class EnemyUseCase
    {
        private readonly EnemyFactory _enemyFactory;
        private readonly EnemyRepository _enemyRepository;

        public EnemyUseCase(EnemyFactory enemyFactory, EnemyRepository enemyRepository)
        {
            _enemyFactory = enemyFactory;
            _enemyRepository = enemyRepository;
        }

        public EnemyView SetUp(EnemyType type, Vector3 position)
        {
            var data = _enemyRepository.FindEnemyData(type);
            if (data == null)
            {
                throw new Exception($"Can't find Enemy data. (type: {type})");
            }

            var enemy = _enemyFactory.Generate(data.enemy, position);
            return enemy;
        }
    }
}