using System.Collections.Generic;
using System.Linq;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class RoleUseCase
    {
        public RoleUseCase()
        {
        }

        public PictureType RunRoleAction(IEnumerable<PictureType> list)
        {
            var distinct = list.Distinct().ToArray();
            return distinct.Length == 1 ? distinct[0] : PictureType.None;
        }
    }
}