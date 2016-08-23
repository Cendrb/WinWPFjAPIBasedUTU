using com.farast.utuapi.data;
using com.farast.utuapi.util;
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
    /// Interaction logic for TimetableWindow.xaml
    /// </summary>
    public partial class TimetableWindow : Window
    {
        public TimetableWindow(Timetable timetable)
        {
            InitializeComponent();

            int totalLessonsPerDay = 9;
            for (int i = 0; i < totalLessonsPerDay + 1; i++)
                mainGridView.ColumnDefinitions.Add(new ColumnDefinition());
            int rowIndex = 0;
            foreach(SchoolDay day in timetable.getSchoolDays().toIEnumerable<SchoolDay>())
            {
                mainGridView.RowDefinitions.Add(new RowDefinition());

                TextBlock dateBlock = new TextBlock();
                dateBlock.VerticalAlignment = VerticalAlignment.Center;
                dateBlock.Text = DateUtil.CZ_WEEK_DATE_FORMAT.format(day.getDate());
                setLessonAt(0, rowIndex, dateBlock);
                foreach(Lesson lesson in day.getLessons().toIEnumerable<Lesson>())
                {
                    setLessonAt(lesson.getSerialNumber(), rowIndex, new LessonControl(lesson));
                }
                rowIndex++;
            }
        }

        private void setLessonAt(int x, int y, UIElement element)
        {
            Grid.SetColumn(element, x);
            Grid.SetRow(element, y);
            mainGridView.Children.Add(element);
        }
    }
}
