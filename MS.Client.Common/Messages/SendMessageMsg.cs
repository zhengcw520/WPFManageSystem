using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Client.Common.Messages
{
    public class SendMessageMsg
    {
        public string? SendMessage { get; set; }
    }

    public class LoadingPayload
    {
        public bool IsShow { get; set; }
        public string Message { get; set; }
    }
    public class LoadingEvent : PubSubEvent<LoadingPayload> { }

}
