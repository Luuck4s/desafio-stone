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
            RuleFor(person => person.SkinColor).NotEmpty().NotNull().Must(skinColor => SkinColorValues.Values().Contains(skinColor));
            RuleFor(person => person.Sex).NotEmpty().NotNull().Must(sex => SexValues.Values().Contains(sex));
            RuleFor(person => person.Education).NotEmpty().NotNull().Must(education => SexValues.Values().Contains(education));

            RuleFor(person => person.FatherID).Must((person, fatherId) => fatherId != person.Id);
            RuleFor(person => person.MotherID).Must((person, motherId) => motherId != person.Id);
        }
    }
}
