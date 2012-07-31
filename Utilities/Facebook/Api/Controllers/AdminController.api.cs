using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook.Json;

namespace Facebook.Api.Controllers
{
    public partial class AdminController
    {
        /// <summary>Returns specified metrics for your application, given a time period.</summary>
        /// <param name="startTime">A Unix time for the start of the range (inclusive).</param>
        /// <param name="endTime">A Unix time for the end of the range (inclusive). The <code>end_time</code> cannot be more than 30 days after the <code>start_time</code>.</param>
        /// <param name="period">The length of the period, in seconds, during which the metrics were collected. Currently, the only supported periods are 86400 (1 day), 604800 (7-days), and 2592000 (30 days).</param>
        /// <param name="metrics">A JSON-encoded list of metrics to retrieve (e.g. <code>["active_users", "canvas_page_views"]</code>).</param>
        public FacebookResponse<FacebookList<Metrics>> GetMetrics(DateTime startTime, DateTime endTime, ApplicationMetricPeriod period, params String[] metrics)
        {
            return this.GetMetrics(startTime.ToUnixDateTime(), endTime.ToUnixDateTime(), (Int64)period, JsonConvert.SerializeObject(metrics));
        }
    }
}
