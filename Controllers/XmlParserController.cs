using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Linq;
using XmlParser.Models;
using Microsoft.Extensions.Configuration;

namespace XmlParser.Controllers
{
    public class XmlParserController : Controller
    {
        private readonly IConfiguration _configuration;
        public XmlParserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {
            string xmlUrl = _configuration.GetValue<string>("ApiSettings:XmlApiUrl"); ; // Replace with your URL
            var results = await GetXmlData(xmlUrl);

            return View(results); // Pass the results to the view
        }

        private async Task<List<XmlElementModel>> GetXmlData(string xmlUrl)
        {
            try
            {
                using HttpClient client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(30);

                // Fetch the XML content from the URL
                string xmlContent = await client.GetStringAsync(xmlUrl);

                // Load the XML content
                var doc = XDocument.Parse(xmlContent);

                // Parse XML for elements with "name" or "value" attributes
                var results = doc.Descendants()
                    .Where(e => e.Attribute("name") != null || e.Attribute("value") != null)
                    .Select(element =>
                    {
                        string elementValue = element.Attribute("name")?.Value ?? element.Attribute("value")?.Value;
                        string documentation = element.Descendants("{http://www.w3.org/2001/XMLSchema}documentation")
                                                      .FirstOrDefault()?.Value?.Trim() ?? "No description";
                        documentation = documentation.Replace("\n", " ").Replace("\r", " ").Trim();
                        return new XmlElementModel { Element = elementValue, Description = documentation };
                    })
                    .Where(item => item.Description != "No description")
                    .ToList();

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching or parsing XML: " + ex.Message);
                return new List<XmlElementModel>(); // Return an empty list if there's an error
            }
        }
    }
}
