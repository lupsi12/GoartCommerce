using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class CampaignProduct : EntityMongoBase
    {
        public string CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public int ProductId { get; set; }
    }
}