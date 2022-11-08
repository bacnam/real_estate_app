using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.Renderers
{
    public class CustomImageButton : ImageButton
    {
        public static readonly BindableProperty TagProperty = BindableProperty.Create("Tag", typeof(long), typeof(CustomImageButton), (long)0);

        public long Tag
        {
            get { return (long)GetValue(TagProperty); }
            set { SetValue(TagProperty, value); }
        }
    }
}
