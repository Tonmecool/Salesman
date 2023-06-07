using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SalesmanCore.DataAccess.Models;

/// <summary>
/// Файл пользователя
/// </summary>
[EntityTypeConfiguration(typeof(UserFile))]
public class UserFile : AbstractEntity, IEntityTypeConfiguration<UserFile>
{
    #region Свойства

    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// Пользователь
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    /// Имя файла
    /// </summary>
    [Required]
    [MaxLength(512)]
    public string FileName { get; set; }

    /// <summary>
    /// Содержимое файла
    /// </summary>
    [Required]
    public string FileJson { get; set; }

    #endregion

    #region Методы

    public override string ToString()
    {
        return FileName;
    }

    #endregion

    #region IEntityTypeConfiguration<UserFile> Реализаторы интерфейса

    public void Configure(EntityTypeBuilder<UserFile> builder)
    {
        builder.HasIndex(x => new { x.FileName, x.UserId }).IsUnique();
    }

    #endregion
}