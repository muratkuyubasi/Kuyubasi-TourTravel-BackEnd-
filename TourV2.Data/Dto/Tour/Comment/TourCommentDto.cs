﻿using System;

namespace TourV2.Data.Dto
{
    public class TourCommentDto
    {
        public Guid Id { get; set; }
        public int ActiveTourId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public short? Point { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public ActiveTour ActiveTour { get; set; }
    }
}
