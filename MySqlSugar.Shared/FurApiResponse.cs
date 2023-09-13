using System;
using System.Collections.Generic;
using System.Text;

namespace MySqlSugar.Shared
{
    public class FurApiResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public int statusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object? Result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool succeeded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? errors { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? extras { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timestamp { get; set; }
    }
}
