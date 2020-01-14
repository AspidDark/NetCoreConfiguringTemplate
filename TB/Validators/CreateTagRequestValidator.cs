using FluentValidation;
using TB.Contracts.V1.Requests;

namespace TB.Validators
{
    public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
    {
        public CreateTagRequestValidator()
        {
            RuleFor(x => x.TagName)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");

            //string specialText = "specialText";
            //RuleFor(x => x.TagName).Must(s=>s.Contains(specialText));
        }
    }
}
