using CarrierProblem.Contracts;
using System.Collections.Generic;

namespace CarrierProblem
{
    public class HourManagedCarrierManager
    {
        private readonly HourlyManagedCarrierIntersactionCatcher _intersactionCatcher = new HourlyManagedCarrierIntersactionCatcher();

        private readonly List<Carrier<int>> _carrierList = new List<Carrier<int>>(); //No need to make it public.

        public HourManagedCarrierManager Add(Carrier<int> carrier)
        {
            _carrierList.Add(carrier);
            return this;
        }

        public List<Carrier<int>> FindTheMostIntersectingCarriers()
        {
            return _intersactionCatcher.FindTheMostIntersectingCarriers(_carrierList);
        }
    }

}
