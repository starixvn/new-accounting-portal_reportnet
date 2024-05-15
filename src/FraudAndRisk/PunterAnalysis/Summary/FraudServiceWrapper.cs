using Newtonsoft.Json;
using Olympus.FPSAPI.Client;
using Olympus.FPSAPI.Client.Model;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace PunterAnalysis.Summary
{
    public interface FraudServiceWrapperNew
    {
        Task<List<CustomerDevice>> GetAssociatedCustomers(string host, int custId);

        Task<string> GetLatestIP(string host, int custId);

        Task<string> GetCategory(string host, string custId);

        Task<List<CustomerByUsername>> GetAssociatedCustomerByDeviceOrManual(string host, int custId, int associationType);

        Task<List<CustomerById>> GetAssociatedCustomersByIp(string host, int custId);
    }

    public class FraudServiceWrapperNewImp : FraudServiceWrapperNew
    {
        private const int AssociatedByIP = 3;

        private static HttpClient jsonClient = new HttpClient();

        private static HttpClient xmlClient = new HttpClient();

        public async Task<List<CustomerDevice>> GetAssociatedCustomers(string host, int custId)
        {
            jsonClient.DefaultRequestHeaders.Accept.Clear();
            jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = $"{host}/associatedcustomers/{custId}";
            HttpResponseMessage response = await jsonClient.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
            return (!response.IsSuccessStatusCode) ? new List<CustomerDevice>() : (await response.Content.ReadAsAsync<List<CustomerDevice>>().ConfigureAwait(continueOnCapturedContext: false));
        }

        public async Task<string> GetLatestIP(string host, int custId)
        {
            jsonClient.DefaultRequestHeaders.Accept.Clear();
            jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = $"{host}/Get10LatestIps/{custId}";
            HttpResponseMessage response = await jsonClient.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            return (await response.Content.ReadAsAsync<List<string>>().ConfigureAwait(continueOnCapturedContext: false)).FirstOrDefault((string ip) => ip != "127.0.0.1");
        }

        public async Task<string> GetCategory(string host, string custId)
        {
            List<string> result = new List<string>();
            xmlClient.DefaultRequestHeaders.Accept.Clear();
            xmlClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
            string path = $"{host}/GetCustomerClassification/?custIdList={custId}";
            HttpResponseMessage response = await xmlClient.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
            if (!response.IsSuccessStatusCode)
            {
                return string.Empty;
            }

            var responseContent = await response.Content.ReadAsAsync<string>();
            XDocument xdoc = XDocument.Parse(await response.Content.ReadAsAsync<string>());
            IEnumerable<XElement> elements = xdoc.Descendants("Root").Descendants("CustomerClassification");
            if (elements.Any())
            {
                foreach (XElement element in elements)
                {
                    result.Add(element.Attribute("CategoryName").Value);
                }
            }

            return string.Join(", ", result);
        }

        public async Task<List<CustomerByUsername>> GetAssociatedCustomerByDeviceOrManual(string host, int custId, int associationType)
        {
            jsonClient.DefaultRequestHeaders.Accept.Clear();
            jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = $"{host}/cts/GetAssociationPerType?custId={custId}&associationType={associationType}";
            HttpResponseMessage response = await jsonClient.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
            string text = ((!response.IsSuccessStatusCode) ? string.Empty : (await response.Content.ReadAsAsync<string>().ConfigureAwait(continueOnCapturedContext: false)));
            string responseData = text;
            return JsonConvert.DeserializeObject<List<CustomerByUsername>>(responseData);
        }

        public async Task<List<CustomerById>> GetAssociatedCustomersByIp(string host, int custId)
        {
            jsonClient.DefaultRequestHeaders.Accept.Clear();
            jsonClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string path = $"{host}/cts/GetAssociationPerType?custId={custId}&associationType={3}";
            HttpResponseMessage response = await jsonClient.GetAsync(path).ConfigureAwait(continueOnCapturedContext: false);
            string text = ((!response.IsSuccessStatusCode) ? string.Empty : (await response.Content.ReadAsAsync<string>().ConfigureAwait(continueOnCapturedContext: false)));
            string responseData = text;
            return JsonConvert.DeserializeObject<List<CustomerById>>(responseData);
        }
    }
}
