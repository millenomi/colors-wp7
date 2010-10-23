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
using System.Runtime.Serialization;

namespace Colors
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        public ObservableCollection<ColorEntry> AllColors
        {
            get { return this.State().AllColors; }
        }

        public ObservableCollection<ColorEntry> OpenColors
        {
            get { return App.Instance.State.OpenColors; }
        }

        private void NewColorPicked(object sender, SelectionChangedEventArgs e)
        {
            ListBox newColorList = (ListBox) sender;
            ColorEntry entry = (ColorEntry) newColorList.SelectedItem;

            if (entry != null)
                this.NavigateToNewColorPage(entry);

            newColorList.SelectedIndex = -1;
        }

        private void ExistingColorPicked(object sender, SelectionChangedEventArgs e)
        {
            ListBox openColorList = (ListBox)sender;
            ColorEntry entry = (ColorEntry) openColorList.SelectedItem;

            if (entry != null)
                this.NavigateToEditColorPage(entry);

            openColorList.SelectedIndex = -1;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!this.State().DidAutoOpenThisSession)
            {
                if (this.State().EditingEntry != null)
                {
                    this.NavigateToEditColorPage(this.State().EditingEntry, true);
                }
            }

            this.State().DidAutoOpenThisSession = true;
            this.State().EditingEntry = null;
        }
    }

    public class LowercaseTransformer : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((string)value).ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [DataContract]
    public class ColorEntry : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Color _color;
        
        [DataMember]
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                NotifyPropertyChanged("Color");
            }
        }

        public SolidColorBrush ColorBrush
        {
            get { return new SolidColorBrush(Color); }
        }

        private string _description;

        [DataMember]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }
    }

}