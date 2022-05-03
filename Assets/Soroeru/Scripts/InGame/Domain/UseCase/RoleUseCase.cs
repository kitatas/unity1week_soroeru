using System.Collections.Generic;
using System.Linq;
using EFUK;

namespace Soroeru.InGame.Domain.UseCase
{
    public sealed class RoleUseCase
    {
        public RoleUseCase()
        {
        }

        public PictureType RunRoleAction(List<PictureType> list)
        {
            var enumerable = list.GroupBy(type => type, type => type);
            foreach (var grouping in enumerable)
            {
                var count = grouping.Count();

                // 全一致
                if (count == 3)
                {
                    return grouping.Key;
                }

                // 2つ一致
                if (count == 2)
                {
                    return grouping.Key;
                }
            }

            // TODO: どうするか
            // 全不一致
            return PictureType.None;
        }
    }
}