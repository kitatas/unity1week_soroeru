using System.Collections;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public sealed class BombView : BaseSlotItem
    {
        [SerializeField] private BombDamageView bombDamageView = default;
        private static readonly int _ignite = Animator.StringToHash("Ignite");

        public override void Generate(float lifeTime)
        {
            StartCoroutine(SetUp(lifeTime));
        }

        private IEnumerator SetUp(float lifeTime)
        {
            var igniteTime = 0.3f * SlotItemConfig.BOMB_IGNITE_COUNT;
            var explodeTime = 0.4f;
            var idleTime = lifeTime - igniteTime - explodeTime;
            yield return new WaitForSeconds(idleTime);

            var animator = GetComponent<Animator>();
            animator.SetBool(_ignite, true);
            yield return new WaitForSeconds(igniteTime);

            var bomb = Instantiate(bombDamageView, transform.position, Quaternion.identity);
            bomb.SetUp(explodeTime);
            Destroy(gameObject);
        }
    }
}