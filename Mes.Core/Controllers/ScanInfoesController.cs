using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IServices;
using Services;
using Model;
//using IRepository;

namespace Mes.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanInfoesController : ControllerBase
    {
        //private readonly MyContext _context;
        readonly IScanInfoServices scanInfoServices;

        public ScanInfoesController(IScanInfoServices scanInfoServices)
        {
            this.scanInfoServices = scanInfoServices;
            //_context = context;
        }
        #region 自动生成
        //// GET: api/ScanInfoes
        ///// <summary>
        ///// 获取所有信息列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ScanInfo>>> GetScaninfos()
        //{
        //    return await _context.Scaninfos.ToListAsync();
        //}
        //// GET: api/ScanInfoes/5
        ///// <summary>
        ///// 获取制定信息列表
        ///// </summary>
        ///// <param name="id">ID</param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ScanInfo>> GetScanInfo(int id)
        //{
        //    var scanInfo = await _context.Scaninfos.FindAsync(id);

        //    if (scanInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return scanInfo;
        //}

        //// PUT: api/ScanInfoes/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        ///// <summary>
        ///// 修改指定信息
        ///// </summary>
        ///// <param name="id">ID</param>
        ///// <param name="scanInfo">ScanInfo对象实体</param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutScanInfo(int id, ScanInfo scanInfo)
        //{
        //    if (id != scanInfo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(scanInfo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ScanInfoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/ScanInfoes
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        ///// <summary>
        ///// 添加信息
        ///// </summary>
        ///// <param name="scanInfo">ScanInfo对象实体</param>
        ///// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult<ScanInfo>> PostScanInfo(ScanInfo scanInfo)
        //{
        //    _context.Scaninfos.Add(scanInfo);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetScanInfo", new { id = scanInfo.Id }, scanInfo);
        //}

        //// DELETE: api/ScanInfoes/5
        ///// <summary>
        ///// 删除制定信息
        ///// </summary>
        ///// <param name="id">ID</param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        ////[ApiExplorerSettings(IgnoreApi = true)]//在开发文档里隐藏接口
        //public async Task<ActionResult<ScanInfo>> DeleteScanInfo(int id)
        //{
        //    var scanInfo = await _context.Scaninfos.FindAsync(id);
        //    if (scanInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Scaninfos.Remove(scanInfo);
        //    await _context.SaveChangesAsync();

        //    return scanInfo;
        //}

        //private bool ScanInfoExists(int id)
        //{
        //    return _context.Scaninfos.Any(e => e.Id == id);
        //}
        #endregion
        /// <summary>
        /// 获取所有信息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScanInfos>>> GetScaninfos()
        {
            return await scanInfoServices.Query();
        }
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<List<ScanInfos>> GetScanInfo(int id)
        {
            return await scanInfoServices.Query(c => c.Id == id);
            //return await scanInfoServices.QueryByID(id);
        }

        /// <summary>
        /// 修改指定信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="scanInfo">ScanInfo对象实体</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScanInfo(int id, ScanInfos scanInfo)
        {
            if (id != scanInfo.Id)
            {
                return BadRequest();
            }
            try
            {
                await scanInfoServices.Update(scanInfo);
            }
            catch (System.Exception)
            {
                if (!scanInfoServices.Query(q => q.Id == id).IsCompleted)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="scanInfo">ScanInfo对象实体</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ScanInfos>> PostScanInfo(ScanInfos scanInfo)
        {
            await scanInfoServices.Add(scanInfo);
            return CreatedAtAction("GetScanInfo", new { id = scanInfo.Id }, scanInfo);
        }

        /// <summary>
        /// 删除制定信息
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        //[ApiExplorerSettings(IgnoreApi = true)]//在开发文档里隐藏接口
        public async Task<ActionResult<ScanInfos>> DeleteScanInfo(int id)
        {
            var scanInfo = await scanInfoServices.QueryByID(id);
            if (scanInfo == null)
            {
                return NotFound();
            }
            await scanInfoServices.DeleteById(id);

            return scanInfo;
        }
    }
}
