using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
using CoreProject.Common.DB;

namespace CoreProject.Model.Seed
{
    public class MyContext
    {
        private static string _connectionString = BaseDBConfig.ConnectionString;
        private static DbType _dbType = (DbType)BaseDBConfig.DbType;
        private SqlSugarClient _db;


    }
}
