using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace ConsoleApp_Basic_HTTP
{
    internal class Program
    {
        static async Task OldMain(string[] args)
        {
            Console.WriteLine("Hello, Basic Demo of String!");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                var content = await response.Content.ReadAsStringAsync();
                
                //var posts = JsonSerializer.Deserialize<List<Post>>(content);
                var posts = JsonConvert.DeserializeObject<List<Post>>(content);

                var post = posts.FirstOrDefault(p => p.Id ==14);

                Console.WriteLine(post.Body);

                post.Title = "Demo Title";
                post.Body = "Demo body";

                var postJsonContent = new StringContent(JsonConvert.SerializeObject(posts));    

                httpClient.PostAsync("https://jsonplaceholder.typicode.com/posts",postJsonContent);

                using(var postRequestMessage = 
                    new HttpRequestMessage(HttpMethod.Post, "https://jsonplaceholder.typicode.com/posts"))
                {
                    postRequestMessage.Headers.Add("content-type", "application/json");
                    postRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(post));

                    var post2Result = httpClient.SendAsync(postRequestMessage);
                }

                var queryParams = HttpUtility.ParseQueryString(string.Empty);
                queryParams["postId"] = "1";
            }
        }
    }
}