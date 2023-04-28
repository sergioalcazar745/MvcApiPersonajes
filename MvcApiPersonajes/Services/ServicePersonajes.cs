using MvcApiPersonajes.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiPersonajes.Services
{
    public class ServicePersonajes
    {
        private MediaTypeWithQualityHeaderValue Headers;
        private string UrlApi;

        public ServicePersonajes(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Headers = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Headers);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<T>();
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            return await CallApiAsync<List<Personaje>>("/api/Personajes");
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            return await CallApiAsync<Personaje>("/api/Personajes/" + id);
        }

        public async Task<HttpStatusCode> InsertPersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/InsertPersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Headers);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
                else
                {
                    return 0;
                }
            }
        }

        public async Task<HttpStatusCode> UpdatePersonajeAsync(Personaje personaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/UpdatePersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Headers);

                string json = JsonConvert.SerializeObject(personaje);

                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
                else
                {
                    return 0;
                }
            }
        }

        public async Task<HttpStatusCode> DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/DeletePersonaje/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage response = await client.DeleteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
