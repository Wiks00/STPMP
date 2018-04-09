using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Bitcoin.Curses.ViewModel
{
    public class MapViewModel
    {
        public ObservableCollection<Pin> Pins { get; set; }

        public Command<MapClickedEventArgs> MapClickedCommand =>
            new Command<MapClickedEventArgs>(args =>
            {
                Pins.Add(new Pin
                {
                    Label = $"Pin{Pins.Count}",
                    Position = args.Point
                });
            });
    }
}
