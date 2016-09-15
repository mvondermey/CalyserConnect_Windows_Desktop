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

            public String Message { get; set; }
            public String UUID { get; set; }
            public String Command { get; set; }

            public String GetJSON()
            {
                //
                //MessageJSON myJson = new MessageJSON { Message = "Beep", UUID = "XLM", Command = "NULL"};
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                return serializer.Serialize(this);
            //
            }
    }
}
