using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBank.Clients
{
    /// <summary>
    /// Класс, описывающий логику клиента - физического лица.
    /// </summary>
    public class Individual : Client
    {
        /// <summary>
        /// Создаем клиента - физическое лицо.
        /// </summary>
        /// <param name="fullName">Полное имя (ФИО).</param>
        /// <param name="bankAccount">Расчётный счёт.</param>
        /// <param name="isVip">Является ли клиент привилегированным?</param>
        public Individual(string fullName, BankAccount bankAccount, bool isVip) 
            : base(fullName, ClientTypes.Individual, bankAccount, isVip)
        {
        }

        public Individual(string fullName, ObservableCollection<BankAccount> bankAccounts, bool isVip)
            : base(fullName, ClientTypes.Individual, bankAccounts, isVip)
        {
        }



        protected override void IncreaseAmountWithCapitalization(BankAccount bankAccount)
        {
            var percent = _isVip ? 0.015m : 0.01m;
            bankAccount.Sum += bankAccount.Sum * percent;
        }

        protected override void IncreaseAmountWithoutCapitalization(BankAccount bankAccount)
        {
            var percent = _isVip ? 0.15m : 0.12m;
            bankAccount.Sum += bankAccount.Sum * percent;
        }
    }
}
