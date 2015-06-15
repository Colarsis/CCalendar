using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColarsisUserControls
{
    class TimeIntervalException : System.ApplicationException
    {

        public TimeIntervalException() {}
        public TimeIntervalException(string message) {}
        public TimeIntervalException(string message, System.Exception inner) {}

        protected TimeIntervalException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) {}

    }
}
