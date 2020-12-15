using System.Collections.Generic;
using System.Linq;
namespace bobo
{
    public class ServiceResponse
    {
        private List<string> messages;
        public string Message => string.Join("\n", this.messages);
        public bool Success { get; set; }

        public List<string> Messages => messages;

        public void AddMessage(string message)
        {
            messages.Add(message);
        }

        public void Merge(ServiceResponse response)
        {
            this.Success = this.Success && response.Success;
            this.messages.AddRange(response.Messages);
            this.messages.Distinct();
        }
        public ServiceResponse()
        {
            this.messages = new List<string>();
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
}