using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public int rows;
    public int columns;
    public FitType fitType;
    public Vector2 cellSize;
    public Vector2 spacing;

    public bool fitX;
    public bool fitY;

    public bool centerBottomRow;

    public override void CalculateLayoutInputHorizontal()
    { 
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;
            float sqrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRt);
            columns = Mathf.CeilToInt(sqrRt);
        }



        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }

        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }


        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float initialWidth = parentWidth / (float)columns;
        float spacingWidth = (spacing.x / (float)columns) * ((float)columns - 1);
        float paddingWidth = (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellWidth = initialWidth - spacingWidth - paddingWidth;


        float initialHeight = parentHeight / (float)rows;
        float spacingHeight = (spacing.y / (float)rows) * ((float)rows - 1);
        float paddingHeight = (padding.top / (float)rows) - (padding.bottom / (float)rows);
        float cellHeight = initialHeight - spacingHeight - paddingHeight;

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount;
        int rowCount;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            float xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            float yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);

            
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        //Do nothing
    }

    public override void SetLayoutHorizontal()
    {
        //Do nothing
    }

    public override void SetLayoutVertical()
    {
        //Do nothing
    }
}
