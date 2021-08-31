using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChooseYourShoes.Models
{
    public class CodeDecode:BaseMessage
    {
        public string CodeTxt { get; set; }
        public string DecodeTxt { get; set; }
        public string CodeHeaderTxt { get; set; }
        public string SequenceNbr { get; set; }
    }
}