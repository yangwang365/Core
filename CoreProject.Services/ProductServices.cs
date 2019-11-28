using System;
using System.Collections.Generic;
using System.Text;
using CoreProject.Model.Model;
using CoreProject.IServices;
using CoreProject.Services.BASE;
using CoreProject.Model.ViewModel;
using CoreProject.IRepository;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;

namespace CoreProject.Services
{
    public class ProductServices : BaseServices<Product>, IProductServices
    {
        IProductRepository _dal;
        IMapper _mapper;

        public ProductServices(IProductRepository dal, IMapper mapper)
        {
            _dal = dal;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取产品详情信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductViewModel> GetDetail(int id)
        {
            var productlist = await base.Query(a => a.IsDeleted == false, a => a.pID);
            var product = (await base.Query(a => a.pID == id)).FirstOrDefault();
            ProductViewModel models = null;
            if (product!=null)
            {
                Product prevproduct;
                Product nextproduct;
                int productIndex = productlist.FindIndex(item => item.pID == id);
                if (productIndex>=0)
                {
                    try
                    {
                        prevproduct = productIndex > 0 ? productlist[productIndex - 1] : null;
                        nextproduct = productIndex + 1 < productlist.Count() ? productlist[productIndex + 1] : null;
                        models = _mapper.Map<ProductViewModel>(product);//
                        if (nextproduct != null)
                        {
                            models.next = nextproduct.pName;
                            models.nextID = nextproduct.pID;
                        }

                        if (prevproduct != null)
                        {
                            models.previous = prevproduct.pName;
                            models.previousID = prevproduct.pID;
                        }
                        var entity2Viewmodel = _mapper.Map<Product>(models);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
            return models;
        }
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> GetProducts()
        {
            var productlist = await base.Query(a => a.pID > 0, a => a.pID);
            return productlist;
        }
    }
}
