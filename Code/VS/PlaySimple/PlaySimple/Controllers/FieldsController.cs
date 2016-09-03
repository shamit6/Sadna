using System;
using System.Collections.Generic;
using System.Web.Http;
using PlaySimple.QueryProcessors;

namespace PlaySimple.Controllers
{
    [Authorize(Roles = Consts.Roles.Customer)]
    public class FieldsController : ApiController
    {
        private readonly IFieldsQueryProcessor _fieldsQueryProcessor;

        public FieldsController(IFieldsQueryProcessor fieldsQueryProcessor)
        {
            _fieldsQueryProcessor = fieldsQueryProcessor;
        }

        public IEnumerable<DTOs.Field> Search(int pageNum, int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            return _fieldsQueryProcessor.Search(pageNum, orderId, orderStatusId, fieldId, fieldName, startDate, endDate);
        }

        public DTOs.Field Get(int id)
        {
            return _fieldsQueryProcessor.GetField(id);
        }

        public DTOs.Field Save(DTOs.Field field)
        {
            return _fieldsQueryProcessor.Save(field);
        }

        public DTOs.Field Update(int id, DTOs.Field field)
        {
            return _fieldsQueryProcessor.Update(id, field);
        }
    }
}
