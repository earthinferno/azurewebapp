using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using techtasticwelcome.Models;

namespace techtasticwelcome.Services
{
    public class CourseStore
    {
        private DocumentClient client;
        private Uri coursesLink;
        public CourseStore()
        {
            var uri = new Uri("https://techtasticdb.documents.azure.com:443/");
            var key = "cqQVpHAXHJmEB3hbbtSAlCvSRVLESYPIZY4gd2bOCLjytTbSne0TFZljYm5WhRoia7yXlF1FKOWmTSFuVAJjYg==";
            client = new DocumentClient(uri, key);
            coursesLink = UriFactory.CreateDocumentCollectionUri("welcometotechtastic", "ttwelcome");
        }
        public async  Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                await client.CreateDocumentAsync(coursesLink, course);
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = client.CreateDocumentQuery<Course>(coursesLink).OrderBy(c => c.Title);

            return courses;
        }


    }

}
