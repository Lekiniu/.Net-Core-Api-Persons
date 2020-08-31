using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Persons.Data.Models;

namespace Persons.Api.Validators
{
    public class PersonModelValidator : AbstractValidator<PersonsModel>
    {
        public PersonModelValidator()
        {
            RuleFor(person => person.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("{PropertyName} is  empty")
                .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid")
                .Must(BeValidName).WithMessage("Length of {PropertyName} Invalid");

            RuleFor(person => person.NameEng)
               .Cascade(CascadeMode.Stop)
              .NotNull().WithMessage("{PropertyName} is  empty")
              .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid");

            RuleFor(person => person.Surname)
                .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage("{PropertyName} is  empty")
             .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid");

            RuleFor(person => person.SurnameEng)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid");

            RuleFor(person => person.PrivateNumber)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .Length(11, 11).WithMessage(" {PropertyName} must Contain 11 chars");

            RuleFor(person => person.MobileNumber)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .Length(12).WithMessage("Maximum Length of chars in  {PropertyName} is 12");

            RuleFor(person => person.PhoneNumber)
                .Cascade(CascadeMode.Stop)
            .Length(12).WithMessage("Maximum Length of chars in  {PropertyName} is 12");

            RuleFor(person => person.BirthDate)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .Must(BeValidDate).WithMessage("invalid {PropertyName}");
        }

        protected  bool BeValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }

        protected bool BeValidDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear && dobYear > (currentYear - 120))
            {
                return true;
            }
            return false;
        }
    }
}
