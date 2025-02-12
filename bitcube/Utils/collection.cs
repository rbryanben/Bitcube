using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bitcube.Utils
{
    /*
     *  This class will contain methods that can be used across the entire project
     */
    public class collection
    {

        /*
         *   Generate a key that can be used for purposes tokens and api key or reference
         *   Why ? central place to change the complexity of the generation of these UUI
         */
        public static string generateKey()
        {
            // Lets be simple here
            return Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        }

        /*
         *  Error response object creator just to standardize the error response code
         */
        public static object errorResponse(int statusCode,string error)
        {
            return new
            {
                statusCode = statusCode,
                error = error
            };
        }
    }
}
