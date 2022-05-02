using UnityEngine;

namespace Soroeru.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeData), menuName = "DataTable/" + nameof(SeData), order = 0)]
    public sealed class SeData : ScriptableObject
    {
        [SerializeField] private SeType seType = default;
        [SerializeField] private AudioClip audioClip = default;

        public SeType type => seType;
        public AudioClip clip => audioClip;
    }
}