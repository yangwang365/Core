using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Model.ViewModel
{
   public class ProductViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int pID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string pName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string pContent { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string pSubmitter { get; set; }

        /// <summary>
        /// 条形码
        /// </summary>
        public string pScan { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
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
        public string pRemark { get; set; }
        /// <summary>
        /// 上一篇
        /// </summary>
        public string previous { get; set; }

        /// <summary>
        /// 上一篇id
        /// </summary>
        public int previousID { get; set; }

        /// <summary>
        /// 下一篇
        /// </summary>
        public string next { get; set; }

        /// <summary>
        /// 下一篇id
        /// </summary>
        public int nextID { get; set; }
    }
}
