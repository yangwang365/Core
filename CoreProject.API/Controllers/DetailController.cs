using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreProject.IServices;
using Microsoft.AspNetCore.Mvc;

namespace CoreProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailController : Controller
    {
        readonly IProductServices _productServices;
        public DetailController(IProductServices detailServices)
        {
            _productServices = detailServices;
        }
        [HttpGet("{id}", Name = "Get")]
        public async Task<object> Get(int id)
        {
            var model = await _productServices.GetDetail(id);//调用该方法，这里 _blogArticleServices 是依赖注入的实例，不是类
            var data = new { success = true, data = model };
            return data;
        }

    }
}
