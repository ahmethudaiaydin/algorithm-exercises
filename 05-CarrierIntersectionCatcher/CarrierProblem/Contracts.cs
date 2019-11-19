using System.Collections.Generic;

namespace CarrierProblem.Contracts
{
    public interface CarrierIntersectionCatcher<TransportTimeType>
    {
        List<Carrier<int>> FindTheMostIntersectingCarriers(List<Carrier<int>> carriers);
    }
    public interface Transport<T>
    {
        T CheckInTime { get; }
        T CheckOutTime { get; }
    }

    public interface Carrier<TransportTimeType>
    {
        Transport<TransportTimeType> CurrentTransport { get; }
    }
}
