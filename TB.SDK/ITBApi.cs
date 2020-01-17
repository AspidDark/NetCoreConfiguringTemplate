using System;
using System.Threading.Tasks;
using Refit;
using System.Collections.Generic;
using TB.Contracts.V1.Requests;
using TB.Contracts.V1.Responses;

namespace TB.SDK
{
    [Headers("Authorization: Bearer")]
    public interface ITBApi
    {

        [Get("/api/v1/posts")]
        Task<ApiResponse<List<PostResponse>>> GetAllAsync();

        [Get("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> GetAsync(Guid postId);

        [Post("/api/v1/posts")]
        Task<ApiResponse<PostResponse>> CreateAsync([Body] CreatePostRequest createPostRequest);

        [Put("/api/v1/posts/{postId}")]
        Task<ApiResponse<PostResponse>> UpdateAsync(Guid postId, [Body] UpdatePostRequest updatePostRequest);

        [Delete("/api/v1/posts/{postId}")]
        Task<ApiResponse<string>> DeleteAsync(Guid postId);
    }
}
