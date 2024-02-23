using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Service.Constants
{
    public class ResponseMessages
    {
        public static string GetEmailSuccessMessage(string emailAddress) => $"Email sent successfully to {emailAddress}";
    }
}
