using System;
namespace WebService.Models
{
    public class NewInternalTransfer
    {
        public NewInternalTransfer()
        {
        }
    }
    public class NewInternalTransferInputJson
    {
        public string Quantity { get; set; }
        public string EPCQuantity { get; set; }
        public string FromBin { get; set; }
        public string ToBin { get; set; }
        public string MTF { get; set; }
        public string ItemCode { get; set; }
        public string TransferType { get; set; }
        public string OriginalDoc { get; set; }

    }
}
