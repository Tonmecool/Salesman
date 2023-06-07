using System.ComponentModel.DataAnnotations.Schema;

namespace SalesmanCore.DataAccess.Models;

public abstract class AbstractEntity
{
    #region Свойства

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    #endregion
}