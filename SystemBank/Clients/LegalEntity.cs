using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBank.Clients
{
    /// <summary>
    /// Класс, описывающий логику клиента - юридического лица.
    /// </summary>
    public class LegalEntity : Client
    {
        /// <summary>
        /// Создаем клиента - юридическое лицо.
        /// </summary>
        /// <param name="fullName">Полное имя (ФИО).</param>
        /// <param name="bankAccount">Расчётный счёт.</param>
        /// <param name="isVip">Является ли клиент привилегированным?</param>
        public LegalEntity(string fullName, BankAccount bankAccount, bool isVip) 
            : base(fullName, ClientTypes.LegalEntity, bankAccount, isVip)
        {
        }


        public LegalEntity(string fullName, ObservableCollection<BankAccount> bankAccounts, bool isVip)
            : base(fullName, ClientTypes.LegalEntity, bankAccounts, isVip)
        {
        }



        protected override void IncreaseAmountWithCapitalization(BankAccount bankAccount)
        {
            var percent = _isVip ? 0.025m : 0.02m;
            bankAccount.Sum += bankAccount.Sum * percent;
        }

        protected override void IncreaseAmountWithoutCapitalization(BankAccount bankAccount)
        {
            var percent = _isVip ? 0.25m : 0.2m;
            bankAccount.Sum += bankAccount.Sum * percent;
        }
    }
}
