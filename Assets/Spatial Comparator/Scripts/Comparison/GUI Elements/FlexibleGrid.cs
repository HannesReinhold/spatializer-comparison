using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// From the YouTube tutorial "How To Get A Better Grid Layout In Unnity" by Game Dev Guide
/// https://www.youtube.com/watch?v=CGsEJToeXmA
/// </summary>
public class FlexibleGrid : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColums
    }

    public FitType fitType;

    public int rows;

    public int columns;

    public Vector2 cellSize;

    public Vector2 spacing;

    public bool fitX;

    public bool fitY;

    public bool flexibleSpacingX;
    public bool flexibleSpacingY;



    public override void CalculateLayoutInputVertical()
    {

    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Uniform || fitType == FitType.Width || fitType == FitType.Height)
        {
            //fitX = true;
            //fitY = true;
            float sqrt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrt);
            columns = Mathf.CeilToInt(sqrt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColums)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }



        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = 0;
        float cellHeight = 0;

        if (flexibleSpacingX)
        {
            cellWidth = cellSize.x;
            spacing.x = (parentWidth) / (float)(columns - 1) - cellWidth * (float)columns / (float)(columns - 1);
        }
        else
        {
            cellWidth = (parentWidth - spacing.x * (columns - 1) - padding.left - padding.right) / (float)columns;
        }

        if (flexibleSpacingY)
        {
            cellHeight = cellSize.y;
            spacing.y = (parentHeight) / (float)(rows - 1) - cellHeight * (float)rows / (float)(rows - 1);
        }
        else
        {
            cellHeight = (parentHeight - spacing.y * (rows - 1) - padding.top - padding.bottom) / (float)rows;
        }


        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;


        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x + spacing.x) * columnCount + padding.left;
            var yPos = (cellSize.y + spacing.y) * rowCount + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {

    }
}
