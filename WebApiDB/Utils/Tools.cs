using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiHttpClient.Utils
{
    public class Tools
    {
        public static int GetRandomNumber(int startPoint, int endPoint)
        {
            Random rnd = new Random();
            return rnd.Next(startPoint * 1000, endPoint * 1000);
        }
    }
}