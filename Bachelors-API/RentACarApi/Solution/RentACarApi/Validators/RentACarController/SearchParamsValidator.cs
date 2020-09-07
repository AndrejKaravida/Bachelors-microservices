﻿using FluentValidation;
using RentACarApi.Data;

namespace RentACarApi.Validators.RentACarController
{
    public class SearchParamsValidator : AbstractValidator<SearchParams>
    {
        public SearchParamsValidator()
        {
            RuleFor(x => x.Location).NotNull().MinimumLength(2).MaximumLength(30);
            RuleFor(x => x.StartingDate).NotNull().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.ReturningDate).NotNull().MinimumLength(2).MaximumLength(20);
        }
    }
}
