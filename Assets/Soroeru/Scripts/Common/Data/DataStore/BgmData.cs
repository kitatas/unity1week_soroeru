using UnityEngine;

namespace Soroeru.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmData), menuName = "DataTable/" + nameof(BgmData), order = 0)]
    public sealed class BgmData : ScriptableObject
    {
        [SerializeField] private BgmType bgmType = default;
        [SerializeField] private AudioClip audioClip = default;

        public BgmType type => bgmType;
        public AudioClip clip => audioClip;
    }
}