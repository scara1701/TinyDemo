using System.Collections.ObjectModel;

namespace TinyDemo.SharedLib.Entities
{
    public class Lotto
    {
        public ObservableCollection<int> Numbers { get; set; } = new ObservableCollection<int>();

        public DateTime GeneratedOnTime { get; set; }
    }
}
