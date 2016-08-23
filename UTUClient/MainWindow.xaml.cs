using com.farast.utuapi.data;
using com.farast.utuapi.util.operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using java.util;
using System.Globalization;
using System.Threading;
using com.farast.utuapi.util;

namespace UTUClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sclass sclass;

        TimetableWindow timetableWindow;
        bool timetableUp;


        public MainWindow(Sclass sclass)
        {
            this.sclass = sclass;

            InitializeComponent();

            Static.loader.getOperationManager().setOperationListener(new OperationListenerClass(statusBarView));

            loadData();
        }

        public async void loadData()
        {
            DataLoader loader = Static.loader;

            await System.Threading.Tasks.Task.Run(() => loader.load(sclass));

            foreach (Event ev in loader.getEventsList().toIEnumerable<Event>())
                addEvent(ev);

            foreach (Exam ex in loader.getExamsList().toIEnumerable<Exam>())
                addItem(ex, 1);

            foreach (com.farast.utuapi.data.Task ta in loader.getTasksList().toIEnumerable<com.farast.utuapi.data.Task>())
                addItem(ta, 2);

            timetableWindow = new TimetableWindow((Timetable)loader.getTimetablesList().get(0));
        }


        private void addEvent(Event even)
        {
            ItemInMainGrid grid = new ItemInMainGrid();
            grid.AddOnClickHandler((e, a) =>
            {
                EventWindow window = new EventWindow(even);
                window.ShowDialog();
            });

            grid.LeftText = even.getTitle();
            grid.RightText = DateUtil.CZ_SHORT_DATE_FORMAT.format(even.getStart());

            eventsColumnView.Children.Add(grid);
        }


        private void addItem(TEItem te, int column)
        {
            ItemInMainGrid grid = new ItemInMainGrid();
            grid.AddOnClickHandler((e, a) =>
            {
                TEWindow window = new TEWindow(te);
                window.ShowDialog();
            });

            grid.LeftText = te.getTitle();
            grid.RightText = DateUtil.CZ_SHORT_DATE_FORMAT.format(te.getDate());

            if (column == 1)
                examsColumnView.Children.Add(grid);
            else
                tasksColumnView.Children.Add(grid);
        }

        class OperationListenerClass : OperationListener
        {
            TextInfo textInfo;
            StatusBar bar;

            public OperationListenerClass(StatusBar statusBar)
            {
                textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
                bar = statusBar;
            }

            public void ended(Operation o, OperationManager om)
            {
                bar.Dispatcher.Invoke(() => redraw(om.getRunningOperations()));
            }

            private void redraw(java.util.List list)
            {
                bar.Items.Clear();
                foreach (Operation operation in list.toIEnumerable<Operation>())
                {
                    bar.Items.Add(operation.getName().FirstCharToUpper() + "...");
                }
            }

            public void started(Operation o, OperationManager om)
            {
                bar.Dispatcher.Invoke(() => redraw(om.getRunningOperations()));
            }
        }

        private void rozvrhToggle(object sender, RoutedEventArgs e)
        {
            if (timetableWindow != null)
                if (timetableUp)
                {
                    timetableWindow.Hide();
                    timetableUp = false;
                }
                else
                {
                    timetableWindow.Show();
                    timetableUp = true;
                }
        }
    }
}
