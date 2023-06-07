using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesmanCore.DataAccess.Models;

/// <summary>
/// Пользователь
/// </summary>
[EntityTypeConfiguration(typeof(User))]
public class User : AbstractEntity, IEntityTypeConfiguration<User>
{
    #region Свойства

    /// <summary>
    /// Логин
    /// </summary>
    [Required]
    [MaxLength(30)]
    public string Login { get; set; }

    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    [MaxLength(30)]
    public string Password { get; set; }

    #endregion

    #region IEntityTypeConfiguration<User> Реализаторы интерфейса

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(p => new { p.Login }).IsUnique();
    }

    #endregion
}