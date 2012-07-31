using System;
using System.Collections.Generic;
using System.Linq;

namespace Facebook.Api
{
    /// <summary>Defines time periods supported by <see cref="Controllers.AdminController.GetMetrics(DateTime,DateTime,ApplicationMetricPeriod,String[])" />.</summary>
    public enum ApplicationMetricPeriod : long
    {
        /// <summary>A constant for the <c>Day</c> time period, or <c>86400</c>.</summary>
        Day = 86400,

        /// <summary>A constant for the <c>Week</c> time period, or <c>604800</c>.</summary>
        Week = 604800,

        /// <summary>A constant for the <c>Month</c> time period, or <c>2592000</c>.</summary>
        Month = 2592000
    }
}
