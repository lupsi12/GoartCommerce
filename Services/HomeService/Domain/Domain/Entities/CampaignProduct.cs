using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;

namespace Domain.Entities
{
    public class CampaignProduct : EntityBase
    {
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}