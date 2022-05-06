using Soroeru.InGame.Presentation.View;
using UnityEngine;

namespace Soroeru.InGame.Domain.Factory
{
    public sealed class EnemyFactory
    {
        public EnemyView Generate(EnemyView enemy, Vector3 position)
        {
            return Object.Instantiate(enemy, position, Quaternion.identity);
        }
    }
}