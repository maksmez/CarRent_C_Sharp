using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace CarRent
{
    public class Context : DbContext
    {
        // Имя будущей базы данных можно указать через
        // вызов конструктора базового класса
        public Context() : base("database_db")
        { }

        //Отражение таблиц базы данных на свойства с типом DbSet

        public DbSet<Person> People { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ClCategory> clCategories { get; set; }
        public DbSet<Worker> Workers { get; set; }


    }
}
