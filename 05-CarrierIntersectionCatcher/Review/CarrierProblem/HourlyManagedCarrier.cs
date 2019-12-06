using CarrierProblem.Contracts;
using System;

namespace CarrierProblem
{
    public class HourlyManagedCarrier : Carrier<int>
    {
        // REVIEW: HourlyManagedCarrier kullanım olarak mutable bir class olarak kullanılmış, thread lock'dak kaçınmak için bu iyi birşey 
        // ancak CurrentTransport property'sinin setter'ı var, buna gerek yok. e.g. CurrentTransport { get; }
        public Transport<int> CurrentTransport { get; set; }
        // REVIEW: Satır boşlukları ve #region'lar
        public HourlyManagedCarrier(Transport<int> hourlyManagedTransport)
        {
            CurrentTransport = hourlyManagedTransport ?? throw new ArgumentNullException(nameof(hourlyManagedTransport));
        }
    }
}
