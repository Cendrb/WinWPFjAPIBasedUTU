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
        public TimetableWindow()
        {
            InitializeComponent();
        }
        private void setElementAt(int x, int y, UIElement element, Grid grid)
        {
            Grid.SetColumn(element, x);
            Grid.SetRow(element, y);
            grid.Children.Add(element);
        }

        public void ReloadData()
        {
            tabControl.Items.Clear();

            IEnumerable<Timetable> timetables = Static.loader.getTimetablesList().toIEnumerable<Timetable>();
            foreach (Timetable timetable in timetables)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = timetable.getName();

                Grid mainGridView = new Grid();
                mainGridView.Margin = new Thickness(10, 10, 10, 10);

                int totalLessonsPerDay = 9;

                ColumnDefinition dateColumnDef = new ColumnDefinition();
                dateColumnDef.MinWidth = 70;
                mainGridView.ColumnDefinitions.Add(dateColumnDef);

                for (int i = 0; i < totalLessonsPerDay; i++)
                    mainGridView.ColumnDefinitions.Add(new ColumnDefinition());

                int rowIndex = 0;
                foreach (SchoolDay day in timetable.getSchoolDays().toIEnumerable<SchoolDay>())
                {
                    bool[] lessonsInserted = new bool[totalLessonsPerDay];

                    mainGridView.RowDefinitions.Add(new RowDefinition());

                    TextBlock dateBlock = new TextBlock();
                    dateBlock.VerticalAlignment = VerticalAlignment.Center;
                    dateBlock.Text = DateUtil.CZ_WEEK_DATE_FORMAT.format(day.getDate());
                    setElementAt(0, rowIndex, dateBlock, mainGridView);
                    foreach (Lesson lesson in day.getLessons().toIEnumerable<Lesson>())
                    {
                        lessonsInserted[lesson.getSerialNumber() - 1] = true;
                        setElementAt(lesson.getSerialNumber(), rowIndex, new LessonControl(lesson, lesson.getSerialNumber() == 1, rowIndex == 0), mainGridView);
                    }

                    for (int index = 0; index < lessonsInserted.Length; index++)
                        if (!lessonsInserted[index])
                        {
                            Border border = new Border();
                            border.Background = new SolidColorBrush(Colors.White);
                            border.BorderThickness = new Thickness(index == 0 ? 1 : 0, rowIndex == 0 ? 1 : 0, 1, 1);
                            border.BorderBrush = new SolidColorBrush(Colors.Black);
                            setElementAt(index + 1, rowIndex, border, mainGridView);
                        }
                    rowIndex++;
                }

                tabItem.Content = mainGridView;
                tabControl.Items.Add(tabItem);
            }
        }
    }
}
