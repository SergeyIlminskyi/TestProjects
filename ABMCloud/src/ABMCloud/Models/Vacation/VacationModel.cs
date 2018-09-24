﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABMCloud.Entities;

namespace ABMCloud.Models
{
    public class VacationModel
    {
        public IReadOnlyList<VacationInfo> VacationsList { get; set; }

        public long VacationsCount { get; set; } //вынести в базовый
    }
}