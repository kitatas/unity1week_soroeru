using System.Collections;
using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public class UniView : EnemyView
    {
        [SerializeField] private EnemyAmmoView enemyAmmoView = default;
        [SerializeField] private float shotInterval = default;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(Shot());
        }

        private IEnumerator Shot()
        {
            var interval = new WaitForSeconds(shotInterval);
            while (true)
            {
                yield return interval;

                var ammo = Instantiate(enemyAmmoView, transform.position, Quaternion.identity);
                ammo.Fire();
            }
        }
    }
}