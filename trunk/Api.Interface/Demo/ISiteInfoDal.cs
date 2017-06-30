using Api.Entity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Interface.Demo
{
    public interface ISiteInfoDal : IDependency
    {
        SiteInfoEntity Get(int id);

        string GetName();

    }
}
