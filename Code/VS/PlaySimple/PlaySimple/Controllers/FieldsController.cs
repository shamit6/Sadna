using System;
using System.Collections.Generic;
using System.Web.Http;
using PlaySimple.QueryProcessors;

namespace PlaySimple.Controllers
{
    //[Authorize(Roles = Consts.Roles.Customer)]
    public class FieldsController : ApiController
    {
        private readonly IFieldsQueryProcessor _fieldsQueryProcessor;

        public FieldsController(IFieldsQueryProcessor fieldsQueryProcessor)
        {
            _fieldsQueryProcessor = fieldsQueryProcessor;
        }

        public IEnumerable<DTOs.Field> Search(int? pageNum, int? fieldId, string fieldName)
        {
            return _fieldsQueryProcessor.Search(pageNum, fieldId, fieldName);
        }

        public DTOs.Field Get(int id)
        {
            return _fieldsQueryProcessor.GetField(id);
        }

        [HttpPost]
        public DTOs.Field Save([FromBody]DTOs.Field field)
        {
            return _fieldsQueryProcessor.Save(field);
        }

        [HttpPut]
        public DTOs.Field Update([FromUri]int id, [FromBody]DTOs.Field field)
        {
            return _fieldsQueryProcessor.Update(id, field);
        }

        [HttpDelete]
        public void Delete([FromUri]int id)
        {

        }
    }
}
