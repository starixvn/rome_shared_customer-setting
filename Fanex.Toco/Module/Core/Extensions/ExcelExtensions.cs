// --------------------------------------------------------------------------
// <copyright file="ExcelExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/19">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Extensions
{
    using System;
    using System.Drawing;
    using System.Web;
    using ExcelUtil.Spreadsheet;

    /// <summary>
    /// Excel Extensions.
    /// </summary>
    public static class ExcelExtensions
    {
        public const int FontWidth = 30;
        public const int FontSize = 160;

        public static readonly Color BackgroundColor = Color.White;
        public static readonly Color HeaderTitleColor = Color.FromArgb(60, 94, 165);
        public static readonly Color EvenRowColor = Color.FromArgb(228, 242, 253);
        public static readonly Color OddRowColor = Color.White;
        public static readonly Color SubTotalRowColor = Color.FromArgb(194, 217, 236);
        public static readonly Color TotalRowColor = Color.FromArgb(244, 244, 190);
        public static readonly Color GrandTotalRowColor = Color.FromArgb(219, 225, 187);
        public static readonly Color GrandTotalSeperatorColor = Color.FromArgb(255, 102, 0);
        public static readonly Color RevenueLicenseeColor = Color.FromArgb(213, 132, 52);

        private const string FontName = "Tahoma";
        private const string NegativeNumber = "#,##0{0}_);[RED](#,##0{1})";
        private const string NormalNumberFormat = "#,##0{0}_);-#,##0{1}";
        private const string PositiveNumberFormat = "[RED][>=0.1]#,##0{0}_);-#,##0{1}";

        /// <summary>
        /// Response Stream for export excel file.
        /// </summary>
        /// <param name="excelFile">The excel file.</param>
        /// <param name="reportName">Name of the report.</param>
        public static void ReponseStream(this ExcelFile excelFile, string reportName)
        {
            var response = HttpContext.Current.Response;
            response.Clear();
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", reportName));
            excelFile.SaveXls(response.OutputStream);
        }

        /// <summary>
        /// Format exist Title Cell.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        public static void FormatTitleCell(this CellStyle cellStyle, Color backgroundColor)
        {
            cellStyle.FormatCellText(backgroundColor, true);
            cellStyle.Font.Color = Color.White;
            cellStyle.VerticalAlignment = VerticalAlignmentStyle.Center;
            cellStyle.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            cellStyle.WrapText = true;
        }

        /// <summary>
        /// Get Style of Title Cell.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <returns>The CellStyle object.</returns>
        public static CellStyle GetCellTitleStyle(Color backgroundColor)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.FormatTitleCell(backgroundColor);
            return cellStyle;
        }

        /// <summary>
        /// Get Style of Text Cell.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <returns>The CellStyle object.</returns>
        public static CellStyle GetCellTextStyle(
            Color backgroundColor,
            bool isBold)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.FormatCellText(backgroundColor, isBold);
            return cellStyle;
        }

        /// <summary>
        /// Get Cell Style with Full Options.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="textAlign">The text align.</param>
        /// <param name="isItalic">If set to <c>true</c> [is italic].</param>
        /// <param name="fontColor">Color of the font.</param>
        /// <returns>The CellStyle object.</returns>
        public static CellStyle GetCellTextStyle(
            Color backgroundColor,
            bool isBold,
            HorizontalAlignmentStyle textAlign,
            bool isItalic,
            Color fontColor)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.FormatCellText(backgroundColor, isBold, textAlign, isItalic, fontColor);
            return cellStyle;
        }
        
        /// <summary>
        /// Get Style of Text Cell.
        /// </summary>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="textAlign">The text align.</param>
        /// <returns>The CellStyle object.</returns>
        public static CellStyle GetCellTextStyle(
            Color backgroundColor,
            bool isBold,
            HorizontalAlignmentStyle textAlign)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.FormatCellText(backgroundColor, isBold, textAlign);
            return cellStyle;
        }

        /// <summary>
        /// Format Exist Cell Text.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        public static void FormatCellText(
            this CellStyle cellStyle,
            Color backgroundColor,
            bool isBold)
        {
            cellStyle.FormatCellText(backgroundColor, isBold, HorizontalAlignmentStyle.General);
        }
        
        /// <summary>
        /// Format Text Cell With Background Color, Bold or not, And Text Align.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="textAlign">The text align.</param>
        public static void FormatCellText(
            this CellStyle cellStyle,
            Color backgroundColor,
            bool isBold,
            HorizontalAlignmentStyle textAlign)
        {
            cellStyle.FormatCellText(backgroundColor, isBold, textAlign, false, Color.Black);
        }

        /// <summary>
        /// Format Cell Text Full Options.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="textAlign">The text align.</param>
        /// <param name="isItalic">If set to <c>true</c> [is italic].</param>
        /// <param name="fontColor">Color of the font.</param>
        public static void FormatCellText(
            this CellStyle cellStyle,
            Color backgroundColor,
            bool isBold,
            HorizontalAlignmentStyle textAlign,
            bool isItalic,
            Color fontColor)
        {
            cellStyle.Borders.SetBorders(MultipleBorders.Outside, Color.Gray, LineStyle.Thin);
            cellStyle.Font.Name = FontName;
            cellStyle.Font.Size = FontSize;

            if (backgroundColor != null)
            {
                cellStyle.FillPattern.SetSolid(backgroundColor);
            }

            cellStyle.VerticalAlignment = VerticalAlignmentStyle.Center;
            cellStyle.HorizontalAlignment = textAlign;

            if (isBold)
            {
                cellStyle.Font.Weight = ExcelFont.BoldWeight;
            }

            if (isItalic)
            {
                cellStyle.Font.Italic = true;
            }

            if (fontColor != null)
            {
                cellStyle.Font.Color = fontColor;
            }
        }

        /// <summary>
        /// Get Style Of Cell Number.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="isPositive">If set to <c>true</c> [is positive].</param>
        /// <param name="isNegative">If set to <c>true</c> [is negative].</param>
        /// <returns>The CellStyle object.</returns>
        public static CellStyle GetCellNumberStyle(
            string symbol,
            Color backgroundColor,
            bool isBold,
            bool isPositive,
            bool isNegative)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.FormatCellNumberStyle(symbol, backgroundColor, isBold, isPositive, isNegative);
            return cellStyle;
        }

        /// <summary>
        /// Get Style Of Cell Number.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="fontColor">Color of the font.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="isPositive">If set to <c>true</c> [is positive].</param>
        /// <param name="isNegative">If set to <c>true</c> [is negative].</param>
        /// <returns>The CellStyle object.</returns>
        public static CellStyle GetCellNumberStyle(
            string symbol,
            Color backgroundColor,
            Color fontColor,
            bool isBold,
            bool isPositive,
            bool isNegative)
        {
            CellStyle cellStyle = new CellStyle();
            cellStyle.FormatCellNumberStyle(symbol, backgroundColor, fontColor, isBold, isPositive, isNegative);
            return cellStyle;
        }

        /// <summary>
        /// Format Cell Which contains number value.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="isPositive">If set to <c>true</c> [is positive].</param>
        /// <param name="isNegative">If set to <c>true</c> [is negative].</param>
        public static void FormatCellNumberStyle(
            this CellStyle cellStyle,
            string symbol,
            Color backgroundColor,
            bool isBold,
            bool isPositive,
            bool isNegative)
        {
            cellStyle.FormatCellNumberStyle(symbol, backgroundColor, Color.Black, isBold, isPositive, isNegative);
        }

        /// <summary>
        /// Format Cell Which contains number value.
        /// </summary>
        /// <param name="cellStyle">The cell style.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="backgroundColor">Color of the background.</param>
        /// <param name="fontColor">Color of the font.</param>
        /// <param name="isBold">If set to <c>true</c> [is bold].</param>
        /// <param name="isPositive">If set to <c>true</c> [is positive].</param>
        /// <param name="isNegative">If set to <c>true</c> [is negative].</param>
        public static void FormatCellNumberStyle(
            this CellStyle cellStyle,
            string symbol,
            Color backgroundColor,
            Color fontColor,
            bool isBold,
            bool isPositive,
            bool isNegative)
        {
            cellStyle.VerticalAlignment = VerticalAlignmentStyle.Center;
            cellStyle.HorizontalAlignment = HorizontalAlignmentStyle.Right;
            cellStyle.Borders.SetBorders(MultipleBorders.Outside, Color.Gray, LineStyle.Thin);

            if (isNegative)
            {
                cellStyle.NumberFormat = string.Format(NegativeNumber, symbol, symbol);
            }
            else if (isPositive)
            {
                cellStyle.NumberFormat = string.Format(PositiveNumberFormat, symbol, symbol);
            }
            else
            {
                cellStyle.NumberFormat = string.Format(NormalNumberFormat, symbol, symbol);
            }

            cellStyle.Font.Name = FontName;
            cellStyle.Font.Size = FontSize;

            if (backgroundColor != null)
            {
                cellStyle.FillPattern.SetSolid(backgroundColor);
            }

            if (fontColor != null)
            {
                cellStyle.Font.Color = fontColor;
            }

            if (isBold)
            {
                cellStyle.Font.Weight = ExcelFont.BoldWeight;
            }
        }

        /// <summary>
        /// Format Empty Row Extension.
        /// </summary>
        /// <param name="excelWorksheet">The work sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startColumn">The start column.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="endColumn">The end column.</param>
        public static void ExportEmptyRow(
            this ExcelWorksheet excelWorksheet,
            int startRow,
            int startColumn,
            ref int currentRow,
            int endColumn)
        {
            var currentColumn = startColumn;
            var currentCellRange = excelWorksheet.Cells.GetSubrangeAbsolute(currentRow, currentColumn, currentRow, endColumn);
            currentCellRange.Merged = true;
            currentCellRange.Value = string.Empty;
            currentCellRange.Style.FormatCellText(Color.White, false);
            currentCellRange.Style.Font.Color = Color.White;
            currentRow++;
        }

        /// <summary>
        /// Export empty report message.
        /// </summary>
        /// <param name="excelWorksheet">Excel Worksheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startColumn">The start column.</param>
        /// <param name="currentRow">The current row.</param>
        /// <param name="endColumn">The end column.</param>
        public static void ExportEmptyReport(
            this ExcelWorksheet excelWorksheet,
            int startRow,
            int startColumn,
            ref int currentRow,
            int endColumn)
        {
            var currentColumn = startColumn;
            var currentCellRange = excelWorksheet.Cells.GetSubrangeAbsolute(currentRow, currentColumn, currentRow, endColumn);
            currentCellRange.Merged = true;
            currentCellRange.Value = ResourceExtension.GetResource("Labels","EmptyReportMessage");
            currentCellRange.Style.FormatCellText(Color.White, true);
            currentCellRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
            currentRow++;
        }
    }
}