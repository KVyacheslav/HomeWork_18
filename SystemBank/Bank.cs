using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SystemBank.Clients;
using SystemBank.EF;
using Newtonsoft.Json.Linq;
using System.IO;

namespace SystemBank
{
    public static class Bank
    {
        private static Random rnd;
        private static event Action<Log> ActionLogs;

        public static int CountClients { get; set; }
        public static ObservableCollection<Log> Logs { get; set; }
        public static BankClients<Individual> Individuals { get; set; } // Список клиентов - физических лиц.
        public static BankClients<LegalEntity> LegalEntities { get; set; } // Список клиентов - юридических лиц.
        public static DateTime Date { get; set; } // Текущая дата
        public static bool FinishedGenerate { get; set; } // Закончина ли генерация клиентов
        public static SkContext Context { get; set; }

        static Bank()
        {
            try
            {
                string strCon = LoadConfigurationConnection();
                Context = new SkContext(strCon);
                Date = DateTime.Now;
                Individuals = new BankClients<Individual>();
                LegalEntities = new BankClients<LegalEntity>();
                Logs = new ObservableCollection<Log>();
                rnd = new Random();
                ActionLogs += log => Logs.Add(log);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Для корректного подключения, проверьте файл \"settings_connection.json\"", "Ошибка подключения");
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
            }
        }

        private static string LoadConfigurationConnection()
        {
            var fileName = @"settings_connection.json";
            if (!File.Exists(fileName))
            {
                var js = new JObject();
                var sett = new JObject();
                js["settings_connection"] = sett;
                sett["data_source"] = @"localhost\SQLEXPRESS";
                sett["data_base"] = "SkillboxDB";
                File.WriteAllText("settings_connection.json", js.ToString());
            }

            var settings = JObject.Parse(File.ReadAllText(fileName))["settings_connection"];
            var dataSource = settings["data_source"].ToString();
            var dataBase = settings["data_base"].ToString();

            return $"data source={dataSource};initial catalog={dataBase};integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

        }

        public static void LoadClients()
        {
            try
            {
                Context.Clients.Load();
                Context.BankAccounts.Load();
                Context.BankCredits.Load();
                CountClients = Context.Clients.Count();
                for (int i = 1; i <= 100; i++)
                {
                    var bankAccounts = Context.BankAccounts
                                        .Where(b => b.clientId == i)
                                        .ToList();
                    ObservableCollection<BankAccount> ba = new ObservableCollection<BankAccount>();
                    foreach (var item in bankAccounts)
                    {
                        if (item != null)
                        {
                            ba.Add(new BankAccount(item.number,
                                item.dateOpen,
                                item.balance,
                                item.capitalization,
                                item.numberTimesIncreased)
                            { ClientId = item.clientId });
                        }
                    }

                    var clientDB = Context.Clients.Single(c => c.id == i);
                    Client client = new Individual(string.Empty, new ObservableCollection<BankAccount>(), false);

                    if (clientDB.typeId == 1)
                        client = new Individual(clientDB.fullName, ba, clientDB.privileged) { Id = i };
                    else
                        client = new LegalEntity(clientDB.fullName, ba, clientDB.privileged) { Id = i };

                    var bankCredits = Context.BankCredits
                                        .Where(b => b.clientId == i)
                                        .ToList();
                    ObservableCollection<BankCredit> bc = new ObservableCollection<BankCredit>();

                    foreach (var item in bankCredits)
                    {
                        if (item != null)
                        {
                            var bankAccount = GetBankAccount(ba, item.numberBankAccount);
                            bc.Add(new BankCredit(item.number,
                                                  item.sumCredit,
                                                  item.dateOpen,
                                                  item.creditTerm / 12,
                                                  item.paidOut,
                                                  bankAccount)
                            { ClientId = item.clientId });
                        }
                    }

                    client.BankCredits = bc;

                    if (client.ClientType == ClientTypes.Individual)
                        Individuals.Clients.Add(client as Individual);
                    else
                        LegalEntities.Clients.Add(client as LegalEntity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Для корректного подключения, проверьте файл \"settings_connection.json\"", "Ошибка подключения");
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.HelpLink);
                MessageBox.Show(ex.Source);
                MessageBox.Show(ex.StackTrace);
                return;
            }


            FinishedGenerate = true;
        }

        private static BankAccount GetBankAccount(ObservableCollection<BankAccount> bankAccounts, string number)
        {
            return bankAccounts.FirstOrDefault(c => c.Number == number);
        }

        public static void SaveChanges()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.HelpLink);
                MessageBox.Show(ex.Source);
                MessageBox.Show(ex.StackTrace);
            }
        }

        public static void UpdateBalanceDB(string number, decimal balance)
        {
            var ba = Context.BankAccounts
                .Single(b => b.number == number);
            ba.balance = balance;
            ba.numberTimesIncreased++;
            SaveChanges();
        }

        public static void UpdatePaidOutDB(string number, decimal paidOut)
        {
            Context.BankCredits
                .First(ba => ba.number == number).paidOut = paidOut;
            SaveChanges();
        }

        public static void DeleteBankCreditDB(string number)
        {
            var bc = Context.BankCredits.First(b => b.number == number);
            Context.BankCredits.Remove(bc);
            SaveChanges();
        }

        public static void DeleteBankAccountDB(string number)
        {
            var ba = Context.BankAccounts.First(b => b.number == number);
            Context.BankAccounts.Remove(ba);
            SaveChanges();
        }

        public static void AddBankAccountDB(BankAccount bankAccount)
        {
            Context.BankAccounts.Add(new BankAccounts()
            {
                number = bankAccount.Number,
                dateOpen = bankAccount.DateOpen,
                balance = bankAccount.Sum,
                capitalization = bankAccount.Capitalization,
                numberTimesIncreased = 0,
                clientId = bankAccount.ClientId
            });
            SaveChanges();
        }

        public static void AddBankCreditDB(BankCredit bankCredit)
        {
            Context.BankCredits.Add(new BankCredits()
            {
                number = bankCredit.Number,
                dateOpen = bankCredit.DateOpen,
                creditTerm = bankCredit.CountYears * 12,
                numberBankAccount = bankCredit.BankAccount.Number,
                paidOut = bankCredit.PaidOut,
                sumCredit = bankCredit.Credit,
                clientId = bankCredit.ClientId
            });
            SaveChanges();
        }

        public static void AddClientDB(int i, string name, int typeInt, bool isVip)
        {
            Context.Clients.Add(new EF.Clients()
            {
                id = i,
                fullName = name,
                typeId = typeInt,
                privileged = isVip
            });
            SaveChanges();
            CountClients = Context.Clients.Count();
        }

        public static void UpdateClientDB(int id, int typeId, bool privileged)
        {
            var client = Context.Clients.First(c => c.id == id);
            client.typeId = typeId;
            client.privileged = privileged;
            SaveChanges();
        }

        #region Generate

        /// <summary>
        /// Генерация клиентов.
        /// </summary>
        public static void GenerateClients()
        {
            for (int i = 1; i <= 10_000; i++)
            {
                var name = GetRandomFullName();
                var typeInt = rnd.Next(1, 3);
                var type = typeInt == 1
                    ? ClientTypes.Individual
                    : ClientTypes.LegalEntity;
                var isVip = rnd.Next(5) == 0;
                var bankAccount = GetGenerateBankAccount();
                bankAccount.ClientId = i;
                Client client;



                if (type == ClientTypes.Individual)
                {
                    client = new Individual(name, bankAccount, isVip);
                    Individuals.AddClient(client as Individual);
                }
                else
                {
                    client = new LegalEntity(name, bankAccount, isVip);
                    LegalEntities.AddClient(client as LegalEntity);
                }

                client.Id = i;

                AddClientDB(i, name, typeInt, isVip);
                AddBankAccountDB(bankAccount);

                var typeString = client.ClientType == ClientTypes.Individual ? "физическое лицо" : "юридическое лицо";
                var msg = $"Клиент {client.FullName} зарегистрировался как {typeString} от {Date:dd.MM.yyyy}.";
                

                ActionLogs?.Invoke(new Log(msg));

                if (rnd.Next(5) == 0)
                {
                    var ba = GetGenerateBankAccount();
                    client.AddBankAccount(ba);
                    AddBankAccountDB(ba);
                }

                if (rnd.Next(3) != 2)
                    client.AddBankCredit(GetGenerateBankCredit(client, out decimal sum), sum);

            }

            FinishedGenerate = true;
        }

        /// <summary>
        /// Сгенерировать клиенту кредит.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="sum">Сумма взятая в кредит.</param>
        /// <returns></returns>
        private static BankCredit GetGenerateBankCredit(Client client, out decimal sum)
        {
            var ba = client.BankAccounts[0];
            sum = rnd.Next(500, (int)ba.Sum);
            var credit = sum + sum * (decimal)(client.IsVip ? 0.2 : 0.3);
            var bk = new BankCredit(credit, Date, 3, ba, sum);
            AddBankCreditDB(bk);

            return bk;
        }

        /// <summary>
        /// Сгенерировать расчётные счета.
        /// </summary>
        /// <returns></returns>
        private static BankAccount GetGenerateBankAccount()
        {
            return new BankAccount(
                DateTime.Now,
                rnd.Next(1000, 10001),
                rnd.Next(2) == 0);
        }

        /// <summary>
        /// Сгенерировать полное имя.
        /// </summary>
        /// <returns>Полное имя.</returns>
        private static string GetRandomFullName()
        {
            var data = @"Бичурин Алексей Платонович
Царёва Ева Якововна
Бочарова Оксана Виталиевна
Грефа Софья Филипповна
Гусева Роза Мефодиевна
Дёмшина Арина Елизаровна
Архипов Артем Ираклиевич
Цветкова Людмила Павеловна
Ямзин Леонид Филимонович
Плюхина Нина Емельяновна
Григорьева Инна Василиевна
Анисимова Полина Борисовна
Лешев Виктор Богданович
Бессуднов Станислав Евстафиевич
Топоров Анатолий Самсонович
Васильева Владлена Серафимовна
Бикулов Аскольд Капитонович
Головкина Алина Федоровна
Насонова Лада Мироновна
Островерха Ульяна Станиславовна
Шамякин Терентий Тихонович
Кидирбаева Валентина Анатолиевна
Булыгина Диана Никитевна
Беломестнов Фока Никанорович
Гайдученко Тимофей Зиновиевич
Казьмин Агафон Семенович
Соломахина Юлия Михеевна
Хорошилова Ярослава Романовна
Волынкина Валерия Леонидовна
Садовничий Алиса Петровна
Чичерин Кондратий Титович
Рыкова Зинаида Олеговна
Крутой Наталия Брониславовна
Есипов Герман Касьянович
Лачков Аркадий Назарович
Яикбаева Инга Фомевна
Семенов Иосиф Кондратиевич
Курушин Прокл Валериевич
Денисова Анна Кузьмевна
Рыжанов Богдан Моисеевич
Канадина Светлана Данииловна
Никаева Изольда Юлиевна
Кочинян Никон Феликсович
Бурмакина Элеонора Георгиевна
Висенина Ульяна Владиленовна
Валиев Вениамин Яковович
Ярилов Зиновий Епифанович
Гибазов Эдуард Сергеевич
Клокова Антонина Серафимовна
Волобуева Раиса Семеновна
Бабышев Гавриил Феликсович
Задков Филипп Миронович
Варфоломеева Варвара Феликсовна
Селиванов Герман Карлович
Томсин Аскольд Эрнестович
Енотова Евгения Юлиевна
Мандрыкин Владислав Богданович
Голубцов Аскольд Давидович
Рыжов Прокл Всеволодович
Кораблин Иннокентий Наумович
Черенчикова Светлана Несторовна
Арсеньева Римма Виталиевна
Громыко Лука Елизарович
Архаткин Леонид Евграфович
Дубинина Арина Леонидовна
Дуркина Надежда Фомевна
Шкиряк Аким Ипполитович
Солдатов Петр Вячеславович
Иванников Ефрем Григориевич
Липова Пелагея Казимировна
Янкин Модест Ираклиевич
Машлыкин Станислав Евгениевич
Погребной Прохор Сигизмундович
Кетов Лавр Иосифович
Степихова Мирослава Казимировна
Кучава Всеволод Касьянович
Кустов Вадим Назарович
Борзилов Макар Миронович
Блатова Светлана Олеговна
Лапотников Семён Мартьянович
Аронова Клара Никитевна
Кудяшова Розалия Никитевна
Киприянов Антип Вячеславович
Ягунова Дарья Геннадиевна
Ручкина Варвара Юлиевна
Малинина Ярослава Ростиславовна
Завражный Кондратий Эмилевич
Крымов Андрон Матвеевич
Голубов Тимур Андриянович
Клоков Нестор Кондратиевич
Гоминова Роза Евгениевна
Петухов Ефрем Савелиевич
Вьялицына Виктория Несторовна
Игнатенко Эвелина Иосифовна
Фернандес Аким Савелиевич
Блатова Эвелина Якововна
Любимцев Ярослав Мирославович
Уголева ﻿Агата Петровна
Саянов Виталий Адрианович
Якунова Зоя Леонидовна
Дорохова ﻿Агата Германовна
Журавлёв Евгений Игоревич
Цветков Игнатий Наумович
Дагина Эвелина Мироновна
Гика Алла Яновна
Дубровский Роман Александрович
Касатый Агафья Иларионовна
Березовский Артём Игнатиевич
Чекмарёв Никита Куприянович
Смотров Георгий Демьянович
Кошелева Элеонора Антониновна
Калашников Борислав Кондратиевич
Травкина Ангелина Леонидовна
Кочубей Роза Александровна
Шурдукова Антонина Родионовна
Голованова Полина Всеволодовна
Карчагина Каролина Святославовна
Золотухин Михей Гордеевич
Прокашева Анисья Павеловна
Кулешов Роман Георгиевич
Воронцов Яков Моисеевич
Рунов Марк Ульянович
Солодский Елизар Адамович
Васенин Фока Ерофеевич
Кидина Роза Данииловна
Кологреев Валерий Андреевич
Козариса Василиса Тимуровна
Тупицын Вацлав Святославович
Жариков Петр Модестович
Якубович Платон Иосифович
Нуряев Владилен Миронович
Бебчука Виктория Тимофеевна
Шамякин Гавриил Мартьянович
Елешев Аким Ираклиевич
Лагошина Каролина Яновна
Кантонистов Николай Куприянович
Мусин Михей Анатолиевич
Ямковой Анфиса Данииловна
Ажищенкова Инга Тимуровна
Окрокверцхов Иннокентий Яковович
Зууфина Ника Виталиевна
Янин Алексей Кондратович
Мацовкин Филипп Эдуардович
Камбарова Лада Марковна
Тимофеева Софья Мефодиевна
Зёмина Кристина Андрияновна
Сагунова Яна Яновна
Распутина Мария Геннадиевна
Стегнова Рада Трофимовна
Фанина Жанна Родионовна
Мосякова Инга Иосифовна
Шамякин Артемий Маркович
Драгомирова ﻿Агата Ефимовна
Ельченко Валерий Пахомович
Добролюбов Порфирий Севастьянович
Кругликова Елена Ростиславовна
Бабышев Осип Богданович
Дудника Ангелина Евгениевна
Бондарчука Агния Трофимовна
Кобелева Таисия Данииловна
Сапалёва Всеслава Игнатиевна
Дуболазов Всеволод Титович
Яшвили Агап Евграфович
Коллерова Анисья Василиевна
Палюлин Юрий Сигизмундович
Цыгвинцев Дмитрий Филимонович
Большаков Трофим Демьянович
Яндарбиева Софья Алексеевна
Валуев Лаврентий Адамович
Колотушкина Наталья Вячеславовна
Бруевича Жанна Казимировна
Масмеха Кира Несторовна
Меншикова Кира Василиевна
Шаршин Мстислав Сократович
Малафеев Харитон Кириллович
Завражина Виктория Брониславовна
Заболотный Самуил Семенович
Яскунов Фадей Макарович
Зимин Виссарион Григориевич
Крестьянинов Евгений Давидович
Земляков Клавдий Ростиславович
Соловьёв Андриян Прохорович
Якурин Илья Гаврилевич
Мещерякова Алина Кузьмевна
Катаева Бронислава Ираклиевна
Грязнов ﻿Август Матвеевич
Никулина Доминика Карповна
Марьин Геннадий Капитонович
Зюлёва Инга Игоревна
Лобана Ярослава Несторовна
Счастливцева Василиса Всеволодовна
Набоко Егор Капитонович
Аничков Всеслав Капитонович
Костюк Венедикт Святославович
Синдеева Ираида Яновна
Москвитина Ефросинья Феликсовна
Арданкина Оксана Мироновна
Казанцева Анастасия Игоревна
Якутин Виталий Брониславович
Безрукова Альбина Владленовна"
                .Split('\n');
            return data[rnd.Next(data.Length)];
        }

        #endregion


        /// <summary>
        /// Добавление лога в список.
        /// </summary>
        /// <param name="text">Текст лога.</param>
        public static void StartActionLogs(string text)
        {
            Log log = new Log(text);
            ActionLogs?.Invoke(log);
        }
    }
}
