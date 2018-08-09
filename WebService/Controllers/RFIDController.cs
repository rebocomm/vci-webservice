using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using WebService.Libs;
using WebService.Models;
using Newtonsoft.Json;
using System.Xml;

namespace WebService.Controllers
{
    public class RFIDController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage getInternalTransfer([FromBody] InternalTransferInputJson value)
        {
            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getInternalTransfer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = value.Type;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);

                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
        [HttpPost]
        public HttpResponseMessage updateInternalTransfer([FromBody] InternalTransferInputJson value)
        {
            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("updateInternalTransfer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                        cmd.Parameters.Add("@EPCQuantity", SqlDbType.Int).Value = value.EPCQuantity;
                        cmd.Parameters.Add("@FromBin", SqlDbType.VarChar).Value = value.FromBin;
                        cmd.Parameters.Add("@ToBin", SqlDbType.VarChar).Value = value.ToBin;
                        cmd.Parameters.Add("@Type", SqlDbType.Int).Value = value.Type;
                        cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = value.ItemCode;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);

                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
        //[transferDetails]

        [HttpPost]
        public HttpResponseMessage updateTransferDetails([FromBody] TransferDetailsInputJson value)
        {
            var result = JsonConvert.DeserializeObject<List<TransferDetails>>(value.JsonData);

           

            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {
                    foreach (TransferDetails item in result)
                    {
                        using (SqlCommand cmd = new SqlCommand("insertOrUpdateTransferDetails", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                            cmd.Parameters.Add("@EPC", SqlDbType.VarChar).Value = item.EPC;
                            cmd.Parameters.Add("@FromBin", SqlDbType.VarChar).Value = item.FromBin;
                            cmd.Parameters.Add("@ToBin", SqlDbType.VarChar).Value = item.ToBin;
                            cmd.Parameters.Add("@UOM", SqlDbType.VarChar).Value = item.UOM;
                            cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = item.BatchNo;
                            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = item.Type;
                            cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = item.ItemCode;

                            //var json11 = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                            //Common.WriteLog("Request", json11);
                            conn.Open();
                            dt = Common.ExecuteNonQuery(cmd);
                            conn.Close();
                        }
                    }

                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", null));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }

        [HttpPost]
        public HttpResponseMessage getComposeGoods([FromBody] ComposeGoodsInputJson value)
        {
            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getComposeGoods", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Key", SqlDbType.VarChar).Value = value.MTF;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                        conn.Close();

                    }
                    if (dt == null || dt.Rows.Count <= 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("getInternalTransfer", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = 4;
                            conn.Open();
                            dt = Common.ExecuteNonQuery(cmd);
                        }
                    }

                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }

        [HttpPost]
        public HttpResponseMessage getRFIDByNo([FromBody] RFIDByNoInputJson value)
        {

            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getRFIDByNo", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@No", SqlDbType.VarChar).Value = value.No;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                        conn.Close();
                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
        [HttpPost]
        public HttpResponseMessage getAllRFIDMapping()
        {

            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getAllRFIDMapping", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                        conn.Close();
                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
        [HttpPost]
        public HttpResponseMessage getLastMTFInternalTransfer()
        {

            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getLastMTFInternalTransfer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                        conn.Close();
                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
        [HttpPost]
        public HttpResponseMessage insertNewInternalTransfer([FromBody] NewInternalTransferInputJson value)
        {

            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("insertNewInternalTransfer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                        cmd.Parameters.Add("@FromBin", SqlDbType.VarChar).Value = value.FromBin;
                        cmd.Parameters.Add("@ToBin", SqlDbType.VarChar).Value = value.ToBin;
                        cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = value.Quantity;
                        cmd.Parameters.Add("@EPCQuantity", SqlDbType.Int).Value = value.EPCQuantity;
                        cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = value.ItemCode;
                        cmd.Parameters.Add("@TransferType", SqlDbType.VarChar).Value = value.TransferType;
                        cmd.Parameters.Add("@OriginalDoc", SqlDbType.VarChar).Value = value.OriginalDoc;

                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                        conn.Close();
                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, "Thành công", dt));
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
        string Baseurl = "http://vietceramics.com:81/";
        [HttpPost]
        public async  Task<HttpResponseMessage> pushVCI([FromBody] VCIInputJson value)
        {
            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            DataTable dt1;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getInternalTransferPush", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                        conn.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("getTransferDetails", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                        conn.Open();
                        dt1 = Common.ExecuteNonQuery(cmd);
                        conn.Close();
                    }

                    using (var client = new HttpClient())
                    {
                        //Passing service base url  
                        client.BaseAddress = new Uri(Baseurl);

                        client.DefaultRequestHeaders.Clear();
                        //Define request data format  
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var ass = new VCIJson(dt, dt1);

                        var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(ass);
                        HttpResponseMessage response = await client.PostAsJsonAsync(
                            "/RTLS/Api/UpdateInventory", ass);
                        //Checking the response is successful or not which is sent using HttpClient  
                       
                        if (response.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var EmpResponse = response.Content.ReadAsStringAsync().Result;
                            Common.WriteLog("Request", "Response["+EmpResponse+ "]\r\n \r\n" + json1);
                            return Request.CreateResponse(HttpStatusCode.OK, EmpResponse);

                        }else{
                            Common.WriteLog("Request", "Response[" + response.IsSuccessStatusCode + "]\r\n" + json1);
                            return Request.CreateResponse(HttpStatusCode.OK, "NO");
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Common.WriteLog("LogError", "[pushVCI] " + ex.ToString());
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }

        }

        [HttpPost]
        public string physicalCountData([FromBody] TransferDetailsInputJson value1)
        {
            try
            {
                string value = value1.JsonData;
                //string  value = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><ale:ECReports xmlns:ale=\"urn:epcglobal:ale:xsd:1\" xmlns:epcglobal=\"urn:epcglobal:xsd:1\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:ale_mojix_ext=\"urn:mojix:ale:xsd:1\" ale_mojix_ext:longitude=\"-118.47\" ale_mojix_ext:elevation=\"41.15\" ale_mojix_ext:latitude=\"34.03\" ale_mojix_ext:declination=\"0.0\" ale_mojix_ext:units=\"meters\" date=\"2013-01-31T22:02:26.154+00:00\" specName=\"_mojix_ECSpec_loc\" schemaVersion=\"1.1\" terminationTrigger=\" urn:com.mojix.epc.triggers:duration_elapsed:_mojix_ECSpec_loc\" creationDate=\"2013-01-31T22:02:26.154+00:00\" totalMilliseconds=\"1000\" terminationCondition=\"TRIGGER\" initiationCondition=\"REQUESTED\" ALEID=\"D4BED9A84EE0\" ><!-- Version ID : 002 --><!-- xsi:schemaLocation=\"http://localhost:8090/services/ALEService/?xsd=EPCglobal-ale-1_1-ale.xsd\"--><reports><report reportName=\"zone report\"><group><groupList><member ts=\"2013-01-31T22:02:26.136Z\" ale_mojix_ext:logicalReader=\"RTLS\" ale_mojix_ext:posX=\"77.72\" ale_mojix_ext:posY=\"13.72\" ale_mojix_ext:posZ=\"0.00\" ale_mojix_ext:eNode=\"83347\" ><rawHex>urn:epc:raw:96.x10C000000000000000000000</rawHex></member><member ts=\"2013-01-31T22:02:26.136Z\" ale_mojix_ext:logicalReader=\"RTLS\" ale_mojix_ext:posX=\"77.73\" ale_mojix_ext:posY=\"13.72\" ale_mojix_ext:posZ=\"0.00\" ale_mojix_ext:eNode=\"83347\" ><rawHex>urn:epc:raw:96.x10C000000000000000000001</rawHex></member></groupList><groupCount><count>2</count></groupCount></group></report></reports></ale:ECReports>";
                string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlDBHelper sql = new SqlDBHelper();

                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    var doc = new XmlDocument();
                    doc.LoadXml(value);
                    var countNodes = doc.GetElementsByTagName("count");
                    XmlElement xmlElementCount = (XmlElement)countNodes[0];

                    //tong so luong tag doc duoc
                    var count = xmlElementCount.InnerText;

                    //chi tiet cac epc
                    var bookNodes = doc.GetElementsByTagName("member");
                    foreach (var bookNode in bookNodes)
                    {
                        XmlElement xmlElement = (XmlElement)bookNode;
                        string x = xmlElement.Attributes["ale_mojix_ext:posX"].Value;
                        string y = xmlElement.Attributes["ale_mojix_ext:posY"].Value;
                        string rawHex = xmlElement.InnerText;
                        int index = rawHex.LastIndexOf('.');
                        if (index != -1)
                        {
                            Console.WriteLine("Index " + index);
                            rawHex = rawHex.Substring(index+2);
                        }
                        Console.WriteLine(rawHex + " " + x + " " + y);
                        using (SqlCommand cmd = new SqlCommand("insertOrUpdatePhysicalCountData", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@EPC", SqlDbType.VarChar).Value = rawHex;
                            cmd.Parameters.Add("@posX", SqlDbType.VarChar).Value = x;
                            cmd.Parameters.Add("@posY", SqlDbType.VarChar).Value = y;
                            cmd.Parameters.Add("@InventoryCount", SqlDbType.Int).Value = count;
                            conn.Open();
                            Common.ExecuteNonQuery(cmd);
                            conn.Close();
                        }
                    }


                    return "OK ";
                }
               
            }
            catch (Exception ex)
            { 
                return ex.Message;
            }
           
        }

        [HttpPost]
        public HttpResponseMessage updateInternalTransferByHH([FromBody] InternalTransferInputByHHJson value)
        {
            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlDBHelper sql = new SqlDBHelper();
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("updateInternalTransferByHH", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.VarChar).Value = value.MTF;
                        cmd.Parameters.Add("@UOM", SqlDbType.VarChar).Value = value.UOM;
                        cmd.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = value.BatchNo;
                        cmd.Parameters.Add("@ItemCode", SqlDbType.VarChar).Value = value.ItemCode;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);

                    }
                    var json1 = Newtonsoft.Json.JsonConvert.SerializeObject( "Thành công");
                    var response1 = Request.CreateResponse(HttpStatusCode.OK);
                    response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                    return response1;

                }
            }
            catch (Exception ex)
            {
                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, ex.Message, null));
                var response1 = Request.CreateResponse(HttpStatusCode.OK);
                response1.Content = new StringContent(json1, System.Text.Encoding.UTF8, "application/json");
                return response1;
            }
        }
    }
}
