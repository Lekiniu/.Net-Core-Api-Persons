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
              .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid")
              .Matches("^[A-Za-z]*$").WithMessage("{PropertyName} Invalid");

            RuleFor(person => person.Surname)
                .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage("{PropertyName} is  empty")
             .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid");
            

            RuleFor(person => person.SurnameEng)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid")
             .Matches("^[A-Za-z ]*$").WithMessage("{PropertyName} Invalid");

            RuleFor(person => person.PrivateNumber)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .Length(11, 11).WithMessage(" {PropertyName} must Contain 11 chars")
             .Matches("^([0-9])*$").WithMessage("{PropertyName} Invalid");

            RuleFor(person => person.MobileNumber)
                .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("{PropertyName} is  empty")
            .MaximumLength(12).WithMessage("Maximum Length of chars in  {PropertyName} is 12")
            .Matches("^([0-9])*$").WithMessage("{PropertyName} Invalid");

            RuleFor(person => person.PhoneNumber)
                .Cascade(CascadeMode.Stop)
            .MaximumLength(12).WithMessage("Maximum Length of chars in  {PropertyName} is 12")
            .Matches("^[0-9]$").WithMessage("{PropertyName} Invalid");

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

    public class PersonAddressModelValidator : AbstractValidator<AddressesModel>
    {
        public PersonAddressModelValidator()
        {
            RuleFor(address => address.Country)
               .Cascade(CascadeMode.Stop)
               .MaximumLength(20).WithMessage("Length of {PropertyName} Invalid")
               .Matches("^[A-Za-z]*$").WithMessage("{PropertyName} Invalid");

            RuleFor(address => address.City)
             .Cascade(CascadeMode.Stop)
             .MaximumLength(20).WithMessage("Length of {PropertyName} Invalid")
             .Matches("^[A-Za-z]*$").WithMessage("{PropertyName} Invalid");

            RuleFor(address => address.Street)
              .Cascade(CascadeMode.Stop)
              .MaximumLength(50).WithMessage("Length of {PropertyName} Invalid");     
        }
    }

    public class PersonFileModelValidator : AbstractValidator<FilesModel>
    {
        public PersonFileModelValidator()
        {
            RuleFor(file => file.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("{PropertyName} is  empty")
               .MaximumLength(20).WithMessage("Length of {PropertyName} Invalid");

            RuleFor(address => address.Url)
             .Cascade(CascadeMode.Stop)
             .NotNull().WithMessage("{PropertyName} is  empty");

        }
    }

    public class PersonTypeModelValidator : AbstractValidator<PersonTypeModel>
    {
        public PersonTypeModelValidator()
        {
            RuleFor(file => file.TypeName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("{PropertyName} is  empty")
               .MaximumLength(20).WithMessage("Length of {PropertyName} Invalid");
        }
    }
}
