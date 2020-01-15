using Swashbuckle.AspNetCore.Filters;
using TB.Contracts.V1.Responses;

namespace TB.SwaggerExamples.Response
{
    public class TagResponseExample : IExamplesProvider<TagResponse>
    {
        public TagResponse GetExamples()
        {
            return new TagResponse
            {
                Name = "new tag"
            };
        }
    }
}
