
using System;

namespace Guidelines.Characteristics
{
    public interface ICharacteristic
    {
        void Start();
        void Finish();
    }

    public class TimedActivityCharacteristic : ICharacteristic
    {
        public DateTime StartedTime { get; private set; }
        public DateTime? CompletedTime { get; private set; } = DateTime.MinValue;

        public TimeSpan ActivityTotalTime
        {
            get
            {
                var endPointTime = CompletedTime ?? DateTime.UtcNow;
                return endPointTime.Subtract(StartedTime);
            }
        }

        public void Start()
        {
            StartedTime = DateTime.UtcNow;
        }

        public void Finish()
        {
            CompletedTime = DateTime.UtcNow;
        }
    }
}