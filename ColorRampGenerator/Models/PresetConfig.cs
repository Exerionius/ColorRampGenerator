using System.Collections.Generic;
using JetBrains.Annotations;

namespace ColorRampGenerator.Models
{
    public class PresetConfig
    {
        public List<ShiftsPreset> HuePresets { get; [UsedImplicitly] set; }
        public List<ShiftsPreset> SaturationPresets { get; [UsedImplicitly] set; }
        public List<ShiftsPreset> BrightnessPresets { get; [UsedImplicitly] set; }
    }
}