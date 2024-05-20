using FluentValidation;
using FullCoffee.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCoffee.Service.Validations
{
    public class UserDtoValidator:AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(x=>x.FirstName).NotEmpty().WithMessage("{PropertyName} is required.").NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x=>x.Email).NotEmpty().WithMessage("{PropertyName} is required.").NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x=>x.Password).NotEmpty().WithMessage("{PropertyName} is required.").NotNull().WithMessage("{PropertyName} is required.");
            RuleFor(x=>x.LastName).NotEmpty().WithMessage("{PropertyName} is required.").NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
