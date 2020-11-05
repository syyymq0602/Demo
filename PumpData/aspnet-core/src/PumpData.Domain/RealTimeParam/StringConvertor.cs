﻿using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace PumpData.RealTimeParam
{
    public static class StringConvertor
    {
        public static BsonTimestamp ConvertToTimeStamp(this string strTime)
        {
            TimeSpan ts = Convert.ToDateTime(strTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var s = Convert.ToInt64(ts.TotalSeconds).ToString();
            return BsonTimestamp.Create(s);
        }
    }
}
