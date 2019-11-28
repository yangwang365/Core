using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreProject.Model.Model;
using CoreProject.Model.ViewModel;

namespace CoreProject.API.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
           CreateMap<Product,ProductViewModel> ();
            CreateMap<ProductViewModel, Product>();
        }
    }
}
