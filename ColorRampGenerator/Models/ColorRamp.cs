using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using ColorRampGenerator.Prism;
using ColorRampGenerator.Tools;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;

namespace ColorRampGenerator.Models
{
    public class ColorRamp: BindableBase
    {
        public enum ShiftMode
        {
            SameDirection,
            OppositeDirections
        }
        
        public HsbColor BaseColor => Colors[Colors.Count / 2];
        public ChartValues<HsbColor> Colors { get; }
        public SeriesCollection SeriesCollection { get; }

        public int Size
        {
            get => Colors.Count;
            set
            {
                if(Colors.Count == value) return;
                
                var baseColor = BaseColor;
                Init(baseColor, value);
                ApplyShifts();
                
                RaisePropertyChanged(nameof(Size));
                RaisePropertyChanged(nameof(Colors));
                RaisePropertyChanged(nameof(HueShifts));
                RaisePropertyChanged(nameof(SaturationShifts));
                RaisePropertyChanged(nameof(BrightnessShifts));
            }
        }

        #region Hue Shifts 
        public ObservableCollection<Shift> HueShifts { get; }
        public Shift GeneralHueShift { get; }
        public ShiftMode GeneralHueShiftMode
        {
            get => _generalHueShiftMode;
            set
            {
                _generalHueShiftMode = value;
                RaisePropertyChanged(nameof(GeneralHueShiftMode));
                SetHueShifts(GeneralHueShift.Value);
            }
        }
        private ShiftMode _generalHueShiftMode;
        public bool CustomHueShifts
        {
            get => _customHueShifts;
            set
            {
                _customHueShifts = value;
                RaisePropertyChanged(nameof(CustomHueShifts));
                if (!value)
                {
                    SetHueShifts(GeneralHueShift.Value);
                }
            }
        }
        private bool _customHueShifts;
        #endregion
        
        #region Saturation Shifts
        public ObservableCollection<Shift> SaturationShifts { get; }
        public Shift GeneralSaturationShift { get; }
        public ShiftMode GeneralSaturationShiftMode
        {
            get => _generalSaturationShiftMode;
            set
            {
                _generalSaturationShiftMode = value;
                RaisePropertyChanged(nameof(GeneralSaturationShiftMode));
                SetSaturationShifts(GeneralSaturationShift.Value);
            }
        }
        private ShiftMode _generalSaturationShiftMode;
        public bool CustomSaturationShifts
        {
            get => _customSaturationShifts;
            set
            {
                _customSaturationShifts = value;
                RaisePropertyChanged(nameof(CustomSaturationShifts));
                if (!value)
                {
                    SetSaturationShifts(GeneralSaturationShift.Value);
                }
            }
        }
        private bool _customSaturationShifts;
        #endregion

        #region Brightness Shifts
        public ObservableCollection<Shift> BrightnessShifts { get; }
        public Shift GeneralBrightnessShift { get; }

        public ShiftMode GeneralBrightnessShiftMode
        {
            get => _generalBrightnessShiftMode;
            set
            {
                _generalBrightnessShiftMode = value;
                RaisePropertyChanged(nameof(GeneralBrightnessShiftMode));
                SetBrightnessShifts(GeneralBrightnessShift.Value);
            }
        }
        private ShiftMode _generalBrightnessShiftMode;
        public bool CustomBrightnessShifts
        {
            get => _customBrightnessShifts;
            set
            {
                _customBrightnessShifts = value;
                RaisePropertyChanged(nameof(CustomBrightnessShifts));
                if (!value)
                {
                    SetBrightnessShifts(GeneralBrightnessShift.Value);
                }
            }
        }
        private bool _customBrightnessShifts;
        #endregion
        

        public ColorRamp(HsbColor baseColor, int size,
            int generalHueShift, int generalSaturationShift, int generalBrightnessShift)
        {
            Colors = new ChartValues<HsbColor>();
            HueShifts = new ObservableCollection<Shift>();
            SaturationShifts = new ObservableCollection<Shift>();
            BrightnessShifts = new ObservableCollection<Shift>();

            GeneralHueShift = generalHueShift;
            GeneralHueShift.PropertyChanged += (sender, args) =>
            {
                SetHueShifts(GeneralHueShift.Value);
            };
            GeneralHueShiftMode = ShiftMode.SameDirection;
            
            GeneralSaturationShift = generalSaturationShift;
            GeneralSaturationShift.PropertyChanged += (sender, args) =>
            {
                SetSaturationShifts(GeneralSaturationShift.Value);
            };
            GeneralSaturationShiftMode = ShiftMode.OppositeDirections;
            
            GeneralBrightnessShift = generalBrightnessShift;
            GeneralBrightnessShift.PropertyChanged += (sender, args) =>
            {
                SetBrightnessShifts(GeneralBrightnessShift.Value);
            };
            GeneralBrightnessShiftMode = ShiftMode.SameDirection;

             Init(baseColor, size);
            ApplyShifts();
            
            var mapper = Mappers.Xy<HsbColor>()
                .X(v => v.Hue)
                .Y(v => v.Brightness)
                .Fill((v, _) => new SolidColorBrush(ColorHelper.GetRgb(v)));
            
            SeriesCollection = new SeriesCollection(mapper)
            {
                new ScatterSeries
                {
                    Title = string.Empty,
                    Values = Colors,
                    LabelPoint = p => $"Hue: {p.X}, Brightness: {p.Y}",
                    PointGeometry = DefaultGeometries.Diamond,
                    MaxPointShapeDiameter = 15
                }
            };
            
            BaseColor.PropertyChanged += (o, e)
                => ApplyShifts();
        }

        public ColorRamp Clone()
        {
            return new ColorRamp(BaseColor.Clone(), Colors.Count,
                GeneralHueShift.Clone(), GeneralSaturationShift.Clone(), GeneralBrightnessShift.Clone())
            {
                GeneralHueShiftMode = GeneralHueShiftMode,
                GeneralSaturationShiftMode = GeneralSaturationShiftMode,
                GeneralBrightnessShiftMode = GeneralBrightnessShiftMode
            };
        }

        private void Init(HsbColor baseColor, int size)
        {
            for(var i = 0; i < HueShifts.Count; i++)
            {
                HueShifts[i].PropertyChanged -= OnShiftPropertyChange;
                SaturationShifts[i].PropertyChanged -= OnShiftPropertyChange;
                BrightnessShifts[i].PropertyChanged -= OnShiftPropertyChange;
            }
            
            Colors.Clear();
            HueShifts.Clear();
            SaturationShifts.Clear();
            BrightnessShifts.Clear();
            
            var middle = size / 2;
            for (var i = 0; i < size; i++)
            {
                if (i < middle)
                {
                    Colors.Add(baseColor.Clone());
                    HueShifts.Add(GeneralHueShift.Value);
                    SaturationShifts.Add(GeneralSaturationShift.Value);
                    BrightnessShifts.Add(GeneralBrightnessShift.Value);
                }
                else if (i == middle)
                {
                    Colors.Add(baseColor);
                    HueShifts.Add(0);
                    SaturationShifts.Add(0);
                    BrightnessShifts.Add(0);
                }
                else
                {
                    Colors.Add(baseColor.Clone());
                    HueShifts.Add(GeneralHueShiftMode == ShiftMode.OppositeDirections ? GeneralHueShift.Value : GeneralHueShift * -1);
                    SaturationShifts.Add(GeneralSaturationShiftMode == ShiftMode.OppositeDirections ? GeneralSaturationShift.Value : GeneralSaturationShift * -1);
                    BrightnessShifts.Add(GeneralBrightnessShiftMode == ShiftMode.OppositeDirections ? GeneralBrightnessShift.Value : GeneralBrightnessShift * -1);
                }

                HueShifts[i].PropertyChanged += OnShiftPropertyChange;
                SaturationShifts[i].PropertyChanged += OnShiftPropertyChange;
                BrightnessShifts[i].PropertyChanged += OnShiftPropertyChange;
            }
        }

        private void ApplyShifts()
        {
            var middle = Colors.Count / 2;
            var baseColor = BaseColor;
            
            var totalHueShift = 0;
            var totalSaturationShift = 0;
            var totalBrightnessShift = 0;
            for (var i = middle - 1; i >= 0; i--)
            {
                totalHueShift += HueShifts[i];
                totalSaturationShift += SaturationShifts[i];
                totalBrightnessShift += BrightnessShifts[i];
                Colors[i].Hue = baseColor.Hue + totalHueShift;
                Colors[i].Saturation = baseColor.Saturation + totalSaturationShift;
                Colors[i].Brightness = baseColor.Brightness + totalBrightnessShift;
            }

            totalHueShift = 0;
            totalSaturationShift = 0;
            totalBrightnessShift = 0;
            for (var i = middle + 1; i < Colors.Count; i++)
            {
                totalHueShift += HueShifts[i];
                totalSaturationShift += SaturationShifts[i];
                totalBrightnessShift += BrightnessShifts[i];
                Colors[i].Hue = baseColor.Hue + totalHueShift;
                Colors[i].Saturation = baseColor.Saturation + totalSaturationShift;
                Colors[i].Brightness = baseColor.Brightness + totalBrightnessShift;
            }
        }

        private void SetHueShifts(int value)
        {
            var middle = HueShifts.Count / 2;
            for (var i = 0; i < HueShifts.Count; i++)
            {
                if (i < middle)
                {
                    HueShifts[i].Value = value;
                }
                else if (i > middle)
                {
                    HueShifts[i].Value = GeneralHueShiftMode == ShiftMode.OppositeDirections
                        ? value
                        : value * -1;
                }
            }
        }

        private void SetSaturationShifts(int value)
        {
            var middle = SaturationShifts.Count / 2;
            for (var i = 0; i < SaturationShifts.Count; i++)
            {
                if (i < middle)
                {
                    SaturationShifts[i].Value = value;
                }
                else if (i > middle)
                {
                    SaturationShifts[i].Value = GeneralSaturationShiftMode == ShiftMode.OppositeDirections
                        ? value
                        : value * -1;
                }
            }
        }

        private void SetBrightnessShifts(int value)
        {
            var middle = BrightnessShifts.Count / 2;
            for (var i = 0; i < BrightnessShifts.Count; i++)
            {
                if (i < middle)
                {
                    BrightnessShifts[i].Value = value;
                }
                else if (i > middle)
                {
                    BrightnessShifts[i].Value = GeneralBrightnessShiftMode == ShiftMode.OppositeDirections
                        ? value
                        : value * -1;
                }
            }
        }

        private void OnShiftPropertyChange(object sender, PropertyChangedEventArgs args)
        {
            ApplyShifts();
        }
    }
}