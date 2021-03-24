using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.Events
{
    public interface IEventListener
    {
        void AddEventListener();
        void RemoveEventListener();
        void Invoke<T>(string eventName, T e);
    }
}
