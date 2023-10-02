using CustomerApiService.Contracts;
using CustomerApiService.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Command.CreateCustomer
{
    public class CreateCustomerModelValidator : AbstractValidator<CustomerCreateModel>
    {
        private readonly ICustomerRepository _customerRepository;
 

        public CreateCustomerModelValidator(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));

            RuleFor(p => p.FirstName)
                    .NotEmpty().WithMessage("{Firstname} is required.")
                    .MaximumLength(50).WithMessage("{Lastname} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                   .NotEmpty().WithMessage("{Firstname} is required.")
                   .MaximumLength(50).WithMessage("{Firstname} must not exceed 50 characters.");

            RuleFor(p => p.Email)
                .EmailAddress()
               .NotEmpty().WithMessage("{EmailAddress} is required.");

            RuleFor(x => x.Email).Must(IsEmailUnique)
                        .WithMessage("EmailAddress already exists!");

            RuleFor(p => p.BirthdayInEpochStr)
                .Must(p => long.TryParse(p.ToString(), out var val) && val > 0).WithMessage("Invalid value.")
                .NotEmpty().WithMessage("{BirthdayInEpoch} is required.");
        }
        public bool IsEmailUnique(string email)
        {
            var isEmailExists = _customerRepository.GetCustomerByEmail(email).Count();
            if(isEmailExists>0)
            {
                return false;
            }
            return true;
        }
    }
}
