using NHibernate;
using PlaySimple.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlaySimple.Validators
{
    public class ExistsInDBAttribute : ValidationAttribute
    {
        private Type _type;

        public ExistsInDBAttribute(Type type)
        {
            _type = type;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (!_type.IsSubclassOf(typeof(Domain.Entity)))
                throw new ValidationException("type must inherit from Domain.Entity");

            ISession session = null;// NhibernateManager.Instance.OpenSession();
            try
            {
                var entity = session.Get(_type, ((Domain.Entity)value).Id);
                if (entity == null)
                {
                    return new ValidationResult("Entity doesn't exist");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            finally
            {
            }
        }
    }
}