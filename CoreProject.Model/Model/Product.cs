using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Model.Model
{
    public class Product 
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// 这里之所以没用RootEntity，是想保持和之前的数据库一致，主键是bID，不是Id
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int pID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string pName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string pContent { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string pSubmitter { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string pScan { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string pCategory { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime pUpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime pCreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string pRemark { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
    }
}
