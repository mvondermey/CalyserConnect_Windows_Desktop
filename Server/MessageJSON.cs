using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace calyserconnect
{
    class MessageJSON
    {
        //
            public String TimeStamp { get; set; }
            public String Message { get; set; }
            public String UUID { get; set; }
            public String Command { get; set; }

        //
            public String GetJSON()
            {
            //
            DateTime baseDate = new DateTime(1970, 1, 1);
            TimeSpan diff = DateTime.Now - baseDate;
            this.TimeStamp = diff.Milliseconds.ToString();
            //
            JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(this);
                //
            }
    }
}
