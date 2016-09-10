﻿using PlaySimple.Filters;
using PlaySimple.QueryProcessors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlaySimple.Controllers
{
    public class ComplaintsController : ApiController
    {
        private readonly IComplaintsQueryProcessor _complaintsQueryProcessor;

        public ComplaintsController(IComplaintsQueryProcessor ComplaintsQueryProcessor)
        {
            _complaintsQueryProcessor = ComplaintsQueryProcessor;
        }

        [HttpGet]
        [Route("api/complaints/search")]
        public List<DTOs.Complaint> Search(int? customerId = null)
        {
            return _complaintsQueryProcessor.Search(customerId, null, null, null).OrderByDescending(x => x.Date).ToList();
        }

        [HttpGet]
        public DTOs.Complaint Get(int id)
        {
            return _complaintsQueryProcessor.GetComplaint(id);
        }

        [HttpPost]
        [TransactionFilter]
        public DTOs.Complaint Save([FromBody]DTOs.Complaint Complaint)
        {
            return _complaintsQueryProcessor.Save(Complaint);
        }

        //[HttpPut]
        //[TransactionFilter]
        //public DTOs.Complaint Update([FromUri]int id, [FromBody]DTOs.Complaint Complaint)
        //{
        //    return _complaintsQueryProcessor.Update(id, Complaint);
        //}
    }
}