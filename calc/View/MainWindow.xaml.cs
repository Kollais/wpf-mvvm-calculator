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

        public MainWindow()
        {
            InitializeComponent();
            style = true; //first style is blue
        }

        //Changes styles after clicking menu button
        private void ChangeStyle_Click(object sender, RoutedEventArgs e)
        {
            if(style)
            {
                mgrid.Style = (Style)this.Resources["pinkGrid"];
                grid.Style = (Style)this.Resources["pinkGrid"];
                menu.Style = (Style)this.Resources["pinkMenu"];
                menuitm.Style = (Style)this.Resources["pinkMenuItm"];
                textbox.Style = (Style)this.Resources["pinkTextbox"];
                btn1.Style = (Style)this.Resources["lightPinkButton"];
                btn2.Style = (Style)this.Resources["lightPinkButton"];
                btn3.Style = (Style)this.Resources["lightPinkButton"];
                btn4.Style = (Style)this.Resources["lightPinkButton"];
                btn5.Style = (Style)this.Resources["lightPinkButton"];
                btn6.Style = (Style)this.Resources["lightPinkButton"];
                btn7.Style = (Style)this.Resources["lightPinkButton"];
                btn8.Style = (Style)this.Resources["lightPinkButton"];
                btn9.Style = (Style)this.Resources["lightPinkButton"];
                btn0.Style = (Style)this.Resources["lightPinkButton"];
                strbtn.Style = (Style)this.Resources["lightPinkButton"];
                CEbtn.Style = (Style)this.Resources["lightPinkButton"];
                Cbtn.Style = (Style)this.Resources["lightPinkButton"];
                pmbtn.Style = (Style)this.Resources["lightPinkButton"];
                rbtn.Style = (Style)this.Resources["lightPinkButton"];
                btndiv.Style = (Style)this.Resources["lightPinkButton"];
                btnperc.Style = (Style)this.Resources["lightPinkButton"];
                btnmult.Style = (Style)this.Resources["lightPinkButton"];
                btnrev.Style = (Style)this.Resources["lightPinkButton"];
                btnmin.Style = (Style)this.Resources["lightPinkButton"];
                btnplus.Style = (Style)this.Resources["lightPinkButton"];
                btncomma.Style = (Style)this.Resources["lightPinkButton"];
                btneq.Style = (Style)this.Resources["pkTallButton"];
                style = false;
            }
            else
            {
                mgrid.Style = (Style)this.Resources["blueGrid"];
                grid.Style = (Style)this.Resources["blueGrid"];
                menu.Style = (Style)this.Resources["blueMenu"];
                menuitm.Style = (Style)this.Resources["blueMenuItm"];
                textbox.Style = (Style)this.Resources["blueTextbox"];
                btn1.Style = (Style)this.Resources["lightBlueButton"];
                btn2.Style = (Style)this.Resources["lightBlueButton"];
                btn3.Style = (Style)this.Resources["lightBlueButton"];
                btn4.Style = (Style)this.Resources["lightBlueButton"];
                btn5.Style = (Style)this.Resources["lightBlueButton"];
                btn6.Style = (Style)this.Resources["lightBlueButton"];
                btn7.Style = (Style)this.Resources["lightBlueButton"];
                btn8.Style = (Style)this.Resources["lightBlueButton"];
                btn9.Style = (Style)this.Resources["lightBlueButton"];
                btn0.Style = (Style)this.Resources["lightBlueButton"];
                strbtn.Style = (Style)this.Resources["blueButton"];
                CEbtn.Style = (Style)this.Resources["blueButton"];
                Cbtn.Style = (Style)this.Resources["blueButton"];
                pmbtn.Style = (Style)this.Resources["blueButton"];
                rbtn.Style = (Style)this.Resources["blueButton"];
                btndiv.Style = (Style)this.Resources["blueButton"];
                btnperc.Style = (Style)this.Resources["blueButton"];
                btnmult.Style = (Style)this.Resources["blueButton"];
                btnrev.Style = (Style)this.Resources["blueButton"];
                btnmin.Style = (Style)this.Resources["blueButton"];
                btnplus.Style = (Style)this.Resources["blueButton"];
                btncomma.Style = (Style)this.Resources["lightBlueButton"];
                btneq.Style = (Style)this.Resources["tallButton"];
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
            if (parameter != null)
            {
                string p = parameter.ToString();
                switch (p)
                {
                    case "1": //FontSize for btns
                        coef = 0.4;
                        break;
                    case "2": //FontSize for =
                        coef = 0.75;
                        break;
                    case "3": //FontSize for first paragrph textbox
                        coef = 0.3;
                        break;
                    case "4": //FontSize for second paragrph textbox
                        coef = 1.9;
                        break;
                    default:
                        break;
                }
            }
            var height = (double)value;
            return coef * height;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



