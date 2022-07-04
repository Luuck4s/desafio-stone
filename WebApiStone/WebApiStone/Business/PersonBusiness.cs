using WebApiStone.Entities;
using FluentValidation;
using WebApiStone.Entities.Values;

namespace WebApiStone.Business
{
    public class PersonBusiness : AbstractValidator<Person>
    {
        public PersonBusiness()
        {
            RuleFor(person => person.Name).NotEmpty();
            RuleFor(person => person.LastName).NotEmpty();

            RuleFor(person => person.SkinColor).NotEmpty().NotNull()
                .Must(skinColor => SkinColorValues.Values().Contains(skinColor))
                .WithMessage($"The value must be between [{FormatValues(SkinColorValues.Values())}]");

            RuleFor(person => person.Sex).NotEmpty().NotNull()
                .Must(sex => SexValues.Values().Contains(sex))
                .WithMessage($"The value must be between [{FormatValues(SexValues.Values())}]"); 

            RuleFor(person => person.Education).NotEmpty().NotNull()
                .Must(education => EducationValues.Values().Contains(education))
                .WithMessage($"The value must be between [{FormatValues(EducationValues.Values())}]"); 

            RuleFor(person => person.FatherID).Must((person, fatherId) => fatherId != person.Id);
            RuleFor(person => person.MotherID).Must((person, motherId) => motherId != person.Id);
        }

        private string FormatValues(List<string> list)
        {
            return string.Join(", ", list);
        }
    }
}
