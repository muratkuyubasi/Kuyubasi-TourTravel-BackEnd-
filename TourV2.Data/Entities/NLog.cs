﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TourV2.Data
{
    public class NLog
    {
        public Guid Id { get; set; }
        public string MachineName { get; set; }
        public DateTime Logged { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Logger { get; set; }
        public string Properties { get; set; }
        public string Callsite { get; set; }
        public string Exception { get; set; }
        [MaxLength(50)]
        public string Source { get; set; }
    }
}
