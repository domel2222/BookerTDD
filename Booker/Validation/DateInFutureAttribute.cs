using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booker.Validation
{
    public class DateInFutureAttribute : ValidationAttribute
    {
        private Func<DateTime> _dateTimeProvider;

        public DateInFutureAttribute(Func<DateTime> dateTimeNow)
        {
            this._dateTimeProvider = dateTimeNow;
        }

        public override bool IsValid(object value)
        {
            bool isValid = false;

            if (value is DateTime dateTime)
            {
                //bool isValid = true ? dateTime > _dateTimeProvider() : false;
                isValid = dateTime > _dateTimeProvider();
            }

            return isValid;
        }
    }
}
