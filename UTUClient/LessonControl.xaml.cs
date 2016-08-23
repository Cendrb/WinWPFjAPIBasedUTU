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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UTUClient
{
    /// <summary>
    /// Interaction logic for LessonControl.xaml
    /// </summary>
    public partial class LessonControl : UserControl
    {
        public LessonControl(Lesson lesson, bool leftBorder, bool topBorder)
        {
            InitializeComponent();


            BorderThickness = new Thickness(leftBorder ? 1 : 0, topBorder ? 1 : 0, 1, 1);

            if (lesson.isNotNormal())
                Background = new SolidColorBrush(Color.FromRgb(237, 80, 80));
            else
                Background = new SolidColorBrush(Color.FromRgb(237, 159, 80));

            subjectView.Text = lesson.getSubject().getName();
            roomView.Text = lesson.getRoom();
            teacherView.Text = lesson.getTeacher().getAbbr();

        }
    }
}
