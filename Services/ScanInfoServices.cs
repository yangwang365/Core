using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using IServices;
using IRepository;
using Model;
using Repository;
using Services.BASE;

namespace Services
{
    public class ScanInfoServices : BaseServices<ScanInfos>, IScanInfoServices
    {
        //IScanInfoRepository _dal;
        //public ScanInfoServices(IScanInfoRepository dal)
        //{
        //    this._dal = dal;
        //    base.baseDal = dal;
        //}
    }
}
