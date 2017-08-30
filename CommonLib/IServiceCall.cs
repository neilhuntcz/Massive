using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public interface IServiceCall
    {
        object GetOne<T>(T RecordID, string serviceURI);
        IList GetAll(string serviceURI);
        void Delete<T>(T RecordID, string serviceURI);
        void Add<T>(T addition, string serviceURI);
        void Update<T>(T toupdate, string serviceURI);
    }
}
