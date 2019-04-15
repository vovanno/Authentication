using System;

namespace ApplicationDAL.Exceptions
{
    public class WrongPageException: Exception
    {
        public int MaxPage;
        public WrongPageException(){ }

        public WrongPageException(string message, int maxPage) : base(message)
        {
            MaxPage = maxPage;
        }
        public WrongPageException(string message,Exception inner) : base(message, inner) { }
        public WrongPageException(System.Runtime.Serialization.SerializationInfo si,
            System.Runtime.Serialization.StreamingContext sc) : base(si, sc) { }
    }
}
