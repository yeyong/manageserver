using System;
using System.Collections;
using System.Data;
using System.Text;

using SAS.Cache;
using SAS.Common;
using SAS.Common.Generic;
using SAS.Entity;

namespace SAS.Sirius.Data
{
    public class DTOProvider
    {
        public static SAS.Common.Generic.List<TeamInfo> GetAlbumCategory()
        {
            SASCache cache = SASCache.GetCacheService();
            SAS.Common.Generic.List<TeamInfo> acic = cache.RetrieveObject("/Sirius/TeamInfoList") as SAS.Common.Generic.List<TeamInfo>;

            if (acic == null)
            {
                acic = new SAS.Common.Generic.List<TeamInfo>();
                acic = Data.DbProvider.GetInstance().GetAllTeamList();
                cache.AddObject("/Sirius/TeamInfoList", (ICollection)acic);
            }
            return acic;
        }
    }
}
