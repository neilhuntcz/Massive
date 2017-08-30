using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface IServiceCall
    {
        object GetOne<T>(T RecordID, string serviceURI);
        IList GetAll(string serviceURI);
        HttpResponseMessage Delete<T>(T RecordID, string serviceURI);
        HttpResponseMessage Add<T>(T addition, string serviceURI);
        HttpResponseMessage Update<T>(T toupdate, string serviceURI);
    }
}
