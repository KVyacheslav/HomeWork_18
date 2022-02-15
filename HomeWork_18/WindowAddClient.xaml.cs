using System;
using System.Windows;
using System.Windows.Input;
using SystemBank;
using SystemBank.Clients;

namespace HomeWork_18
{
    /// <summary>
    /// Логика взаимодействия для WindowAddClient.xaml
    /// </summary>
    public partial class WindowAddClient : Window
    {
        private MainWindow window;          // Главное окно
        private decimal balance;            // Баланс р/с

        public WindowAddClient(MainWindow window)
        {
            InitializeComponent();

            this.window = window;
        }

        /// <summary>
        /// Задает баланс при изменении положения ползунка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.balance = (decimal)Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = this.balance.ToString();
        }

        /// <summary>
        /// Добавляем клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbName.Text))
            {
                MessageBox.Show("Поле ФИО не может быть пустым!", "Ошибка");
                return;
            }

            var id = Bank.CountClients + 1;
            var name = this.tbName.Text;
            var type = this.cbTypes.SelectedItem.ToString().Equals("Физическое лицо")
                ? ClientTypes.Individual : ClientTypes.LegalEntity;
            var isVip = (bool)this.chbIsVip.IsChecked;
            var capitalization = (bool)this.chbCapitalization.IsChecked;
            var date = Bank.Date;
            BankAccount bankAccount = new BankAccount(date, this.balance, capitalization) { ClientId = id };
            Bank.AddBankAccountDB(bankAccount);


            if (type == ClientTypes.Individual)
            {
                var client = new Individual(name, bankAccount, isVip);
                Bank.Individuals.AddClient(client);
                Bank.AddClientDB(id, name, 1, isVip);

                var typeString = "физическое лицо";
                var msg = $"Клиент {client.FullName} зарегистрировался как {typeString} от {Bank.Date:dd.MM.yyyy}.";
                Bank.StartActionLogs(msg);
            }
            else
            {
                var client = new LegalEntity(name, bankAccount, isVip);
                Bank.LegalEntities.AddClient(client);
                Bank.AddClientDB(id, name, 2, isVip);

                var typeString = "юридическое лицо";
                var msg = $"Клиент {client.FullName} зарегистрировался как {typeString} от {Bank.Date:dd.MM.yyyy}.";
                Bank.StartActionLogs(msg);
            }


            this.Close();
        }

        private void WindowAddClient_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
