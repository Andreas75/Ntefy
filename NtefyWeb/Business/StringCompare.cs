using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FuzzyString;
using System.Threading.Tasks;

namespace NtefyWeb.Business
{
    public static class StringCompare
    {
        public async static Task<bool> CompareResultToRequest(string req, string res)
        {
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);

            FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Strong;

            var result = req.ApproximatelyEquals(res, options, tolerance);

            return result;
        }
        
    }
}