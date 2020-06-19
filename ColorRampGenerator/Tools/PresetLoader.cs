using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ColorRampGenerator.Models;
using Newtonsoft.Json;

namespace ColorRampGenerator.Tools
{
    public static class PresetLoader
    {
        private const string FileName = "presets.json";
        
        public static PresetConfig GetConfig()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            if (string.IsNullOrWhiteSpace(directoryName)) return null;

            var path = Path.Combine(directoryName, FileName);
            if (!File.Exists(path)) return null;
            
            try
            {
                using (var stream = File.OpenRead(path))
                using (var reader = new StreamReader(stream))
                {
                    var data = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<PresetConfig>(data);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
            return null;
        }
    }
}