using ColorRampGenerator.Prism;

namespace ColorRampGenerator.Models
{
    public class Shift: BindableBase
    {
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }
        private int _value;

        private Shift(int value)
        {
            _value = value;
        }

        public static implicit operator int(Shift shift) => shift._value;
        public static implicit operator Shift(int value) => new Shift(value);

        public override string ToString()
        {
            return _value > 0 ? "+" + _value : _value.ToString();
        }
    }
}