using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Converters;

namespace calc.View
{

    public partial class MainWindow : Window
    {
        private bool style;
        private ResourceDictionary BlueStyle;
        private ResourceDictionary PinkStyle;

        public MainWindow()
        {
            InitializeComponent();
            style = false; 
            //defining paths to blue and pink skins
            BlueStyle = new ResourceDictionary();
            BlueStyle.Source = new Uri("../Styles/BlueStyle.xaml", UriKind.Relative);
            PinkStyle = new ResourceDictionary();
            PinkStyle.Source = new Uri("../Styles/PinkStyle.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            //load blue skin
            this.Resources.MergedDictionaries.Add(BlueStyle);
        }

        //on button "style" click change style
        private void ChangeStyle_Click(object sender, RoutedEventArgs e)
        {
            if (style)
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(BlueStyle);
                style = false;
            }
            else
            {
                this.Resources.MergedDictionaries.Clear();
                this.Resources.MergedDictionaries.Add(PinkStyle);
                style = true;
            }
        }
    }

    //Converter for resizing font sizes
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double coef = 1;
            bool fl = false;
            if (parameter != null)
            {
                string p = parameter.ToString();
                if (p == "3" || p == "4")
                    fl = true;
                switch (p)
                {
                    case "0": //FontSize for light blue btns
                        coef = 0.4;
                        break;
                    case "1": //FontSize for blue btns
                        coef = 0.35;
                        break;
                    case "2": //FontSize for =
                        coef = 0.65;
                        break;
                    case "3": //FontSize for first paragrph textbox
                        coef = 0.2;
                        break;
                    case "4": //FontSize for second paragrph textbox
                        coef = 1.5;
                        break;
                    default:
                        break;
                }
            }
            var height = (double)value;
            if (fl) //make textbox font size bigger for more pleasant design
                height += 5;
            return coef * height;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



