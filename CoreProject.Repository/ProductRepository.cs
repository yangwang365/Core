using CoreProject.IRepository;
using CoreProject.Model.Model;
using CoreProject.Repository.BASE;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreProject.Repository
{
    public class ProductRepository: BaseRepository<Product>,IProductRepository
    {
    }
}
