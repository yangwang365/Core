using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Model
{
    /// <summary>
    /// 条码信息类
    /// </summary>
    public class ScanInfos
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 条形码
        /// </summary>
        public string Scan { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public string Person { get; set; }
    }
}
