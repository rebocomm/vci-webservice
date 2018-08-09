using System;
using System.Data;
using System.Data.SqlClient;
namespace WebService.Models
{
    public class TransferDetails
    {
        public string EPC { get; set; }
        public string FromBin { get; set; }
        public string ToBin { get; set; }
        public string MTF { get; set; }
        public string UOM { get; set; }
        public string BatchNo { get; set; }
        public string Type { get; set; }
        public string ItemCode { get; set; }

        public TransferDetails()
        {
        }
        public TransferDetails(string EPC, string FromBin, string ToBin, string MTF, string UOM,string BatchNo,string Type,string ItemCode)
        {
            this.EPC = EPC;
            this.FromBin = FromBin;
            this.ToBin = ToBin;
            this.MTF = MTF;
            this.UOM = UOM;
            this.BatchNo = BatchNo;
            this.Type = Type;
            this.ItemCode = ItemCode;
        }
    }
    public class TransferDetailsInputJson
    {
        public string JsonData { get; set; }
        public string MTF { get; set; }
    }

    public class TransferDetailsReturnJson
    {
        public TransferDetailsReturnJson() { }
        public TransferDetailsReturnJson(bool Success, string Message, DataTable Data)
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
