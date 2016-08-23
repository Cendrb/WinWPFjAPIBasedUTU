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
        public LessonControl(Lesson lesson)
        {
            InitializeComponent();

            if (lesson.isNotNormal())
                Background = new SolidColorBrush(Colors.Red);
            else
                Background = new SolidColorBrush(Colors.Yellow);

            subjectView.Text = lesson.getSubject().getName();
            roomView.Text = lesson.getRoom();
            teacherView.Text = lesson.getTeacher().getAbbr();

        }
    }
}
