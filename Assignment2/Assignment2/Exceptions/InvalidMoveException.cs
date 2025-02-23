using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Exceptions
{
    public class InvalidMoveException : Exception, ISerializable
    {
        public InvalidMoveException() { }

        public InvalidMoveException(string message) : base(message) { }
    }
}