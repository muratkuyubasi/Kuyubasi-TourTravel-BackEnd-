﻿using System;


namespace TourV2.Data.Dto
{
    public class PageActionDto
    {
        public Guid Id { get; set; }
        public Guid ActionId { get; set; }
        public Guid PageId { get; set; }
        public bool Flag { get; set; } = true;
    }
}
