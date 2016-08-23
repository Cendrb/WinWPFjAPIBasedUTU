using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace UTUjAPIBased
{
    public enum MessageType {  mine, received }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random random = new Random();
        bool calling = false;

        public MainWindow()
        {
            InitializeComponent();

            using (NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Username=postgres;Password=danjegay;Database=UTU_development"))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection;

                    command.CommandText =
                        "SELECT exams.title, additional_infos.name " +
                        "FROM info_item_bindings INNER JOIN exams ON (info_item_bindings.item_id = exams.id) " +
                        "INNER JOIN additional_infos ON (info_item_bindings.additional_info_id = additional_infos.id) " +
                        "WHERE (info_item_bindings.item_type = 'Exam')";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int x = reader.FieldCount - 1; x > -1; x--)
                            {
                                Console.Write(reader.GetName(x) + ": ");
                                Console.WriteLine(reader[x]);

                            }
                            Console.WriteLine();
                        }
                    }
                }
            }


            Console.Read();
        }

        private async Task CallJohnCena()
        {
            if (!calling)
            {
                calling = true;
                addNewMessage(messageTextView.Text, MessageType.mine);

                messageTextView.Text = "";
                messageSendView.IsEnabled = false;
                messageSendView.Content = "Sending...";

                String returned = await Task.Run<string>(() =>
                {
                    Thread.Sleep(40);
                    string[] messages = new string[] { "U can't see me!", "My time is now!", "Thats exactly what the undertaker told John Cena!" };
                    return messages[random.Next(messages.Length)];
                });

                messageSendView.Content = "Dial John Cena";
                messageSendView.IsEnabled = true;

                addNewMessage(returned, MessageType.received);
                calling = false;
            }
        }

        private void addNewMessage(String message, MessageType messageType)
        {
            TextBlock inputMessageBlock = new TextBlock();
            inputMessageBlock.Text = message;

            switch (messageType)
            {
                case MessageType.mine:
                    inputMessageBlock.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case MessageType.received:
                    inputMessageBlock.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
            mainChatView.Children.Add(inputMessageBlock);
            scrollerView.ScrollToEnd();
        }

        private void messageTextView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CallJohnCena();
        }

        private void messageSendView_Click(object sender, RoutedEventArgs e)
        {
            CallJohnCena();
        }
    }
}
