using MediatR;
using System.Collections.Generic;
using System.ComponentModel;

namespace Application.Feature.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<List<GetAllProductsResponse>>
    {
        [DefaultValue(1)]
        public int? UserId { get; set; }

        [DefaultValue(1)]
        public int? CategoryId { get; set; }

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
