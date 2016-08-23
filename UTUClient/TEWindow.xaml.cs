using com.farast.utuapi.data;
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
using System.Windows.Shapes;

namespace UTUClient
{
    /// <summary>
    /// Interaction logic for TEWindow.xaml
    /// </summary>
    public partial class TEWindow : Window
    {
        public TEWindow(TEItem toBeShowed)
        {
            DataContext = new TEModelView(toBeShowed);

            InitializeComponent();

            if (toBeShowed is Exam)
                Title = "Test";
            else
                Title = "Úkol";
        }
    }
}
