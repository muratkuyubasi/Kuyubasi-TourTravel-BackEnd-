﻿using Microsoft.Extensions.Configuration;

namespace TourV2.Helper
{
    public class PathHelper
    {
        public IConfiguration _configuration;

        public PathHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string DocumentPath
        {
            get
            {
                return _configuration["DocumentPath"];
            }
        }

        public string UserProfilePath
        {
            get
            {
                return _configuration["UserProfilePath"];
            }
        }
        public string CourseFilePath
        {
            get
            {
                return _configuration["Courses"];
            }
        }
    }
}
