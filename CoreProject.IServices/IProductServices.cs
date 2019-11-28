using System;
using System.Collections.Generic;
using System.Text;
using CoreProject.Model.Model;
using CoreProject.IServices.BASE;
using System.Threading.Tasks;
using CoreProject.Model.ViewModel;

namespace CoreProject.IServices
{
    public interface IProductServices : IBaseServices<Product>
    {
        Task<List<Product>> GetProducts();
        Task<ProductViewModel> GetDetail(int id);
    }
}
