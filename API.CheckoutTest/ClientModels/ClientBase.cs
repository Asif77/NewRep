using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.CheckoutTest.ClientModels
{
    public abstract class ClientBase
    {

        private const string BaseUri = "http://localhost:50000";

        protected async Task<T> HttpPost<T>(string uri, RequestBase request) where T : Response, new()
        {
            T response = new T();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUri);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var reqJson = JsonConvert.SerializeObject(request);
                var apiResponse = await client.PostAsync(
                                uri,
                                new StringContent(reqJson, Encoding.UTF8, "application/json"));


                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    var resultJson = await apiResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject(resultJson, typeof(T)) as T;
                }
                else
                {
                    response.SetFailureResult(apiResponse.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                response.SetFailureResult(exceptionMessage);
            }
            finally
            {
                response.EndTime = DateTime.UtcNow;
            }

            return response;
        }

        protected async Task<T> HttpPut<T>(string uri, RequestBase request) where T : Response, new()
        {
            T response = new T();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUri);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var reqJson = JsonConvert.SerializeObject(request);
                var apiResponse = await client.PutAsync(
                                uri,
                                new StringContent(reqJson, Encoding.UTF8, "application/json"));


                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    var resultJson = await apiResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject(resultJson, typeof(T)) as T;
                }
                else
                {
                    response.SetFailureResult(apiResponse.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                response.SetFailureResult(exceptionMessage);
            }
            finally
            {
                response.EndTime = DateTime.UtcNow;
            }

            return response;
        }

        protected async Task<T> HttpGet<T>(string uri, string data) where T : Response, new()
        {
            T response = new T();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUri);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiResponse = await client.GetAsync(
                                uri + "?" + data);


                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    var resultJson = await apiResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject(resultJson, typeof(T)) as T;
                }
                else
                {
                    response.SetFailureResult(apiResponse.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                response.SetFailureResult(exceptionMessage);
            }
            finally
            {
                response.EndTime = DateTime.UtcNow;
            }

            return response;
        }

        protected async Task<T> HttpDelete<T>(string uri, string data) where T : Response, new()
        {
            T response = new T();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(BaseUri);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var apiResponse = await client.DeleteAsync(
                                uri + "?" + data);

                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    var resultJson = await apiResponse.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject(resultJson, typeof(T)) as T;
                }
                else
                {
                    response.SetFailureResult(apiResponse.ReasonPhrase);
                }
            }
            catch (Exception exp)
            {
                string exceptionMessage = string.Format("Message: {0}, Trace: {1}, InnerException: {2}", exp.Message, exp.StackTrace, exp.InnerException == null ? "" : exp.InnerException.Message);
                response.SetFailureResult(exceptionMessage);
            }
            finally
            {
                response.EndTime = DateTime.UtcNow;
            }

            return response;
        }
    }
}
