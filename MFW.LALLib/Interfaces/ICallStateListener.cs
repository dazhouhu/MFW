using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFW.LALLib
{
    public interface ICallStateListener
    {
        void handleEvent(Call call);
    }
}
