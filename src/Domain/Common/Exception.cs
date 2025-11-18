
namespace Domain.Common
{

    public class BussinessException : Exception
    {
        public BussinessException(string message, string v) : base(message) { }
    }

}