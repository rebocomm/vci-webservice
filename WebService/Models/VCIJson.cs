using System;
using System.Data;
using System.Data.SqlClient;
namespace WebService.Models
{
    public class VCIJson
    {
        public DataTable InternalT { get; set; }
        public DataTable TransferList { get; set; }
        public VCIJson()
        {
        }
        public VCIJson(DataTable InternalT, DataTable TransferList)
        {
            this.InternalT = InternalT;
            this.TransferList= TransferList;
        }



    }
}
