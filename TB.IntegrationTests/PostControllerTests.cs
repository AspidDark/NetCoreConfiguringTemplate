using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TB.Contracts.V1;
using TB.Domain;
using Xunit;

namespace TB.IntegrationTests
{
    public class PostControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            //Arrage
            await AuthenticateAsync();
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            //Arrage
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new Contracts.V1.Requests.CreatePostRequest {Name= "Test post" });
            //Act
            var response = await TestClient
                .GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id.ToString()));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.Id.Should().Be(createdPost.Id);
            returnedPost.Name.Should().Be("Test post");
        }

    }
}
