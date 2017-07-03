using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewedApartmentThingy
{
    [Serializable]
    public class Result
    {
        public Result()
        {

        }
        private string _resultCode;
        private string _resultText;


        public string resultCode
        {
            get { return _resultCode; }
            set { _resultCode = value; }
        }
        public string resultText
        {
            get { return _resultText; }
            set { _resultText = value; }
        }
    }
}