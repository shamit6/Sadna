using Domain;
using PlaySimple.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NHibernate.Linq;

namespace PlaySimple.Controllers
{
    [Authorize(Roles = Consts.Roles.Employee)]
    public class FieldsController : ApiController
    {
        // GET: api/Fields
        public IEnumerable<DTOs.Field> Search(int pageNum, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate, int? fieldTypeId)
        {
            var session = NhibernateManager.Instance.OpenSession();

            try
            {
                var query = session.QueryOver<Field>();

                if (fieldId.HasValue)
                {
                    query.Where(x => x.Id == fieldId);
                }

                if (!string.IsNullOrEmpty(fieldName))
                {
                    query.Where(x => x.Name.Like(fieldName));
                }

                if (fieldTypeId.HasValue)
                {
                    query.Where(x => x.Type.Id == fieldTypeId);
                }

                if (startDate.HasValue || endDate.HasValue)
                {
                    // add logic to query orders table
                }

                var queryResult =
                    query.Skip(Consts.Paging.PageSize * (pageNum - 1)).Take(Consts.Paging.PageSize).List();

                return queryResult.Select(x =>
                {
                    return new DTOs.Field().Initialize(x);
                });
            }
            finally
            {
                NhibernateManager.Instance.CloseSession();
            }
        }

        // GET: api/Fields/5
        public DTOs.Field Get(int id)
        {
            var session = NhibernateManager.Instance.OpenSession();

            try
            {
                var field = session.Get<Field>(id);
                return new DTOs.Field().Initialize(field);
            }
            finally
            {
                NhibernateManager.Instance.CloseSession();
            }
        }

        // POST: api/Fields
        public DTOs.Field Save(DTOs.Field field)
        {
            // convert dto to domain
            // save
            // convert saved entity to dto again
            // return new dto

            return null;
        }

        // PUT: api/Fields/5
        public DTOs.Field Update(int id, [FromBody]string value)
        {
            // get domain object
            // merge with dto
            // save
            // convert saved entity to dto again
            // return new dto

            return null;
        }

        // DELETE: api/Fields/5
        public void Delete(int id)
        {
            var session = NhibernateManager.Instance.OpenSession();

            try
            {
                var field = session.Get<Field>(id);
                session.Delete(field);
            }
            finally
            {
                NhibernateManager.Instance.CloseSession();
            }
        }
    }
}
