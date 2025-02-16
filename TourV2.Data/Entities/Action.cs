﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourV2.Data
{
    public class Action : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CreatedBy")]
        public User CreatedByUser { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifiedByUser { get; set; }
        [ForeignKey("DeletedBy")]
        public User DeletedByUser { get; set; }
    }
}
