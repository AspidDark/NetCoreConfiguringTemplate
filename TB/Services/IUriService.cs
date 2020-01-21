using System;
using TB.Contracts.V1.Requests.Queries;

namespace TB.Services
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);

        Uri GetAllPostsUri(PaginationQuery pagination = null);
    }
}
