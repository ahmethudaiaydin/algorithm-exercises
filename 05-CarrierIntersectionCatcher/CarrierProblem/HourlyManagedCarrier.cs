using CarrierProblem.Contracts;
using System;

namespace CarrierProblem
{
    public class HourlyManagedCarrier : Carrier<int>
    {
        public Transport<int> CurrentTransport { get; set; }
        public HourlyManagedCarrier(Transport<int> hourlyManagedTransport)
        {
            CurrentTransport = hourlyManagedTransport ?? throw new ArgumentNullException(nameof(hourlyManagedTransport));
        }
    }
}
