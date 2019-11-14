using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CoreProject.Model;
using CoreProject.IRepository.BASE;

namespace CoreProject.IRepository
{
    public interface IScanInfoRepository : IBaseRepository<ScanInfos>
    {
        #region 保留
        //int Sum(int i, int j);
        //int Add( ScanInfo model);
        //bool Delete(ScanInfo model);
        //bool Update(ScanInfo model);
        //List<ScanInfo> Query(Expression<Func<ScanInfo, bool>> whereExpression);
        #endregion

    }
}
