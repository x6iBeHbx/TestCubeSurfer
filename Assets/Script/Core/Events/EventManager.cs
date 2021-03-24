using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.Events
{
    public class EventManager
    {
        public delegate void Message<T>();

        private static EventManager _instance;
        private Dictionary<string, List<IEventListener>> events;

        private Dictionary<string, object> eventsCB;

        public EventManager()
        {
            events = new Dictionary<string, List<IEventListener>>();
        }

        public static EventManager Instance
        {
            get
            {
                if (EventManager._instance == null) _instance = new EventManager();
                return _instance;
            }
        }

        public void Add(string key, IEventListener listener)
        {
            if (events.TryGetValue(key, out List<IEventListener> listeners))
            {
                
                listeners.Add(listener);
            } else
            {
                List<IEventListener> list = new List<IEventListener>();
                list.Add(listener);
                events.Add(key, list);
            }
        }

        public void Remove(string key, IEventListener listener)
        {
            if (events.TryGetValue(key, out List<IEventListener> listeners))
            {
                listeners.Remove(listener);

                if (listeners.Count <= 0)
                {
                    events.Remove(key);
                }
            }
        }

        public void RemoveAllOnKey(string key)
        {
            events.Remove(key);
        }

        public void Dispatch<T>(string key, T e)
        {
            if (events.TryGetValue(key, out List<IEventListener> listeners))
            {
                foreach (IEventListener listener in listeners)
                {
                    listener.Invoke(key, e);
                }
            }
        }
    }
}
