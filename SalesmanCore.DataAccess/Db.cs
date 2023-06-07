using Microsoft.EntityFrameworkCore;
using SalesmanCore.DataAccess.Models;

namespace SalesmanCore.DataAccess;

public class Db : DbContext
{
    #region Свойства

    /// <summary>
    /// Пользователь
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Файл пользователя
    /// </summary>
    public DbSet<UserFile> UserFiles { get; set; }

    #endregion

    #region Методы

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Salesman;Integrated Security=True");
    }

    #endregion
}