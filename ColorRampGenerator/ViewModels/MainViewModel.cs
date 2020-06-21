using System.Collections.ObjectModel;
using System.Linq;
using ColorRampGenerator.Models;
using ColorRampGenerator.Prism;
using ColorRampGenerator.Tools;

namespace ColorRampGenerator.ViewModels
{
    public class MainViewModel: BindableBase
    {
        private readonly HsbColor _defaultColor = new HsbColor(120, 60, 50);
        
        public ObservableCollection<ColorRamp> ColorRamps { get; }

        public ColorRamp SelectedColorRamp
        {
            get => _selectedColorRamp;
            set
            {
                _selectedColorRamp = value;
                RaisePropertyChanged(nameof(SelectedColorRamp));
            }
        }
        private ColorRamp _selectedColorRamp;

        private bool CanRemoveAnyColorRamps => ColorRamps.Count > 1;
        
        public DelegateCommand AddColorRampCommand { get; }
        public DelegateCommand<ColorRamp> RemoveColorRampCommand { get; }
        public DelegateCommand CopyCommand { get; }

        public MainViewModel()
        {
            ColorRamps = new ObservableCollection<ColorRamp>
            {
                new ColorRamp(_defaultColor.Clone(), 5, 10, 10, 15)
            };
            SelectedColorRamp = ColorRamps[0];
            
            AddColorRampCommand = new DelegateCommand(AddColorRamp);
            RemoveColorRampCommand = new DelegateCommand<ColorRamp>(RemoveColorRamp, CanRemoveRamp)
                .ObservesProperty(() => CanRemoveAnyColorRamps);
            CopyCommand = new DelegateCommand(CopyToClipboard);
        }

        private void AddColorRamp()
        {
            ColorRamps.Add(SelectedColorRamp.Clone());
            SelectedColorRamp = ColorRamps[ColorRamps.Count - 1];
            RaisePropertyChanged(nameof(CanRemoveAnyColorRamps));
        }

        private void RemoveColorRamp(ColorRamp ramp)
        {
            if (ramp == SelectedColorRamp)
            {
                var index = ColorRamps.IndexOf(ramp);
                var indexToSelect = index == 0 ? index + 1 : index - 1;
                SelectedColorRamp = ColorRamps[indexToSelect];
            }
            ColorRamps.Remove(ramp);
            RaisePropertyChanged(nameof(CanRemoveAnyColorRamps));
        }
        
        private void CopyToClipboard()
        {
            var allColors = ColorRamps.Select(r => r.Colors.ToArray());
            var bitmapSource = BitmapHelper.CreateBitmapSource(allColors.ToList());
            ClipboardHelper.CopyToClipboard(bitmapSource);
        }

        private bool CanRemoveRamp(ColorRamp ramp)
        {
            return CanRemoveAnyColorRamps;
        }
    }
}