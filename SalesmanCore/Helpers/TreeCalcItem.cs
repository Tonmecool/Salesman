using System.Collections.Generic;
using System.Linq;

namespace SalesmanCore.Helpers;

internal class TreeCalcItem
{
    #region Поля

    private int?[,] _matrix;

    #endregion

    #region Свойства

    public TreeCalcItem Left { get; set; }

    public TreeCalcItem Right { get; set; }

    public int?[,] Matrix
    {
        get => _matrix;
        set => _matrix = (int?[,])value.Clone();
    }

    public int Cost { get; set; }

    public List<int> Results { get; set; } = new List<int>();

    public int Icoord { get; set; }

    public int Jcoord { get; set; }

    public int Path { get; set; }

    public int MatrixLength { get; set; }

    /// <summary>
    /// Признак - левая = 1 или правая = 2 ветка, значение 0 - корень дерева
    /// </summary>
    public int LeftRightFlag { get; set; }

    #endregion

    public void Execute(List<TreeCalcItem> list)
    {
        var minСolumnsInts = new int[MatrixLength];
        var minLinesInts = new int[MatrixLength];
        var MinColumn = int.MaxValue;
        var MinLine = int.MaxValue;
        var localCost = 0;

        for (var i = 0; i < MatrixLength; i++)
        {
            minLinesInts[i] = 0;
            minСolumnsInts[i] = 0;

        }

        if (LeftRightFlag == 2)
        {
            //нахождение минимума по строкам
            for (var i = 0; i < Matrix.GetLength(0); i++)
            {
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != null && Matrix[i, j] < MinLine)
                    {
                        MinLine = (int)Matrix[i, j];
                    }
                }

                minLinesInts[i] = MinLine;
                MinLine = int.MaxValue;
            }

            //редукция
            for (var i = 0; i < Matrix.GetLength(0); i++)
            {
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] != null)
                        Matrix[i, j] -= minLinesInts[i];
                }

                localCost += minLinesInts[i];
            }

            //нахождение минимума по столбцам
            for (var i = 0; i < Matrix.GetLength(0); i++)
            {
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[j, i] != null && Matrix[j, i] < MinColumn)
                    {
                        MinColumn = (int)Matrix[j, i];
                    }
                }

                minСolumnsInts[i] = MinColumn;
                MinColumn = int.MaxValue;
            }

            //редукция
            for (var i = 0; i < Matrix.GetLength(0); i++)
            {
                for (var j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[j, i] != null)
                        Matrix[j, i] -= minСolumnsInts[i];
                }

                localCost += minСolumnsInts[i];
            }

        }

        var Matrixtemp1 = (int?[,])Matrix.Clone();

        //Вычисление оценок нулевых клеток
        for (var i = 0; i < Matrixtemp1.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp1.GetLength(1); j++)
            {
                MinLine = int.MaxValue;
                MinColumn = int.MaxValue;

                if ((Matrixtemp1[i, j] != null) && (Matrixtemp1[i, j] == 0))
                {
                    for (var v = 0; v < Matrixtemp1.GetLength(0); v++)
                    {
                        if ((Matrixtemp1[v, j] != null) && (Matrixtemp1[v, j] < MinLine) && (v != i))
                        {
                            MinLine = (int)Matrixtemp1[v, j];
                        }
                    }

                    for (var h = 0; h < Matrixtemp1.GetLength(1); h++)
                    {
                        if ((Matrixtemp1[i, h] != null) && (Matrixtemp1[i, h] < MinColumn) && (h != j))
                        {
                            MinColumn = (int)Matrixtemp1[i, h];
                        }
                    }

                    Matrixtemp1[i, j] = MinColumn + MinLine;
                }
                else
                {
                    Matrixtemp1[i, j] = null;
                }
            }
        }

        var MaxCost = int.MinValue;

        //Выбор нулевой клетки с максимальной оценкой
        for (var i = 0; i < Matrixtemp1.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp1.GetLength(1); j++)
            {
                if (Matrixtemp1[i, j] != null && Matrixtemp1[i, j] > MaxCost)
                {
                    MaxCost = (int)Matrixtemp1[i, j];
                    Icoord = i;
                    Jcoord = j;
                }
            }
        }

        Path = MaxCost;

        // Ветвление - ЛЕВАЯ Включаем маршрут

        var leftItem = new TreeCalcItem();
        leftItem.Matrix = Matrix;
        leftItem.LeftRightFlag = 1;
        leftItem.MatrixLength = MatrixLength;
        leftItem.Results.AddRange(Results);
        leftItem.Results.Add(Icoord);
        leftItem.Results.Add(Jcoord);
        leftItem.Cost = Cost + localCost;

        //Редукция матрицы
        for (var i = 0; i < leftItem.Matrix.GetLength(0); i++)
        {
            leftItem.Matrix[i, Jcoord] = null;
        }

        for (var j = 0; j < leftItem.Matrix.GetLength(0); j++)
        {
            leftItem.Matrix[Icoord, j] = null;
        }

        leftItem.Matrix[Jcoord, Icoord] = null;

        Left = leftItem;
        list.Add(leftItem);

        // Ветвление - ПРАВАЯ Исключаем маршрут

        var rightItem = new TreeCalcItem();
        rightItem.Cost = Cost + Path;
        rightItem.Matrix = Matrix;
        rightItem.Matrix[Icoord, Jcoord] = null;
        rightItem.Matrix[Jcoord, Icoord] = null;//test
        rightItem.MatrixLength = MatrixLength;
        rightItem.LeftRightFlag = 2;
        rightItem.Results.AddRange(Results);

        Right = rightItem;
        list.Add(rightItem);
    }
}