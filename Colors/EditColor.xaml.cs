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

namespace Colors
{
    public partial class EditColor : PhoneApplicationPage
    {
        public EditColor()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty EntryProperty = DependencyProperty.Register(
    "Entry", typeof(ColorEntry), typeof(EditColor), new PropertyMetadata(null));
        public ColorEntry Entry
        {
            get { return (ColorEntry)this.GetValue(EntryProperty); }
            set { this.SetValue(EntryProperty, value); }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string ident;
            if (NavigationContext.QueryString.TryGetValue("id", out ident))
            {
                int i = int.Parse(ident);
                this.Entry = this.State().OpenColors[i];
            }

            string backHint; bool shouldShowBackHint = false;
            if (NavigationContext.QueryString.TryGetValue("shouldShowBackHint", out backHint))
                shouldShowBackHint = (backHint == "true");

            if (shouldShowBackHint)
            {
                Storyboard ani = (Storyboard) Resources["ShowHideBackButtonHint"];
                ani.Begin();
            } 
            else
                this.State().EditingEntry = this.Entry;
        }


    }
}