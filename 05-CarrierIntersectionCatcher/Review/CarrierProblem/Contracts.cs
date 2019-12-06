using System.Collections.Generic;

namespace CarrierProblem.Contracts
{
    // REVIEW: Her classı ayrı bir dosyada tutmak güzel bir alışkanlıktır. Ortak dosyada tuttuğunda kod bloğu büyüdüğünde yönetmesi sorun olabilir.
    public interface CarrierIntersectionCatcher<TransportTimeType>
    {
        List<Carrier<int>> FindTheMostIntersectingCarriers(List<Carrier<int>> carriers);
    } // REVIEW: Satır boşlukları
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
