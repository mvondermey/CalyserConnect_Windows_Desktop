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
            
            TimeSpan diff = DateTime.Now - new DateTime(1970, 1, 1);
            this.TimeStamp = diff.TotalMilliseconds.ToString();
            //
            JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(this);
                //
            }
    }
}
