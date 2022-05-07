namespace Soroeru.Common.Data.Entity
{
    public sealed class SceneEntity : BaseEntity<SceneName>
    {
        public SceneEntity()
        {
            // TODO: fix
            Set(SceneName.Main1);
        }
    }
}