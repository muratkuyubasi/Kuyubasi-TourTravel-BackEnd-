﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourV2.Data.Dto
{
    public class TourRecordDto
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int TourId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string LanguageCode { get; set; }
    }
}
