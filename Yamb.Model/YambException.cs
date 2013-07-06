using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Model
{
    public class YambException : Exception
    {
        protected string _msg;
        public YambException(string msg)
        {
            _msg = msg;
        }

        public string GetMessage()
        {
            return _msg;
        }
    }

    public class FieldIsFilledException : YambException
    {
        public FieldIsFilledException(string msg)
            : base(msg)
        {

        }
    }

    public class MissingParametarsException : YambException
    {
        public MissingParametarsException(string msg)
            : base(msg)
        {

        }
    }

    public class InaccessibleFieldException : YambException
    {
        public InaccessibleFieldException(string msg)
            : base(msg)
        {

        }
    }

    public class FieldIsEmptyException : YambException
    {
        public FieldIsEmptyException(string msg)
            : base(msg)
        {

        }
    }

    public class NonExistingFieldException : YambException
    {
        public NonExistingFieldException(string msg)
            : base(msg)
        {

        }
    }
}
