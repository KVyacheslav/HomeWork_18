using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SystemBank.Clients
{
    /// <summary>
    /// Абстрактный класс, описывающий логику клиента банка.
    /// </summary>
    public abstract class Client : IVip, INotifyPropertyChanged
    {
        protected bool _isVip;         // Является ли клиент привилегированным?
        private int countBankAccounts;
        private int countBankCredits;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Id;

        /// <summary>
        /// Полное имя.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Тип клиента.
        /// </summary>
        public ClientTypes ClientType { get; set; }

        /// <summary>
        /// Является ли клиент привилегированным?
        /// </summary>
        public bool IsVip
        {
            get => this._isVip;
            set
            {
                this._isVip = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVip)));
            }
        }

        /// <summary>
        /// Расчётные счета.
        /// </summary>
        public ObservableCollection<BankAccount> BankAccounts { get; set; }

        /// <summary>
        /// Кредиты.
        /// </summary>
        public ObservableCollection<BankCredit> BankCredits { get; set; }

        /// <summary>
        /// Количество р/с
        /// </summary>
        public int CountBankAccounts
        {
            get => this.countBankAccounts;
            set
            {
                this.countBankAccounts = value;
                OnPropertyChanged(nameof(CountBankAccounts));
            }
        }

        /// <summary>
        /// Количество кредитов
        /// </summary>
        public int CountBankCredits
        {
            get => this.countBankCredits;
            set
            {
                this.countBankCredits = value;
                OnPropertyChanged(nameof(CountBankCredits));
            }
        }


        /// <summary>
        /// Создаем клиента.
        /// </summary>
        /// <param name="fullName">Полное имя.</param>
        /// <param name="clientType">Тип клиента.</param>
        /// <param name="bankAccount">Расчётный счёт.</param>
        /// <param name="isVip">Является ли клиент привилегированным?</param>
        protected Client(string fullName, ClientTypes clientType, BankAccount bankAccount, bool isVip)
        {
            this.FullName = fullName;
            this.BankAccounts = new ObservableCollection<BankAccount>();
            this.BankCredits = new ObservableCollection<BankCredit>();
            this.ClientType = clientType;
            this.BankAccounts.Add(bankAccount);
            this._isVip = isVip;
            this.countBankAccounts = this.BankAccounts.Count;
            this.countBankCredits = this.BankCredits.Count;
        }

        /// <summary>
        /// Создаем клиента.
        /// </summary>
        /// <param name="fullName">Полное имя.</param>
        /// <param name="clientType">Тип клиента.</param>
        /// <param name="bankAccounts">Расчётные счёта.</param>
        /// <param name="isVip">Является ли клиент привилегированным?</param>
        protected Client(string fullName, ClientTypes clientType, ObservableCollection<BankAccount> bankAccounts, bool isVip)
        {
            this.FullName = fullName;
            this.BankAccounts = new ObservableCollection<BankAccount>();
            this.BankCredits = new ObservableCollection<BankCredit>();
            this.ClientType = clientType;
            this.BankAccounts = bankAccounts;
            this._isVip = isVip;
            this.CountBankAccounts = this.BankAccounts.Count;
        }

        /// <summary>
        /// Добавить рассчётный счёт.
        /// </summary>
        /// <param name="bankAccount">Расчётный счёт.</param>
        public void AddBankAccount(BankAccount bankAccount)
        {
            this.BankAccounts.Add(bankAccount);
            this.CountBankAccounts = this.BankAccounts.Count;
        }

        /// <summary>
        /// Удалить расчётный счёт.
        /// </summary>
        /// <param name="bankAccount"></param>
        public void RemoveBankAccount(BankAccount bankAccount)
        {
            if (this.BankAccounts.Count > 1)
            {
                var balance = bankAccount.Sum;
                BankAccounts.Remove(bankAccount);
                BankAccounts[0].Sum += balance;
                this.CountBankAccounts = this.BankAccounts.Count;
            }
        }

        /// <summary>
        /// Добавить кредит.
        /// </summary>
        /// <param name="bankCredit"></param>
        public void AddBankCredit(BankCredit bankCredit, decimal sum)
        {
            if (this.CountBankCredits == 1)
                throw new CountCreditsOutOfRangeException();

            this.BankCredits.Add(bankCredit); 
            
            this.CountBankCredits = this.BankCredits.Count;
        }

        /// <summary>
        /// Добавить кредит.
        /// </summary>
        /// <param name="bankCredit"></param>
        public void LoadBankCredit(BankCredit bankCredit)
        {
            this.BankCredits.Add(bankCredit);

            this.CountBankCredits = this.BankCredits.Count;
        }

        /// <summary>
        /// Пополнить баланс.
        /// </summary>
        /// <param name="bankAccount">Рассчётный счёт</param>
        /// <param name="sum"></param>
        public void Put(BankAccount bankAccount, decimal sum)
        {
            bankAccount.Sum += sum;
        }

        /// <summary>
        /// Снять со счёта.
        /// </summary>
        /// <param name="bankAccount">Рассчётный счёт</param>
        /// <param name="sum">Сумма, которую нужно снять со счёта, не превышающую баланс.</param>
        /// <returns>Остаток на счёте.</returns>
        public decimal Withdraw(BankAccount bankAccount, decimal sum)
        {
            if (bankAccount.Sum - sum >= 0)
                return bankAccount.Sum -= sum;
            
            return bankAccount.Sum;
        }

        /// <summary>
        /// Перевести клиенту.
        /// </summary>
        /// <param name="client">Клиент.</param>
        /// <param name="clientBankAccount">Рассчётный счет клиента.</param>
        /// <param name="bankAccount">Рассчётный счёт</param>
        /// <param name="sum">Сумма.</param>
        public void TransferTo(Client client, BankAccount clientBankAccount, BankAccount bankAccount, decimal sum)
        {
            if (bankAccount.Sum - sum >= 0)
            {
                bankAccount.Sum -= sum;
                client.Put(clientBankAccount, sum);
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                Bank.UpdateBalanceDB(bankAccount.Number, bankAccount.Sum);
                Bank.UpdateBalanceDB(clientBankAccount.Number, clientBankAccount.Sum);
                return;
            }
        }

        /// <summary>
        /// Проверка баланса расчётных счетов и кредитов.
        /// </summary>
        /// <param name="currentDate">Текущая дата.</param>
        public void CheckBankAccountsAndCredits(DateTime currentDate)
        {
            foreach (var bankAccount in this.BankAccounts)
            {
                if (bankAccount.Capitalization)
                {
                    this.IncreaseAmountWithCapitalization(bankAccount);
                    Bank.UpdateBalanceDB(bankAccount.Number, bankAccount.Sum);
                }
                else
                {
                    if (currentDate.Month == bankAccount.DateOpen.Month)
                    {
                        this.IncreaseAmountWithoutCapitalization(bankAccount);
                        Bank.UpdateBalanceDB(bankAccount.Number, bankAccount.Sum);
                    }
                }

            }

            for (int i = 0; i < this.BankCredits.Count; i++)
            {
                BankCredits[i].BankAccount.Sum -= BankCredits[i].MonthlyPayment;
                BankCredits[i].PaidOut += BankCredits[i].MonthlyPayment;
                var min = (int)BankCredits[i].Credit - 5;
                var max = (int)BankCredits[i].Credit + 5;

                Bank.UpdateBalanceDB(BankCredits[i].BankAccount.Number, BankCredits[i].BankAccount.Sum);
                Bank.UpdatePaidOutDB(BankCredits[i].Number, BankCredits[i].PaidOut);

                if ((int)BankCredits[i].PaidOut >= min  
                    && (int)BankCredits[i].PaidOut <= max)
                {
                    BankCredits.Remove(BankCredits[i]);
                    i--;
                    this.CountBankCredits = this.BankCredits.Count;

                    Bank.DeleteBankCreditDB(BankCredits[i].Number);
                }
            }
        }

        /// <summary>
        /// Увеличение сумма расчётного счета с капитализацией.
        /// </summary>
        /// <param name="bankAccount">Текущий расчётный счёт.</param>
        protected abstract void IncreaseAmountWithCapitalization(BankAccount bankAccount);

        /// <summary>
        /// Увеличение сумма расчётного счета без капитализации.
        /// </summary>
        /// <param name="bankAccount">Текущий расчётный счёт.</param>
        protected abstract void IncreaseAmountWithoutCapitalization(BankAccount bankAccount);

        public override string ToString()
        {
            return this.FullName;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
