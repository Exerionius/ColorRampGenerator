using System;
using System.Diagnostics;
using System.Windows;
using ColorRampGenerator.Tools;
using ColorRampGenerator.ViewModels;
using ColorRampGenerator.Windows;

namespace ColorRampGenerator
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var config = PresetLoader.GetConfig();
            var viewModel = new MainViewModel(config?.HuePresets, config?.SaturationPresets, config?.BrightnessPresets);
            var window = new MainWindow(viewModel);

            try
            {
                new Application().Run(window);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}