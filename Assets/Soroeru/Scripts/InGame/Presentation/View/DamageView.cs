using UnityEngine;

namespace Soroeru.InGame.Presentation.View
{
    public abstract class DamageView : MonoBehaviour
    {
        [SerializeField] private int attackPower = default;

        public int power => attackPower;
    }
}