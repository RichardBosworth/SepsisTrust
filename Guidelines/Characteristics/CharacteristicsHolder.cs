using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guidelines.Characteristics
{
    public class CharacteristicsHolder
    {
        private List<ICharacteristic> _characteristics = new List<ICharacteristic>();

        public void AddCharacteristic<T>() where T : ICharacteristic
        {
            if (_characteristics.Any(characteristic => characteristic is T))
            {
                return;
            }
            else
            {
                var characteristicInstance = Activator.CreateInstance<T>();
                _characteristics.Add(characteristicInstance);
            }
        }

        public void RemoveCharacteristic<T>() where T : ICharacteristic
        {
            _characteristics.RemoveAll(characteristic => characteristic is T);
        }

        public T GetCharacteristic<T>() where T : ICharacteristic
        {
            ICharacteristic foundCharacteristic = _characteristics.Single(characteristic => characteristic is T);
            return (T) foundCharacteristic;
        }

        public void Start()
        {
            foreach (var characteristic in _characteristics)
            {
                characteristic.Start();
            }
        }

        public void Finish()
        {
            foreach (var characteristic in _characteristics)
            {
                characteristic.Finish();
            }
        }
    }
}
