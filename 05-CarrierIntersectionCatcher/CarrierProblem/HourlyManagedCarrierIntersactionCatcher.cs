using System;
using CarrierProblem.Contracts;
using System.Collections.Generic;

namespace CarrierProblem
{
    public class HourlyManagedCarrierIntersactionCatcher : CarrierIntersectionCatcher<int>
    {
        public List<Carrier<int>> FindTheMostIntersectingCarriers(List<Carrier<int>> carriers)
        {
            ValidateCarrierList(carriers);

            var carrierCount = carriers.Count;
            var result = new List<Carrier<int>>();

            if (carrierCount == 0)
            {
                return result;
            }

            var intersectingCarriers = new Dictionary<int, List<Carrier<int>>>();
            var theMostIntersectingCarriersHour = 0;
            var maxIntersectingCarrierCount = 0;

            for (var hour = 0; hour < 24; hour++)
            {
                for (int carrierIndex = 0; carrierIndex < carrierCount; carrierIndex++)
                {
                    var currentCarrier = carriers[carrierIndex];
                    if (currentCarrier.CurrentTransport.CheckInTime <= hour &&
                        currentCarrier.CurrentTransport.CheckOutTime >= hour)
                    {
                        intersectingCarriers[hour] = GetCarrierListForASpecificHour(intersectingCarriers, hour);
                        intersectingCarriers[hour].Add(currentCarrier);

                        if (maxIntersectingCarrierCount < intersectingCarriers[hour].Count)
                        {
                            maxIntersectingCarrierCount = intersectingCarriers[hour].Count;
                            theMostIntersectingCarriersHour = hour;
                        }
                    }
                }
            }

            return intersectingCarriers[theMostIntersectingCarriersHour];
        }

        private List<Carrier<int>> GetCarrierListForASpecificHour(Dictionary<int, List<Carrier<int>>> intersectingCarriers, int hour)
        {
            return intersectingCarriers.ContainsKey(hour) ? intersectingCarriers[hour] : new List<Carrier<int>>();
        }

        private void ValidateCarrierList(List<Carrier<int>> carriers)
        {
            if (carriers == null)
            {
                throw new ArgumentNullException(nameof(carriers));
            }
        }
    }
}
