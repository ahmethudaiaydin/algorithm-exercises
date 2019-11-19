using CarrierProblem.Contracts;
using System;

//Kuryelerin günlük çalışma periyodunda olduğu kabul edilmiştir. Yani bir kurye gece 23:00 de iş alıp, ertesi gün saat 05:00 de varış yapamaz.
namespace CarrierProblem
{
    public class HourlyManagedTransport : Transport<int>
    {
        private readonly int _dayStartHour = 0;
        private readonly int _dayEndHour = 23;
        public int CheckInTime { get; }
        public int CheckOutTime { get; }

        public HourlyManagedTransport(int checkInHour, int checkOutHour)
        {
            ValidateParameters(checkInHour, checkOutHour);
            CheckInTime = checkInHour;
            CheckOutTime = checkOutHour;
        }

        private void ValidateParameters(int checkInHour, int checkOutHour)
        {
            //TODO: Burada parametre adı olarak, user'ın ilk set etmeye çalıştığı paramName'i kullanmak istiyorum.
            //Parametre adını ben içeride değiştirmiş olabilirim. Ancak son kullanıcı dışarıdan set ettiği parametre adını görmeli.
            if (checkInHour < _dayStartHour || checkInHour > _dayEndHour)
            {
                throw new ArgumentOutOfRangeException(nameof(checkInHour), $"Parameter {nameof(checkInHour)} must be between 0 to 23.");
            }

            if (checkOutHour < _dayStartHour || checkOutHour > _dayEndHour)
            {
                throw new ArgumentOutOfRangeException(nameof(checkOutHour), $"Parameter {nameof(checkOutHour)} must be between 0 to 23.");
            }

            if (checkInHour >= checkOutHour)
            {
                throw new ApplicationException($"Parameter {nameof(checkInHour)} must be smaller than {nameof(checkOutHour)}.");
            }
        }
    }
}
