﻿using System;

namespace TourV2.Data
{
    public class AppSetting: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
