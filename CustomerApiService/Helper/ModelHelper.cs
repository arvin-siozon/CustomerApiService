using Azure.Core;
using CustomerApiService.Command.CreateCustomer;
using CustomerApiService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApiService.Helper
{
    public static class ModelHelper
    {
        public static CreateCustomerCommand Map(this CustomerCreateModel entity)
        {
            return new CreateCustomerCommand
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthdayInEpochStr = entity.BirthdayInEpochStr,
                Email = entity.Email,
            };
        }
        public static UpdateCustomerCommand Map(this CustomerUpdateModel entity)
        {
            return new UpdateCustomerCommand
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthdayInEpochStr = entity.BirthdayInEpochStr,
                Email = entity.Email,
            };
        }
        public static Customer Map(this CreateCustomerCommand request)
        {
            return new Customer()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthdayInEpoch = request.BirthdayInEpoch,
                Email = request.Email,
            };
        }
        public static Customer Map(this UpdateCustomerCommand request)
        {
            return new Customer()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthdayInEpoch = request.BirthdayInEpoch,
                Email = request.Email,
            };
        }
        public static BadRequestObjectResult Map(this FluentValidation.Results.ValidationResult entity)
        {
            return new BadRequestObjectResult(entity.Errors.Select(e => new
            {
                e.ErrorCode,
                e.PropertyName,
                e.ErrorMessage
            }));
        }

    }
}
