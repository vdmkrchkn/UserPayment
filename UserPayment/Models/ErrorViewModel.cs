using System;

namespace UserPayment.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId()
        { 
            return !string.IsNullOrEmpty(RequestId);
        }
    }
}