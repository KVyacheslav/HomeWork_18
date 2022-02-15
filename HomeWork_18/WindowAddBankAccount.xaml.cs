using System;
using System.Windows;
using System.Windows.Input;
using SystemBank;
using SystemBank.Clients;

namespace HomeWork_18
{
    /// <summary>
    /// Логика взаимодействия для WindowAddBankAccount.xaml
    /// </summary>
    public partial class WindowAddBankAccount : Window
    {
        private MainWindow window;
        private Client client;
        private decimal balance;

        public WindowAddBankAccount(MainWindow window, Client client)
        {
            InitializeComponent();

            this.window = window;
            this.client = client;
            this.balance = (decimal) Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = balance.ToString();
        }

        /// <summary>
        /// Добавить расчётный счёт.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBankAccount(object sender, RoutedEventArgs e)
        {
            BankAccount ba = 
                new BankAccount(Bank.Date, balance, (bool) chbCapitalization.IsChecked);

            client.AddBankAccount(ba);
            Bank.AddBankAccountDB(ba);

            Bank.StartActionLogs(
                $"Клиент {client.FullName} зарегистрировал р/с №{ba.Number} " +
                $"на сумму {ba.Sum} от {ba.DateOpen:dd.MM.yyyy}."
            );

            this.Close();
        }

        /// <summary>
        /// Отображать значение в форме при изменении.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.balance = (decimal)Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = balance.ToString();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowAddBankAccount_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
