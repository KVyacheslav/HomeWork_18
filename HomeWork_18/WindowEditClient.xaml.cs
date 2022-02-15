using System.Windows;
using System.Windows.Input;
using SystemBank;
using SystemBank.Clients;

namespace HomeWork_18
{
    /// <summary>
    /// Логика взаимодействия для WindowEditClient.xaml
    /// </summary>
    public partial class WindowEditClient : Window
    {
        private Client client;              // Текущий клиент
        private MainWindow window;          // Главное окно
        private ClientTypes firstType;      // Изначальный тип клиента
        private bool firstValueIsVip;       // Изначальное значение ВИП?

        public WindowEditClient(MainWindow window, Client client)
        {
            InitializeComponent();

            this.client = client;
            this.window = window;
            this.tbName.Text = client.FullName;
            this.firstValueIsVip = client.IsVip;
            this.firstType = client.ClientType;
            if (client.ClientType == ClientTypes.Individual)
                cbTypes.SelectedIndex = 0;
            else
                cbTypes.SelectedIndex = 1;

            this.chbIsVip.IsChecked = client.IsVip;
        }

        /// <summary>
        /// Изменить клиента.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClient(object sender, RoutedEventArgs e)
        {
            this.client.IsVip = (bool)chbIsVip.IsChecked;

            var currentTypeStr = cbTypes.SelectedItem.ToString();
            var currentType = currentTypeStr.Equals("Физическое лицо")
                ? ClientTypes.Individual : ClientTypes.LegalEntity;

            if (currentType != this.firstType)
            {
                if (this.firstType == ClientTypes.Individual)
                {
                    Bank.Individuals.RemoveClient(client as Individual);
                    var tmp = new LegalEntity(client.FullName, client.BankAccounts, client.IsVip) { Id = client.Id };
                    tmp.BankCredits = client.BankCredits;
                    Bank.LegalEntities.AddClient(tmp);
                    Bank.UpdateClientDB(client.Id, 2, tmp.IsVip);

                    Bank.StartActionLogs($"Клиент {client.FullName} перевелся в Юр.лицо от {Bank.Date:dd.MM.yyyy}.");
                }
                else
                {
                    Bank.LegalEntities.RemoveClient(client as LegalEntity);
                    var tmp = new Individual(client.FullName, client.BankAccounts, client.IsVip) { Id = client.Id };
                    tmp.BankCredits = client.BankCredits;
                    Bank.Individuals.AddClient(tmp);
                    Bank.UpdateClientDB(client.Id, 1, tmp.IsVip);

                    Bank.StartActionLogs($"Клиент {client.FullName} перевелся в Физ.лицо от {Bank.Date:dd.MM.yyyy}.");
                }
            }

            if (client.IsVip != firstValueIsVip)
            {
                this.window.lvLogs.ScrollIntoView(this.window.lvLogs.Items[this.window.lvLogs.Items.Count - 1]);
            }

            window.tbIsVip.Text = "Привилегированный: " + (client.IsVip ? "Да" : "Нет");
            this.Close();
        }

        private void WindowEditClient_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
