﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ConsultantImage : File
    {
        public long ConsultantId { get; set; }
        public long FileTypeId { get; set; }
    }
}