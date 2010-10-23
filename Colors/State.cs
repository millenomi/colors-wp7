using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.IO.IsolatedStorage;

namespace Colors
{
    public class State : INotifyPropertyChanged
    {
        public State()
        {
            _allColors.Add(new ColorEntry() { Color = System.Windows.Media.Colors.Magenta, Description = "Magenta" });
            _allColors.Add(new ColorEntry() { Color = System.Windows.Media.Colors.LightGray, Description = "Light Gray" });
            _allColors.Add(new ColorEntry() { Color = System.Windows.Media.Colors.Orange, Description = "Orange" });
            _allColors.Add(new ColorEntry() { Color = System.Windows.Media.Colors.Blue, Description = "Blue" });
            _allColors.Add(new ColorEntry() { Color = System.Windows.Media.Colors.Green, Description = "Green" });
        }

        public bool DidAutoOpenThisSession { get; set; }

        private ObservableCollection<ColorEntry> _openColors = new ObservableCollection<ColorEntry>();
        public ObservableCollection<ColorEntry> OpenColors
        {
            get { return _openColors; }
        }

        private ObservableCollection<ColorEntry> _allColors = new ObservableCollection<ColorEntry>();
        public ObservableCollection<ColorEntry> AllColors
        {
            get { return _allColors; }
        }

        public ColorEntry _editingEntry = null;
        public ColorEntry EditingEntry
        {
            get { return _editingEntry; }
            set
            {
                _editingEntry = value;
                NotifyPropertyChanged("EditingEntry");
            }
        }

        public void Save()
        {
            List<ColorEntry> snapshotOfOpenColors = new List<ColorEntry>();
            foreach (ColorEntry e in OpenColors)
                snapshotOfOpenColors.Add(e);

            IsolatedStorageSettings.ApplicationSettings["OpenColors"] = snapshotOfOpenColors;
            int i = -1;

            if (EditingEntry != null)
                i = snapshotOfOpenColors.IndexOf(EditingEntry);

            IsolatedStorageSettings.ApplicationSettings["EditingEntryIndex"] = i;
        }

        public void Load()
        {
            OpenColors.Clear();
            List<ColorEntry> colorsToOpen = null;
            try
            {
                colorsToOpen = (IsolatedStorageSettings.ApplicationSettings["OpenColors"] as List<ColorEntry>);
            }
            catch (KeyNotFoundException) { }

            if (colorsToOpen != null)
            {
                foreach (ColorEntry e in colorsToOpen)
                    OpenColors.Add(e);

                int? i = IsolatedStorageSettings.ApplicationSettings["EditingEntryIndex"] as int?;
                if (i.HasValue && i.Value != -1 && i.Value >= 0 && i.Value < OpenColors.Count)
                {
                    EditingEntry = OpenColors[i.Value];
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
