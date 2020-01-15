using Swashbuckle.AspNetCore.Filters;
using TB.Contracts.V1.Requests;

namespace TB.SwaggerExamples.Request
{
    public class CreateTagRequestExample : IExamplesProvider<CreateTagRequest>
    {
        public CreateTagRequest GetExamples()
        {
            return new CreateTagRequest
            {
                TagName = "new tag"
            };
        }
    }
}
