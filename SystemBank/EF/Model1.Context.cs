﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SystemBank.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SkContext : DbContext
    {
        private string conStr = "";
        public SkContext(string conStr)
            : base($"metadata=res://*/EF.Model1.csdl|res://*/EF.Model1.ssdl|res://*/EF.Model1.msl;provider=System.Data.SqlClient;provider connection string=\"{conStr}\"")
            //: base("name=SkConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BankAccounts> BankAccounts { get; set; }
        public virtual DbSet<BankCredits> BankCredits { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Types> Types { get; set; }
    }
}
