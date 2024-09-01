using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Homes.Command.CreateViewedProduct
{
    public class CreateViewedProductResponse
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime ViewedDate { get; set; }
    }
}
