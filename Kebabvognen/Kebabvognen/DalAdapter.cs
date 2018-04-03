using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public abstract class DalAdapter
    {
        public abstract bool IsRunning();
        public abstract void Start();
        public abstract void Stop();
        public abstract Menu[] GetMenus();


        
    }
}
