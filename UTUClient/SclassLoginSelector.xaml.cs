using com.farast.utuapi.data;
using com.farast.utuapi.util;
using java.io;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SclassLoginSelector : Window
    { 

        public SclassLoginSelector()
        {
            DataContext = this;

            InitializeComponent();

            loadPredataAsync();
        }


        private void showProgress(bool showProgress, string message)
        {
            if (showProgress)
            {
                waitRect.Visibility = Visibility.Visible;
                waitTextBlock.Visibility = Visibility.Visible;
                waitTextBlock.Text = message;
            }
            else
            {
                waitRect.Visibility = Visibility.Hidden;
                waitTextBlock.Visibility = Visibility.Hidden;
            }
        }

        private async System.Threading.Tasks.Task loadPredataAsync()
        {
            showProgress(true, "Stahování tříd...");
            try
            {
                await System.Threading.Tasks.Task.Run(() => Static.loader.loadPredata());
                IEnumerable<Sclass> sclasses = Static.loader.getSclasses().toIEnumerable<Sclass>();

                foreach (Sclass sclass in sclasses)
                    sclassesListBox.Items.Add(sclass);

                sclassesListBox.SelectedItem = sclasses.First();
            }
            catch(Exception e)
            {
                MessageBoxResult result = MessageBox.Show("Nepodařilo se připojit k serveru. Zkusit znovu?", "Chyba při stahování tříd", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                    loadPredataAsync();
                else
                    Application.Current.Shutdown();
            }
            finally
            {
                showProgress(false, null);
            }
        }

        private void loadSclassButton_Click(object sender, RoutedEventArgs e)
        {
            if (sclassesListBox.SelectedItem != null)
                launchMainWindow((Sclass)sclassesListBox.SelectedItem);
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            showProgress(true, "Přihlašování..");

            try
            {
                String email = emailText.Text;
                String password = passwordText.Password;

                bool successfulLogin = await System.Threading.Tasks.Task.Run<Boolean>(() => Static.loader.login(email, password));



                if (successfulLogin)
                {
                    launchMainWindow((Sclass)CollectionUtil.findById(Static.loader.getSclasses(), Static.loader.getCurrentUser().getSclassId()));
                }
                else
                {
                    MessageBox.Show("Špatný email nebo heslo", "Chyba při přihlašování", MessageBoxButton.OK, MessageBoxImage.Error);
                    passwordText.Focus();
                    passwordText.Password = String.Empty;
                }
            }
            catch (Exception)
            {
                MessageBoxResult result = MessageBox.Show("Nepodařilo se připojit k serveru. Zkusit znovu?", "Chyba při přihlašování", MessageBoxButton.YesNo, MessageBoxImage.Error, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                    loginButton_Click(sender, e);
                else
                    Application.Current.Shutdown();
            }
            finally
            {
                showProgress(false, null);
            }
        }

        private void launchMainWindow(Sclass sclass)
        {
            MainWindow window = new MainWindow(sclass);
            window.Show();
            Close();
        }
    }
}
