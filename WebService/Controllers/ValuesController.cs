using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Data;
using System.Data.SqlClient;
using WebService.Libs;
using WebService.Models;
namespace WebService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public HttpResponseMessage Get(string mtf)
        {
            string stringConnect = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt;
            try
            {
                using (SqlConnection conn = new SqlConnection(stringConnect))
                {

                    using (SqlCommand cmd = new SqlCommand("getInternalTransfer", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@MTF", SqlDbType.NVarChar).Value = mtf;
                        conn.Open();
                        dt = Common.ExecuteNonQuery(cmd);
                    }
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    string otp = dt.Rows[0]["otp"].ToString();
                    string p_status = dt.Rows[0]["p_status"].ToString();
                    string message = dt.Rows[0]["Messagess"].ToString();

                    int status;
                    Int32.TryParse(p_status, out status);
                    if (status == 1)
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(true, message, null));
                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        return response;
                    }
                    else
                    {
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, message, null));
                        var response = Request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                        return response;
                    }

                }
                else
                {
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, "Gửi OTP thất bại, vui lòng thử lại sau!", null));
                    var response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    return response;
                }

            }
            catch (Exception ex)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(new InternalTransferReturnJson(false, "Gửi OTP thất bại, vui lòng thử lại sau!",null));
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                return response;
              
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}