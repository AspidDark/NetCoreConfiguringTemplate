using Microsoft.AspNetCore.Mvc;

namespace TB.Contracts.V1.Requests.Queries
{
    public class GetAllPostsQuery
    {
        [FromQuery(Name = "userId")]
        public string UserId { get; set; }
    }
}
