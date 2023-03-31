using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Basic_HTTP
{
    public class Flurl
    {
        public static async Task Main()
        {
            var result = "https://jsonplaceholder.typicode.com/posts"
                .GetAsync()
                .ReceiveJson<List<Post>>();

            var selectedPost = result.Result.First(p => p.Id == 30);
            await Console.Out.WriteLineAsync(selectedPost.Title);

            var resultPost = await "https://jsonplaceholder.typicode.com/posts"
                .WithHeaders(new
                {
                    header = "value",
                    header2 = "value2",
                    some_header = "some-value"
                }, true)
                .SetQueryParams(
                new
                {
                    postId = 1,
                    somevalue = "value"
                })
                .PostJsonAsync(selectedPost);

            await Console.Out.WriteLineAsync($"Status code {resultPost.StatusCode}");

            var dsasd = 666;
        }
    }
}
