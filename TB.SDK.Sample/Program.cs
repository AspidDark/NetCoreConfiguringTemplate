using System;
using System.Threading.Tasks;
using Refit;
using TB.Contracts.V1.Requests;
using TB.SDK;

namespace TB.SDK.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.ReadKey();//wait for server
            var cachedToken = string.Empty;

            var identityApi = RestService.For<IIdentityApi>("https://localhost:5001");
            var tweetbookApi = RestService.For<ITBApi>("https://localhost:5001", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var registerResponse = await identityApi.RegisterAsync(new UserRegistrationRequest
            {
                Email = "sdkaccount@gmail.com",
                Password = "Test1234!"
            });

            var loginResponse = await identityApi.LoginAsync(new UserLoginRequest
            {
                Email = "sdkaccount@gmail.com",
                Password = "Test1234!"
            });

            cachedToken = loginResponse.Content.Token;

            var allPosts = await tweetbookApi.GetAllAsync();

            var createdPost = await tweetbookApi.CreateAsync(new CreatePostRequest
            {
                Name = "This is created by the SDK",
                Tags = new[] { "sdk" }
            });

            var retrievedPost = await tweetbookApi.GetAsync(createdPost.Content.Id);

            var updatedPost = await tweetbookApi.UpdateAsync(createdPost.Content.Id, new UpdatePostRequest
            {
                Name = "This is updated by the SDK"
            });

             var deletePost = await tweetbookApi.DeleteAsync(createdPost.Content.Id);
        }
    }
}
