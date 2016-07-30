using Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IFieldsQueryProcessor
    {
        IEnumerable<DTOs.Field> Search(int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate);

        DTOs.Field Get(int id);

        DTOs.Field SaveOrUpdate(DTOs.Field field);
    }
    
    public class FieldsQueryProcessor : DBAccessBase<Order>, IFieldsQueryProcessor
    {
        public FieldsQueryProcessor(ISession session) : base(session)
        {

        }

        public IEnumerable<DTOs.Field> Search(int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            return null;
        }

        public DTOs.Field Get(int id) { return null; }

        public DTOs.Field SaveOrUpdate(DTOs.Field field) { return null; }
    }
}