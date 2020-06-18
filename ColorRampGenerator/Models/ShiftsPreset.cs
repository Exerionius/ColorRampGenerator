namespace ColorRampGenerator.Models
{
    public class ShiftsPreset
    {
        public int LeftShift { get; set; }
        public int RightShift { get; set; }
        public string Name { get; set; }
        public bool DefaultSelected { get; set; }
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