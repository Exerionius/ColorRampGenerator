using JetBrains.Annotations;

namespace ColorRampGenerator.Models
{
    public class ShiftsPreset
    {
        public int LeftShift { get; [UsedImplicitly] set; }
        public int RightShift { get; [UsedImplicitly] set; }
        public string Name { get; set; }
        public bool DefaultSelected { get; [UsedImplicitly] set; }
        public bool Custom { get; set; }

        public ShiftsPreset(int left, int right, string name, bool defaultSelected = false, bool custom = false)
        {
            LeftShift = left;
            RightShift = right;
            Name = name;
            DefaultSelected = defaultSelected;
            Custom = custom;
        }

        public ShiftsPreset Clone()
        {
            return new ShiftsPreset(LeftShift, RightShift, Name, DefaultSelected, Custom);
        }
    }
}