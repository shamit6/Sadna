using Domain;
using LinqKit;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaySimple.QueryProcessors
{
    public interface IFieldsQueryProcessor
    {
        IEnumerable<DTOs.Field> Search(int pageNum, int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate);

        DTOs.Field GetField(int id);

        DTOs.Field Save(DTOs.Field field);

        DTOs.Field Update(int id, DTOs.Field field);
    }
    
    public class FieldsQueryProcessor : DBAccessBase<Field>, IFieldsQueryProcessor
    {
        IDecodesQueryProcessor _decodesQueryProcessor;

        public FieldsQueryProcessor(IDecodesQueryProcessor decodesQueryProcessor, ISession session) : base(session)
        {
            _decodesQueryProcessor = decodesQueryProcessor;
        }

        public IEnumerable<DTOs.Field> Search(int pageNum, int? orderId, int? orderStatusId, int? fieldId, string fieldName, DateTime? startDate, DateTime? endDate)
        {
            var filter = PredicateBuilder.New<Field>(x => true);

            if (fieldId.HasValue)
            {
                filter.And(x => x.Id == fieldId);
            }

            if (!string.IsNullOrEmpty(fieldName))
            {
                filter.And(x => x.Name.Contains(fieldName));
            }

            if (startDate.HasValue || endDate.HasValue)
            {
                // TODO add logic to query orders table
            }

            var queryResult =  Query().Where(filter).Skip(Consts.Paging.PageSize * (pageNum - 1)).Take(Consts.Paging.PageSize).ToList();

            return queryResult.Select(x =>
            {
                return new DTOs.Field().Initialize(x);
            });
        }

        // TODO handle not found
        public DTOs.Field GetField(int id)
        {
            return new DTOs.Field().Initialize(Get(id));
        }

        public DTOs.Field Save(DTOs.Field field)
        {
            Field newField = new Field
            {
                Name = field.Name,
                Size = _decodesQueryProcessor.Get<FieldSizeDecode>(field.Size),
                Type = _decodesQueryProcessor.Get<FieldTypeDecode>(field.Type),
            };

            Field persistedField = Save(newField);

            return new DTOs.Field().Initialize(persistedField);
        }

        public DTOs.Field Update(int id, DTOs.Field field)
        {
            Field existingField = Get(id);

            existingField.Name = field.Name ?? existingField.Name;

            if (field.Size != null)
                existingField.Size =  _decodesQueryProcessor.Get<FieldSizeDecode>(field.Size);

            if (field.Type != null)
                existingField.Type = _decodesQueryProcessor.Get<FieldTypeDecode>(field.Type);

            Update(existingField);

            return new DTOs.Field().Initialize(existingField);
        }
    }
}