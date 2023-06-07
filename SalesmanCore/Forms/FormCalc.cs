using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesmanCore.Controls;
using SalesmanCore.Helpers;

namespace SalesmanCore.Forms;

public partial class FormCalc : Form
{
    public FormCalc()
    {
        InitializeComponent();
    }

    public FormCalc(int?[,] matrix)
    {
        InitializeComponent();
        Matrix = matrix;
        MatrixLength = Matrix.GetLength(0);

        const int cellSize = 70;
        matrixGrid.GridSize = new Size(MatrixLength + 1, MatrixLength + 1);
        matrixGrid.Size = new Size(matrixGrid.GridSize.Width * cellSize, matrixGrid.GridSize.Height * cellSize);
    }

    #region Свойства

    private int?[,] Matrix { get; }

    private int MatrixLength { get; }

    #endregion

    #region События

    private void buttonRun_Click(object sender, EventArgs e)
    {
        var treeCalc = new TreeCalc();

        //построение матрицы
        var Matrixtemp = (int?[,])Matrix.Clone();
        var minСolumnsInts = new int[MatrixLength];
        var minLinesInts = new int[MatrixLength];
        var MinColumn = int.MaxValue;
        var MinLine = int.MaxValue;
        var CostRoot = 0;

        //нахождение минимума по строкам
        for (var i = 0; i < Matrixtemp.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp.GetLength(1); j++)
            {
                if (Matrixtemp[i, j] < MinLine)
                {
                    MinLine = (int)Matrixtemp[i, j];
                }
            }

            minLinesInts[i] = MinLine;
            MinLine = int.MaxValue;
        }

        //редукция
        for (var i = 0; i < Matrixtemp.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp.GetLength(1); j++)
            {
                Matrixtemp[i, j] -= minLinesInts[i];
            }

            CostRoot += minLinesInts[i];
        }

        //нахождение минимума по столбцам
        for (var i = 0; i < Matrixtemp.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp.GetLength(1); j++)
            {
                if (Matrixtemp[j, i] < MinColumn)
                {
                    MinColumn = (int)Matrixtemp[j, i];
                }
            }

            minСolumnsInts[i] = MinColumn;
            MinColumn = int.MaxValue;
        }

        //редукция
        for (var i = 0; i < Matrixtemp.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp.GetLength(1); j++)
            {
                Matrixtemp[j, i] -= minСolumnsInts[i];
            }

            CostRoot += minСolumnsInts[i];
        }

        var Matrixtemp1 = (int?[,])Matrixtemp.Clone();

        //Вычисление оценок нулевых клеток
        for (var i = 0; i < Matrixtemp1.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp1.GetLength(1); j++)
            {
                MinLine = int.MaxValue;
                MinColumn = int.MaxValue;

                if (Matrixtemp1[i, j] == 0)
                {
                    for (var v = 0; v < Matrixtemp1.GetLength(0); v++)
                    {
                        if (Matrixtemp1[v, j] <= MinLine && v != i)
                        {
                            MinLine = (int)Matrixtemp1[v, j];
                        }
                    }

                    for (var h = 0; h < Matrixtemp1.GetLength(1); h++)
                    {
                        if (Matrixtemp1[i, h] <= MinColumn && h != j)
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
        var Icoord = 0;
        var Jcoord = 0;

        //Выбор нулевой клетки с максимальной оценкой
        for (var i = 0; i < Matrixtemp1.GetLength(0); i++)
        {
            for (var j = 0; j < Matrixtemp1.GetLength(1); j++)
            {
                if (Matrixtemp1[i, j] != null && Matrixtemp1[i, j] >= MaxCost)
                {
                    MaxCost = (int)Matrixtemp1[i, j];
                    Icoord = i;
                    Jcoord = j;
                }
            }
        }

        treeCalc.Matrix = Matrixtemp;
        treeCalc.Cost = CostRoot;

        var treeItem = new TreeCalcItem();
        treeItem.Matrix = Matrixtemp;
        treeItem.Cost = CostRoot;
        treeItem.Icoord = Icoord;
        treeItem.Jcoord = Jcoord;
        treeItem.Path = (int)Matrix[Icoord, Jcoord]; //(int)Matrix[Icoord, Jcoord];
        treeItem.MatrixLength = MatrixLength;
        treeItem.LeftRightFlag = 0;

        treeCalc.Root = treeItem;
        treeCalc.listItems.Add(treeItem);

        // Первое ветвление до цикла - ЛЕВАЯ Включаем маршрут

        var leftItem = new TreeCalcItem();
        leftItem.Matrix = treeItem.Matrix;
        leftItem.LeftRightFlag = 1;
        leftItem.MatrixLength = treeItem.MatrixLength;
        leftItem.Results.Add(treeItem.Icoord);
        leftItem.Results.Add(treeItem.Jcoord);
        leftItem.Cost = treeItem.Cost;

        //Редукция матрицы
        for (var i = 0; i < leftItem.Matrix.GetLength(0); i++)
        {
            leftItem.Matrix[i, treeItem.Jcoord] = null;
        }

        for (var j = 0; j < leftItem.Matrix.GetLength(0); j++)
        {
            leftItem.Matrix[treeItem.Icoord, j] = null;
        }

        leftItem.Matrix[treeItem.Jcoord, treeItem.Icoord] = null;

        // Вычисляем нижнюю границу левой ветви
        for (var i = 0; i < leftItem.MatrixLength; i++)
        {
            minLinesInts[i] = 0;
            minСolumnsInts[i] = 0;
        }

        MinColumn = int.MaxValue;
        MinLine = int.MaxValue;

        //нахождение минимума по строкам
        for (var i = 0; i < leftItem.Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < leftItem.Matrix.GetLength(1); j++)
            {
                if (leftItem.Matrix[i, j] != null && leftItem.Matrix[i, j] < MinLine)
                {
                    MinLine = (int)leftItem.Matrix[i, j];
                }
            }

            minLinesInts[i] = MinLine;
            MinLine = int.MaxValue;
        }

        //редукция
        for (var i = 0; i < leftItem.Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < leftItem.Matrix.GetLength(1); j++)
            {
                if (leftItem.Matrix[i, j] != null)
                    leftItem.Matrix[i, j] -= minLinesInts[i];
            }

            leftItem.Cost += minLinesInts[i];
        }

        //нахождение минимума по столбцам
        for (var i = 0; i < leftItem.Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < leftItem.Matrix.GetLength(1); j++)
            {
                if (leftItem.Matrix[j, i] != null && leftItem.Matrix[j, i] < MinColumn)
                {
                    MinColumn = (int)leftItem.Matrix[j, i];
                }
            }

            minСolumnsInts[i] = MinColumn;
            MinColumn = int.MaxValue;
        }

        //редукция
        for (var i = 0; i < leftItem.Matrix.GetLength(0); i++)
        {
            for (var j = 0; j < leftItem.Matrix.GetLength(1); j++)
            {
                if (leftItem.Matrix[j, i] != null)
                    leftItem.Matrix[j, i] -= minСolumnsInts[i];
            }

            leftItem.Cost += minСolumnsInts[i];
        }

        treeItem.Left = leftItem;
        treeCalc.listItems.Add(leftItem);

        // Первое ветвление до цикла - ПРАВАЯ Исключаем маршрут

        var rightItem = new TreeCalcItem();
        rightItem.Cost = treeItem.Cost + treeItem.Path;
        rightItem.Matrix = treeItem.Matrix;
        rightItem.Matrix[treeItem.Icoord, treeItem.Jcoord] = null;
        rightItem.Matrix[treeItem.Jcoord, treeItem.Icoord] = null;//test
        rightItem.MatrixLength = treeItem.MatrixLength;
        rightItem.LeftRightFlag = 2;

        treeItem.Right = rightItem;
        treeCalc.listItems.Add(rightItem);

        var item = new TreeCalcItem();

        do
        {
            item = treeCalc.listItems.Where(item => item.Left == null && item.Right == null).OrderBy(item => item.Cost).First();

            item.Execute(treeCalc.listItems);

        } 
        while ((item.MatrixLength - 1)*2 > item.Results.Count);

        int sum = 0;

        for (var i = 0; i < item.Left.Results.Count - 1; i += 2)
        {
            if (item.Left.Results[i] != item.Left.Results[i + 1])
            {
                sum += (int)Matrix[item.Left.Results[i], item.Left.Results[i + 1]];
            }
        }

        foreach (var value in item.Left.Results) 
        { 
            textBox1.Text += value; 
            textBox1.Text += "->";
        }

        textBox1.Text += " "; 
        textBox1.Text += sum;


    }

    private void matrixGrid_CellNeeded(object sender, MatrixGrid.CellNeededEventArgs e)
    {
        if (e.Cell.X == 0) // заголовок по оси Y
        {
            e.BackColor = Color.BurlyWood;
            if (e.Cell.Y != 0)
            {
                e.Value = e.Cell.Y.ToString();
            }
        }
        else if (e.Cell.Y == 0) // заголовок по оси X
        {
            e.BackColor = Color.BurlyWood;
            if (e.Cell.X != 0)
            {
                e.Value = e.Cell.X.ToString();
            }
        }
        else
        {
            e.BackColor = Color.Bisque;
            e.Value = Matrix[e.Cell.X - 1, e.Cell.Y - 1].ToString();
        }
    }

    #endregion
}