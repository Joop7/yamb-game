using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yamb.Util
{
    public abstract class Subject
    {
        List<IObserver> _listObs = new List<IObserver>();

        public void AddObserver(IObserver inObs)
        {
            _listObs.Add(inObs);
        }

        public void NotifyObservers()
        {
            foreach (IObserver obs in _listObs)
                obs.UpdateDisplay();
        }
    }
}
