using System.Collections.Generic;

namespace SalesmanCore.Helpers;

internal class TreeCalc
{
    #region Поля

    private int?[,] _matrix;

    #endregion

    #region Свойства

    public TreeCalcItem Root { get; set; }

    public int?[,] Matrix
    {
        set => _matrix = (int?[,])value.Clone();
    }

    public int Cost { get; set; }

    public List<TreeCalcItem> listItems { get; set; } = new List<TreeCalcItem>();

    #endregion
}