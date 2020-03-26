using System;
using System.ComponentModel.DataAnnotations;

namespace Firefish.Application.Models.Annotations
{
    public class SqlDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            DateTime date = Convert.ToDateTime(value);
            return date > (new DateTime(1900, 1, 1));
        }
    }
}
