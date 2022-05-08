namespace Soroeru.Common.Data.Entity
{
    public sealed class SceneEntity : BaseEntity<SceneName>
    {
        public SceneEntity()
        {
            Set(SceneName.Top);
        }
    }
}