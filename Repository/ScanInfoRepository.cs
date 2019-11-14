using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using IRepository;
using Model;
using SqlSugar;
using Repository.BASE;

namespace Repository
{
    public class ScanInfoRepository : BaseRepository<ScanInfos>, IScanInfoRepository
    {
        #region 保留
        //private DbContext context;
        //private SqlSugarClient db;
        //private SimpleClient<ScanInfo> entityDB;

        //internal SqlSugarClient Db
        //{
        //    get { return db; }
        //    private set { db = value; }
        //}
        //public DbContext Context
        //{
        //    get { return context; }
        //    set { context = value; }
        //}
        //public ScanInfoRepository()
        //{
        //    DbContext.Init(BaseDBConfig.ConnectionString);
        //    context = DbContext.GetDbContext();
        //    db = context.Db;
        //    entityDB = context.GetEntityDB<ScanInfo>(db);
        //}
        //public int Add(ScanInfo model)
        //{
        //    //返回的i是long类型,这里你可以根据你的业务需要进行处理
        //    var i = db.Insertable(model).ExecuteReturnBigIdentity();
        //    return i.ObjToInt();
        //}

        //public bool Delete(ScanInfo model)
        //{
        //    var i = db.Deleteable(model).ExecuteCommand();
        //    return i > 0;
        //}

        //public List<ScanInfo> Query(Expression<Func<ScanInfo, bool>> whereExpression)
        //{
        //    return entityDB.GetList(whereExpression);

        //}

        //public int Sum(int i, int j)
        //{
        //    return i + j;
        //}

        //public bool Update(ScanInfo model)
        //{
        //    //这种方式会以主键为条件
        //    var i = db.Updateable(model).ExecuteCommand();
        //    return i > 0;
        //}
        #endregion

    }
}
