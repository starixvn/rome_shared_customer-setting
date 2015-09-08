// --------------------------------------------------------------------------
// <copyright file="StatementReportExportExcelService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Services.StatementReport
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Fanex.Module.Statement.Configurations;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models;
    using Fanex.Module.Statement.Models.StatementReport;
    using Fanex.Module.Statement.Resources;
    using NPOI.HSSF.UserModel;
    using NPOI.HSSF.Util;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Statement Report Export Excel Service.
    /// </summary>
    public class StatementReportExportExcelService
    {
        /// <summary>
        /// Exports the member statement.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="data">The statement report.</param>
        /// <returns>Memory Stream.</returns>
        public MemoryStream ExportMemberStatement(MemberStatementCriteria criteria, StatementReportModel data)
        {
            var result = new MemoryStream();
            var sheetName = Labels.Statement;
            HSSFWorkbook workbook = new HSSFWorkbook();
            var worksheet = workbook.CreateSheet(sheetName);

            ExportHeaderMemberStatement(workbook, worksheet, criteria);
            ExportBodyMemberStatement(workbook, worksheet, criteria, data);

            workbook.Write(result);
            return result;
        }

        /// <summary>
        /// Exports the SMA statement.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="data">The statement report.</param>
        /// <returns>Memory Stream.</returns>
        public MemoryStream ExportSMAStatement(SMAStatementCriteria criteria, StatementReportModel data)
        {
            var result = new MemoryStream();
            var sheetName = Labels.Statement;
            HSSFWorkbook workbook = new HSSFWorkbook();
            var worksheet = workbook.CreateSheet(sheetName);

            ExportHeaderSMAStatement(workbook, worksheet, criteria);
            ExportBodySMAStatement(workbook, worksheet, criteria, data);

            workbook.Write(result);
            return result;
        }

        /// <summary>
        /// Exports the transfer details.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="data">The statement report.</param>
        /// <returns>Memory Stream.</returns>
        public MemoryStream ExportTransferDetails(TransferDetailsCriteria criteria, StatementReportModel data)
        {
            var result = new MemoryStream();
            var sheetName = Labels.Statement;
            HSSFWorkbook workbook = new HSSFWorkbook();
            var worksheet = workbook.CreateSheet(sheetName);

            ExportHeaderTransferDetails(workbook, worksheet, criteria);
            ExportBodyTransferDetails(workbook, worksheet, criteria, data);

            workbook.Write(result);
            return result;
        }

        /// <summary>
        /// Gets the title style.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <returns>The title style.</returns>
        private ICellStyle TitleStyle(HSSFWorkbook workbook)
        {
            var titleStyle = workbook.CreateCellStyle();

            var font = workbook.CreateFont();
            font.FontName = "Tahoma";
            font.FontHeightInPoints = 9;
            font.Color = HSSFColor.Black.Index;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            titleStyle.Indention = 2;
            titleStyle.SetFont(font);
            return titleStyle;
        }

        /// <summary>
        /// Gets the header style.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <returns>The header style.</returns>
        private ICellStyle HeaderStyle(HSSFWorkbook workbook)
        {
            var headerStyle = workbook.CreateCellStyle();

            var font = workbook.CreateFont();
            font.FontName = "Tahoma";
            font.FontHeightInPoints = 9;
            font.Color = HSSFColor.Black.Index;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            var palette = workbook.GetCustomPalette();
            var colorIndex = palette.FindSimilarColor(131, 202, 255).GetIndex();
            var borderColorIndex = palette.FindSimilarColor(128, 100, 162).GetIndex();

            headerStyle.SetFont(font);
            headerStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            headerStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;

            headerStyle.BottomBorderColor = borderColorIndex;
            headerStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            headerStyle.TopBorderColor = borderColorIndex;
            headerStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            headerStyle.LeftBorderColor = borderColorIndex;
            headerStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            headerStyle.RightBorderColor = borderColorIndex;
            headerStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            headerStyle.FillForegroundColor = colorIndex;
            headerStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            return headerStyle;
        }

        /// <summary>
        /// Gets the report style.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <returns>The title style.</returns>
        private ICellStyle ReportStyle(HSSFWorkbook workbook)
        {
            var reportStyle = workbook.CreateCellStyle();
            var palette = workbook.GetCustomPalette();
            var borderColorIndex = palette.FindSimilarColor(128, 100, 162).GetIndex();

            reportStyle.BottomBorderColor = borderColorIndex;
            reportStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            reportStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            reportStyle.TopBorderColor = borderColorIndex;
            reportStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            reportStyle.LeftBorderColor = borderColorIndex;
            reportStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            reportStyle.RightBorderColor = borderColorIndex;
            reportStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            reportStyle.WrapText = true;
            return reportStyle;
        }

        /// <summary>
        /// Exports the header member statement.
        /// </summary>
        /// <param name="workbook">The work book.</param>
        /// <param name="worksheet">The work sheet.</param>
        /// <param name="criteria">The criteria.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportHeaderMemberStatement(
            HSSFWorkbook workbook,
            ISheet worksheet,
            MemberStatementCriteria criteria)
        {
            var currentRow = 0;
            var titleRow = worksheet.CreateRow(currentRow);
            var titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue(Labels.Statement + " (" + criteria.Username + "): " + criteria.FromDate.ToString("MM/dd/yyyy") +
                "-->" + criteria.ToDate.ToString("MM/dd/yyyy"));
            titleCell.CellStyle = TitleStyle(workbook);
            currentRow++;

            var headerRow = worksheet.CreateRow(currentRow);
            var columnWidths = new int[] { 13, 20, 20, 20, 20, 20, 20, 20, 20 };
            var maxColumnIndex = 8;
            for (var i = 0; i <= maxColumnIndex; i++)
            {
                headerRow.Sheet.SetColumnWidth(i, columnWidths[i] * 256);
            }

            var headerStyle = HeaderStyle(workbook);
            var currentColumn = 0;
            var currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Date);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Opening_Balance);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Turnover);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Void + "/" + Labels.Rejected);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Outstanding);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Win_Loss);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Commission);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.GrandPrize);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Transfer_Amt);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Accumulated_Total);
            currentCell.CellStyle = headerStyle;
            currentColumn++;
        }

        /// <summary>
        /// Exports the body member statement.
        /// </summary>
        /// <param name="workbook">The work book.</param>
        /// <param name="worksheet">The work sheet.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="data">The statement report.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportBodyMemberStatement(
            HSSFWorkbook workbook,
            ISheet worksheet,
            MemberStatementCriteria criteria,
            StatementReportModel data)
        {
            var currentRow = 2;
            var statementListLength = data.StatementList.Count;
            if (statementListLength == 0)
            {
                ExportEmptyReport(workbook, worksheet, 8, currentRow);
                return;
            }

            var palette = workbook.GetCustomPalette();
            var evenRowColorIndex = palette.FindSimilarColor(230, 230, 255).GetIndex();
            var oddRowColorIndex = palette.FindSimilarColor(255, 255, 204).GetIndex();
            var noteOpeningBalanceColorIndex = palette.FindSimilarColor(255, 102, 255).GetIndex();

            var reportStyle = ReportStyle(workbook);
            var textStyle = workbook.CreateCellStyle();

            var mediumBlackNormal = workbook.CreateFont();
            mediumBlackNormal.FontName = "Tahoma";
            mediumBlackNormal.FontHeightInPoints = 9;
            mediumBlackNormal.Color = HSSFColor.Black.Index;

            var mediumRedNormal = workbook.CreateFont();
            mediumRedNormal.FontName = "Tahoma";
            mediumRedNormal.FontHeightInPoints = 9;
            mediumRedNormal.Color = HSSFColor.Red.Index;

            textStyle.CloneStyleFrom(reportStyle);
            textStyle.SetFont(mediumBlackNormal);

            var centerTextStyle = workbook.CreateCellStyle();
            centerTextStyle.CloneStyleFrom(textStyle);
            centerTextStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            var twoDecimalNumberStyle = workbook.CreateCellStyle();
            twoDecimalNumberStyle.CloneStyleFrom(reportStyle);
            twoDecimalNumberStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");
            twoDecimalNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            twoDecimalNumberStyle.SetFont(mediumBlackNormal);

            var twoDecimalRedNumberStyle = workbook.CreateCellStyle();
            twoDecimalRedNumberStyle.CloneStyleFrom(twoDecimalNumberStyle);
            twoDecimalRedNumberStyle.SetFont(mediumRedNormal);

            StatementModel previousStatement = null;
            for (int i = 0; i < statementListLength; i++)
            {
                var row = worksheet.CreateRow(currentRow);
                var colorIndex = i % 2 == 1 ? oddRowColorIndex : evenRowColorIndex;

                var statement = data.StatementList[i];
                var currentColumn = 0;
                var column = row.CreateCell(currentColumn);
                column.SetCellValue(statement.StatementDateString);
                column.CellStyle = centerTextStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.OpeningBalance);
                var cellStyle = workbook.CreateCellStyle();
                cellStyle.CloneStyleFrom(IsNegative(statement.OpeningBalance) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle);
                if (previousStatement != null &&
                    (double)statement.OpeningBalance.Value != (double)previousStatement.AccumulatedTotal.Value)
                {
                    cellStyle.FillForegroundColor = noteOpeningBalanceColorIndex;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                }

                column.CellStyle = cellStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.Turnover);
                column.CellStyle = twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.VoidCancelled);
                column.CellStyle = IsNegative(statement.VoidCancelled) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.Outstanding);
                column.CellStyle = IsNegative(statement.Outstanding) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.WinLoss);
                column.CellStyle = IsNegative(statement.WinLoss) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.Commission);
                column.CellStyle = IsNegative(statement.Commission) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.GrandPrize);
                column.CellStyle = IsNegative(statement.GrandPrize) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.TransferAmount);
                column.CellStyle = IsNegative(statement.TransferAmount) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.AccumulatedTotal);
                column.CellStyle = IsNegative(statement.AccumulatedTotal) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                previousStatement = statement;
                currentRow++;
            }
        }

        /// <summary>
        /// Exports the header member statement.
        /// </summary>
        /// <param name="workbook">The work book.</param>
        /// <param name="worksheet">The work sheet.</param>
        /// <param name="criteria">The criteria.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportHeaderSMAStatement(
            HSSFWorkbook workbook,
            ISheet worksheet,
            SMAStatementCriteria criteria)
        {
            var currentRow = 0;
            var titleRow = worksheet.CreateRow(currentRow);
            var titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue(Labels.Statement + " (" + criteria.Username + "): " + criteria.FromDate.ToString("MM/dd/yyyy") +
                "-->" + criteria.ToDate.ToString("MM/dd/yyyy"));
            titleCell.CellStyle = TitleStyle(workbook);
            currentRow++;

            var headerRow = worksheet.CreateRow(currentRow);
            var columnWidths = new int[] { 13, 20, 20, 20, 20, 20, 20 };
            var maxColumnIndex = 6;
            for (var i = 0; i <= maxColumnIndex; i++)
            {
                headerRow.Sheet.SetColumnWidth(i, columnWidths[i] * 256);
            }

            var headerStyle = HeaderStyle(workbook);
            var currentColumn = 0;
            var currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Date);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Opening_Balance);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Turnover);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Win_Loss);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Commission);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Transfer_Amt);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Accumulated_Total);
            currentCell.CellStyle = headerStyle;
            currentColumn++;
        }

        /// <summary>
        /// Exports the body member statement.
        /// </summary>
        /// <param name="workbook">The work book.</param>
        /// <param name="worksheet">The work sheet.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="data">The statement report.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportBodySMAStatement(
            HSSFWorkbook workbook,
            ISheet worksheet,
            SMAStatementCriteria criteria,
            StatementReportModel data)
        {
            var statementListLength = data.StatementList.Count;
            var currentRow = 2;
            if (statementListLength == 0)
            {
                ExportEmptyReport(workbook, worksheet, 6, currentRow);
                return;
            }

            var palette = workbook.GetCustomPalette();
            var evenRowColorIndex = palette.FindSimilarColor(230, 230, 255).GetIndex();
            var oddRowColorIndex = palette.FindSimilarColor(255, 255, 204).GetIndex();
            var noteOpeningBalanceColorIndex = palette.FindSimilarColor(255, 102, 255).GetIndex();

            var reportStyle = ReportStyle(workbook);
            var textStyle = workbook.CreateCellStyle();

            var mediumBlackNormal = workbook.CreateFont();
            mediumBlackNormal.FontName = "Tahoma";
            mediumBlackNormal.FontHeightInPoints = 9;
            mediumBlackNormal.Color = HSSFColor.Black.Index;

            var mediumRedNormal = workbook.CreateFont();
            mediumRedNormal.FontName = "Tahoma";
            mediumRedNormal.FontHeightInPoints = 9;
            mediumRedNormal.Color = HSSFColor.Red.Index;

            textStyle.CloneStyleFrom(reportStyle);
            textStyle.SetFont(mediumBlackNormal);

            var centerTextStyle = workbook.CreateCellStyle();
            centerTextStyle.CloneStyleFrom(textStyle);
            centerTextStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            var twoDecimalNumberStyle = workbook.CreateCellStyle();
            twoDecimalNumberStyle.CloneStyleFrom(reportStyle);
            twoDecimalNumberStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");
            twoDecimalNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            twoDecimalNumberStyle.SetFont(mediumBlackNormal);

            var twoDecimalRedNumberStyle = workbook.CreateCellStyle();
            twoDecimalRedNumberStyle.CloneStyleFrom(twoDecimalNumberStyle);
            twoDecimalRedNumberStyle.SetFont(mediumRedNormal);

            StatementModel previousStatement = null;
            for (int i = 0; i < statementListLength; i++)
            {
                var row = worksheet.CreateRow(currentRow);
                var colorIndex = i % 2 == 1 ? oddRowColorIndex : evenRowColorIndex;

                var statement = data.StatementList[i];
                var currentColumn = 0;
                var column = row.CreateCell(currentColumn);
                column.SetCellValue(statement.StatementDateString);
                column.CellStyle = centerTextStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.OpeningBalance);
                var cellStyle = workbook.CreateCellStyle();
                cellStyle.CloneStyleFrom(IsNegative(statement.OpeningBalance) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle);
                if (previousStatement != null &&
                   (double)statement.OpeningBalance.Value != (double)previousStatement.AccumulatedTotal.Value)
                {
                    cellStyle.FillForegroundColor = noteOpeningBalanceColorIndex;
                    cellStyle.FillPattern = FillPattern.SolidForeground;
                }

                column.CellStyle = cellStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.Turnover);
                column.CellStyle = twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.WinLoss);
                column.CellStyle = IsNegative(statement.WinLoss) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.Commission);
                column.CellStyle = IsNegative(statement.Commission) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.TransferAmount);
                column.CellStyle = IsNegative(statement.TransferAmount) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.AccumulatedTotal);
                column.CellStyle = IsNegative(statement.AccumulatedTotal) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                previousStatement = statement;
                currentRow++;
            }
        }

        private void ExportDataColumnModel(ICell column, DataColumnModel cellValue)
        {
            if (cellValue.HasData)
            {
                column.SetCellValue((double)cellValue.Value);
            }
            else
            {
                column.SetCellValue(Labels.Symbol_NoData);
            }
        }

        private void ExportDataColumnText(ICell column, string cellValue)
        {
            column.SetCellValue(cellValue);
        }

        /// <summary>
        /// Exports the header member statement.
        /// </summary>
        /// <param name="workbook">The work book.</param>
        /// <param name="worksheet">The work sheet.</param>
        /// <param name="criteria">The criteria.</param>
        private void ExportHeaderTransferDetails(
            HSSFWorkbook workbook,
            ISheet worksheet,
            TransferDetailsCriteria criteria)
        {
            var isShowCreator = StatementSection.Instance.ShowCreatorOnDetail;
            var currentRow = 0;
            var titleRow = worksheet.CreateRow(currentRow);
            var titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue(Labels.Transfer_Details + " ( " + criteria.Username + "): " + criteria.TransferDate);
            titleCell.CellStyle = TitleStyle(workbook);
            currentRow++;

            var headerRow = worksheet.CreateRow(currentRow);
            var columnWidths = new List<int> { 13, 50, 20 };
            if (isShowCreator)
            {
                columnWidths.Add(13);
            }

            var maxColumnIndex = 2;
            for (var i = 0; i <= maxColumnIndex; i++)
            {
                headerRow.Sheet.SetColumnWidth(i, columnWidths[i] * 256);
            }

            var headerStyle = HeaderStyle(workbook);
            var currentColumn = 0;
            var currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Date);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Remark);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            currentCell = headerRow.CreateCell(currentColumn);
            currentCell.SetCellValue(Labels.Amount);
            currentCell.CellStyle = headerStyle;
            currentColumn++;

            if (isShowCreator)
            {
                currentCell = headerRow.CreateCell(currentColumn);
                currentCell.SetCellValue(Labels.Creator);
                currentCell.CellStyle = headerStyle;
                currentColumn++;
            }
        }

        /// <summary>
        /// Exports the body transfer details.
        /// </summary>
        /// <param name="workbook">The work book.</param>
        /// <param name="worksheet">The work sheet.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="data">The statement report.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        private void ExportBodyTransferDetails(
            HSSFWorkbook workbook,
            ISheet worksheet,
            TransferDetailsCriteria criteria,
            StatementReportModel data)
        {
            var statementListLength = data.StatementList.Count;
            var currentRow = 2;
            var maxColumnIndex = 2;
            if (statementListLength == 0)
            {
                ExportEmptyReport(workbook, worksheet, maxColumnIndex, currentRow);
                return;
            }

            var palette = workbook.GetCustomPalette();
            var evenRowColorIndex = palette.FindSimilarColor(230, 230, 255).GetIndex();
            var oddRowColorIndex = palette.FindSimilarColor(255, 255, 204).GetIndex();

            var reportStyle = ReportStyle(workbook);
            var textStyle = workbook.CreateCellStyle();

            var mediumBlackNormal = workbook.CreateFont();
            mediumBlackNormal.FontName = "Tahoma";
            mediumBlackNormal.FontHeightInPoints = 9;
            mediumBlackNormal.Color = HSSFColor.Black.Index;

            var mediumRedNormal = workbook.CreateFont();
            mediumRedNormal.FontName = "Tahoma";
            mediumRedNormal.FontHeightInPoints = 9;
            mediumRedNormal.Color = HSSFColor.Red.Index;

            textStyle.CloneStyleFrom(reportStyle);
            textStyle.SetFont(mediumBlackNormal);

            var centerTextStyle = workbook.CreateCellStyle();
            centerTextStyle.CloneStyleFrom(textStyle);
            centerTextStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;

            var twoDecimalNumberStyle = workbook.CreateCellStyle();
            twoDecimalNumberStyle.CloneStyleFrom(reportStyle);
            twoDecimalNumberStyle.DataFormat = workbook.CreateDataFormat().GetFormat("#,##0.00");
            twoDecimalNumberStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            twoDecimalNumberStyle.SetFont(mediumBlackNormal);

            var twoDecimalRedNumberStyle = workbook.CreateCellStyle();
            twoDecimalRedNumberStyle.CloneStyleFrom(twoDecimalNumberStyle);
            twoDecimalRedNumberStyle.SetFont(mediumRedNormal);

            StatementModel statement;
            var currentColumn = 0;
            ICell column;
            IRow row;
            var isShowExchangeRate = StatementSection.Instance.ShowExchangeRateOnDetail;
            var isShowCreator = StatementSection.Instance.ShowCreatorOnDetail;
            for (int i = 0; i < statementListLength; i++)
            {
                row = worksheet.CreateRow(currentRow);
                var colorIndex = i % 2 == 1 ? oddRowColorIndex : evenRowColorIndex;

                statement = data.StatementList[i];
                currentColumn = 0;
                column = row.CreateCell(currentColumn);
                column.SetCellValue(statement.StatementDateString.Replace("<br>", "\n"));
                column.CellStyle = centerTextStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);

                var remark = statement.TransactionType + "\n" + statement.Description +
                   (isShowExchangeRate && !string.IsNullOrEmpty(statement.ExchangeRate) ?
                       " - " + Labels.Symbol_ExchangeRate + ":" + statement.ExchangeRate :
                       string.Empty);
                column.SetCellValue(remark);
                column.CellStyle = textStyle;
                currentColumn++;

                column = row.CreateCell(currentColumn);
                ExportDataColumnModel(column, statement.TransferAmount);
                column.CellStyle = IsNegative(statement.TransferAmount) ?
                    twoDecimalRedNumberStyle :
                    twoDecimalNumberStyle;
                currentColumn++;

                if (isShowCreator)
                {
                    column = row.CreateCell(currentColumn);
                    ExportDataColumnText(column, statement.Creator);
                    column.CellStyle = textStyle;
                    currentColumn++;
                }

                currentRow++;
            }

            statement = data.Total;
            currentColumn = 0;

            var grandTotalColorIndex = palette.FindSimilarColor(219, 225, 187).GetIndex();
            var grandTotalTextStyle = workbook.CreateCellStyle();

            var boldBlackNormal = workbook.CreateFont();
            boldBlackNormal.FontName = "Tahoma";
            boldBlackNormal.FontHeightInPoints = 9;
            boldBlackNormal.Color = HSSFColor.Black.Index;
            boldBlackNormal.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            var boldRedNormal = workbook.CreateFont();
            boldRedNormal.FontName = "Tahoma";
            boldRedNormal.FontHeightInPoints = 9;
            boldRedNormal.Color = HSSFColor.Red.Index;
            boldRedNormal.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            grandTotalTextStyle.CloneStyleFrom(reportStyle);
            grandTotalTextStyle.SetFont(boldBlackNormal);
            grandTotalTextStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
            grandTotalTextStyle.FillForegroundColor = grandTotalColorIndex;
            grandTotalTextStyle.FillPattern = FillPattern.SolidForeground;

            var twoDecimalRedNumberStyleGrandTotal = workbook.CreateCellStyle();
            twoDecimalRedNumberStyleGrandTotal.CloneStyleFrom(twoDecimalRedNumberStyle);
            twoDecimalRedNumberStyleGrandTotal.SetFont(boldRedNormal);

            var twoDecimalNumberStyleGrandTotal = workbook.CreateCellStyle();
            twoDecimalNumberStyleGrandTotal.CloneStyleFrom(twoDecimalNumberStyle);
            twoDecimalNumberStyleGrandTotal.SetFont(boldBlackNormal);

            row = worksheet.CreateRow(currentRow);
            column = row.CreateCell(currentColumn);
            column.CellStyle = grandTotalTextStyle;
            currentColumn++;

            column = row.CreateCell(currentColumn);
            column.SetCellValue(Labels.Total);
            column.CellStyle = grandTotalTextStyle;
            currentColumn++;

            column = row.CreateCell(currentColumn);
            ExportDataColumnModel(column, statement.TransferAmount);
            var cellStyle = workbook.CreateCellStyle();
            cellStyle.CloneStyleFrom(IsNegative(statement.TransferAmount) ?
                twoDecimalRedNumberStyleGrandTotal :
                twoDecimalNumberStyleGrandTotal);
            cellStyle.FillForegroundColor = grandTotalColorIndex;
            cellStyle.FillPattern = FillPattern.SolidForeground;
            column.CellStyle = cellStyle;
            currentColumn++;

            if (isShowCreator)
            {
                column = row.CreateCell(currentColumn);
                column.SetCellValue(string.Empty);
                column.CellStyle = grandTotalTextStyle;
                currentColumn++;
            }

            currentRow++;
        }

        private bool IsNegative(DataColumnModel cellValue)
        {
            return cellValue.HasData ?
                (double)cellValue.Value < 0 :
                false;
        }

        /// <summary>
        /// Exports the empty report.
        /// </summary>
        /// <param name="workbook">The workbook.</param>
        /// <param name="worksheet">The worksheet.</param>
        /// <param name="maxColumnIndex">Maximum index of the column.</param>
        /// <param name="currentRow">The current row.</param>
        private void ExportEmptyReport(
            HSSFWorkbook workbook,
            ISheet worksheet,
            int maxColumnIndex,
            int currentRow)
        {
            var textStyle = workbook.CreateCellStyle();
            var font = workbook.CreateFont();
            font.FontName = "Tahoma";
            font.FontHeightInPoints = 9;
            font.Color = HSSFColor.Red.Index;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            textStyle.SetFont(font);

            var row = worksheet.CreateRow(currentRow);
            var column = row.CreateCell(0);
            column.SetCellValue(Labels.Message_NoDataReport);
            column.CellStyle = textStyle;
        }
    }
}
