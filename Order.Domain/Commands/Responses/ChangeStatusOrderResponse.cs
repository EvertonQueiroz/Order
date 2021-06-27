using System.Collections.Generic;
using System.Linq;

namespace Order.Domain.Commands.Responses
{
    public class ChangeStatusOrderResponse
    {
        public string Number { get; set; }

        private readonly IList<string> _status;
        public IReadOnlyCollection<string> Status => _status.ToList();

        public ChangeStatusOrderResponse(string number)
        {
            Number = number;
            _status = new List<string>();
        }

        public void AddStatus(string status)
        {
            _status.Add(status);
        }

        public void AddStatus(IEnumerable<string> status)
        {
            foreach (var item in status)
            {
                _status.Add(item);
            }            
        }
    }
}
