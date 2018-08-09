using System;
using System.Data;
using System.Data.SqlClient;
namespace WebService.Models
{
    public class InternalTransfer
    {
        public InternalTransfer()
        {
        }
    }
    public class InternalTransferInputJson
    {
        public string EPCQuantity { get; set; }
        public string FromBin { get; set; }
        public string ToBin { get; set; }
        public int Type { get; set; }
        public string ItemCode { get; set; }
        public string MTF { get; set; }
    }

    public class InternalTransferReturnJson
    {
        public InternalTransferReturnJson() { }
        public InternalTransferReturnJson(bool Success, string Message, DataTable Data)
        {
            this.Success = Success;
            this.Message = Message;
            this.Data = Data;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public DataTable Data { get; set; }

    }
}
