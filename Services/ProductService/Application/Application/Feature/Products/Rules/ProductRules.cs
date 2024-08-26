using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Products.Rules
{
    public class ProductRules
    {

        public void CheckIfCategoryIdsAreValid(IEnumerable<int> categoryIds)
        {
            if (categoryIds == null || !categoryIds.Any())
            {
                throw new ValidationException("At least one category must be selected.");
            }
        }
    }
}
