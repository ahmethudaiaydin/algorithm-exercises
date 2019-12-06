// REVIEW: using'lerde sorting ileride takip açısından önemli, önce System namespace'leri sonra custom namespace'leri yazarak sort edersen güzel olur
using System;
using CarrierProblem.Contracts;
using System.Collections.Generic;

namespace CarrierProblem
{
    // REVIEW: Bu kadar uzun isimlendirmeler anlaşılırlığı biraz zorlaştırıyor
    public class HourlyManagedCarrierIntersactionCatcher : CarrierIntersectionCatcher<int>
    {
        // REVIEW: Bu satırları aşağıdaki örnekler için ekledim
        #region Constants
        
        private const int HoursInADay = 24;
        private const int MinHourOfDay = 0;
        private const int MaxHourOfDay = 23;
        
        #endregion Constants

        #region Static readonly members

        private static readonly Carrier<int>[] EmptyCarriers = new Carrier<int>[0];

        #endregion Static readonly members

        public List<Carrier<int>> FindTheMostIntersectingCarriers(List<Carrier<int>> carriers)
        {
            // REVIEW: Throw edeceğin kompleks kontrol gerektirmeyen validasyanları aynı metodda yaparsan Exception stack frame'ini kısaltırsın
            ValidateCarrierList(carriers);

            var carrierCount = carriers.Count;
            // REVIEW: carrierCount controlünü tam burada yaparsan return edeceğin objeyi bir de result değişkenine atamaktan kurtulursun, 
            // hatta return edeceğin type list yerine array olup immutable bir obje olsa bunu global bir değişken üzerinden kullanıp her defasında yeni bir obje yaratmak zorunda kalmazsın,
            // Hatta burada yaptığın gibi "var result = new List<Carrier<int>>();" şeklinde yarattığın objeyi "count > 0" durumunda bir daha başka bir yerde kullanmama, açıkta kalması gibi hatalara daha az düşersin
            // List<T>'nin koduna bakarsan aynı yaklaşımı orada da _emptyArray tanımı içerisinde görebilirsin
            // https://referencesource.microsoft.com/#mscorlib/system/collections/generic/list.cs,cf7f4095e4de7646
            var result = new List<Carrier<int>>();

            if (carrierCount == 0)
            {
                return result;
            }

            var intersectingCarriers = new Dictionary<int, List<Carrier<int>>>();
            // REVIEW: Kullanılan değişken isimlerinin kendini anlatması önemli ama bunu da en kısa şekilde yapması okunaklılık açısından daha da önemli
            // Lokal değişkenler için maxHour, maxCarrierCount gibi, zaten metodun kendisi değişkeninin context'ini ve anlamını verecek
            // Değişken isimlendirmelerinde kısaltma önemli, zaten global olarak herkesin anlayabileceği kısaltmalar mevcur, 
            // nasıl maximum demiyor max diyoruz, carrierCount yerine de carrierCnt kullanabiliriz, 
            // kısatlmayıp uzun uzun yazınca değişkenin ilk tanım yapıldığı yerde nispeten anlamlı oluyor ama aşağıdaki for loop'unda olduğu gibi kod okunaklılığı da zorlaşıyor 
            var theMostIntersectingCarriersHour = 0;
            var maxIntersectingCarrierCount = 0;

            // REVIEW: Şayet array'de verilen değerler sadece saat değil de saniye ve hatta nano saniye cinsinden bir durum olsa bu mantık nasıl olacaktı?
            // Buradaki yaklaşımı biraz T anındaki concurrency hesaplama gibi ele almak daha güzel bir yaklaşım olurdu
            for (var hour = 0; hour < 24; hour++)
            {
                for (int carrierIndex = 0; carrierIndex < carrierCount; carrierIndex++)
                {
                    // REVIEW: Değişken tanımlarından ve atalamardan sonra eğer yoğun bir işlem bloğu varsa bir satır boşluk atarsan takibi kolaylaşır, 
                    // iki farklı mantıksal işlemi birbirinden ayırmış olursun
                    /*
                    var currentCarrier = carriers[carrierIndex]; 

                    if (currentCarrier.CurrentTransport.CheckInTime <= hour &&
                        currentCarrier.CurrentTransport.CheckOutTime >= hour)
                    {
                        ...
                    }
                    */
                    // REVIEW: Verilen değerler 0..23 aralığında mı, CheckIn <= CheckOut kontrolleri?
                    var currentCarrier = carriers[carrierIndex]; 
                    if (currentCarrier.CurrentTransport.CheckInTime <= hour &&
                        currentCarrier.CurrentTransport.CheckOutTime >= hour)
                    {
                        // REVIEW: Önce "GetCarrierListForASpecificHour"in yorumuna bak
                        // REVIEW: "GetCarrierListForASpecificHour"i ayrı metod olarak yazmak yerine aşağıdaki gibi yazabilir hatta zaten dış loop "hour" için 
                        // iç loop'da her defasında tekrar tekrar yapmak yerine ya loop'ların yerini değiştirip ilerleyebilir veya dış loop'da List'i bir sefer yaratıp içerde kontrol etmeden kullanabilir 
                        // veya en dışta en başta bir defa yaratıp kullanabilirdin. Yorumların her birinin örneğini altta yazıyorum ki daha rahat anlaşılsın
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

        // REVIEW: Açıkmalama örneği
        public Carrier<int>[] GetMaxConcurrentCarriers(List<Carrier<int>> carriers)
        {
            if (carriers == null) {
                throw new ArgumentNullException(nameof(carriers));
            }

            var carrierCnt = carriers.Count;
            if (carrierCnt == 0) {
                return EmptyCarriers;
            }

            var maxHour = 0;
            var maxCnt = -1;

            var carriersByHour = new List<Carrier<int>>[HoursInADay];

            for (var hourIndx = 0; hourIndx < HoursInADay; hourIndx++) {
                var carriersAtCurrHour = carriersByHour[hourIndx] = new List<Carrier<int>>();

                for (int carrierIndx = 0; carrierIndx < carrierCnt; carrierIndx++) {
                    var currCarrier = carriers[carrierIndx]; 
                    var currTransport = currCarrier.CurrentTransport;

                    var checkIn = Math.Min(MaxHourOfDay, Math.Max(MinHourOfDay, currTransport.CheckInTime));
                    var checkOut = Math.Min(MaxHourOfDay, Math.Max(MinHourOfDay, currTransport.CheckOutTime));

                    if ((checkIn <= checkOut) && 
                        (checkIn <= hourIndx && checkOut >= hourIndx)) {
                        carriersAtCurrHour.Add(currCarrier);

                        if (maxCnt < carriersAtCurrHour.Count) {
                            maxCnt = carriersAtCurrHour.Count;
                            maxHour = hourIndx;
                        }
                    }
                }
            }

            return maxCnt == -1
                        ? EmptyCarriers 
                            : carriersByHour[maxHour].ToArray();
        }


        private List<Carrier<int>> GetCarrierListForASpecificHour(Dictionary<int, List<Carrier<int>>> intersectingCarriers, int hour)
        {
            // REVIEW: intersectingCarriers[hour] zaten ContainsKey kontrolü yapar, bu durumda iki defa kontrol yaptırmış oluyorsun           
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
