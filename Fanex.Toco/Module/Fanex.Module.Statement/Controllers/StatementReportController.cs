// --------------------------------------------------------------------------
// <copyright file="StatementReportController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Controllers
{
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.Module.Statement.Criteria.StatementReport;
    using Fanex.Module.Statement.Models.StatementReport;
    using Fanex.Module.Statement.Resources;
    using Fanex.Module.Statement.Services.StatementReport;

    /// <summary>
    /// Statement Report Controller.
    /// </summary>
    public class StatementReportController : BaseController
    {
        public const string SessionKeyStatement = "Fanex.Razorbill.Report.Statement";

        public const string SessionKeyTransferDetails = "Fanex.Razorbill.Report.TransferDetails";

        private readonly IStatementReportService _statementReportService;

        public StatementReportController() :
            this(null)
        {
        }

        public StatementReportController(IStatementReportService statementReportService)
        {
            _statementReportService = statementReportService ?? new StatementReportService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return this.View("~/Areas/Statement/Views/Report/Index.cshtml");
        }
        
        [HttpPost, AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "Statement")]
        public JsonResult SMAStatement(SMAStatementCriteria criteria)
        {
            var statementList = _statementReportService.SMAStatement(criteria);
            Session[SessionKeyStatement] = statementList;
            return Json(statementList);
        }

        public JsonResult MemberStatement(MemberStatementCriteria criteria)
        {
            var statementList = _statementReportService.MemberStatement(criteria);
            Session[SessionKeyStatement] = statementList;
            return Json(statementList);
        }

        [HttpGet]
        public ActionResult TransferDetails(TransferDetailsCriteria criteria)
        {
            var statementList = _statementReportService.TransferDetails(criteria);
            Session[SessionKeyTransferDetails] = statementList;
            ViewBag.TransferDetailsCriteria = criteria;
            return View("~/Areas/Statement/Views/StatementDetails/Index.cshtml", statementList);
        }

        public ActionResult ExportMemberStatement(MemberStatementCriteria criteria)
        {
            var fileDownloadName = string.Format(Labels.StatementReportName, criteria.FromDate, criteria.ToDate);
            return File(
                _statementReportService.ExportMemberStatement(criteria, (StatementReportModel)Session[SessionKeyStatement]).ToArray(),
                "application/vnd.ms-excel",
                fileDownloadName);
        }

        public ActionResult ExportSMAStatement(SMAStatementCriteria criteria)
        {
            var fileDownloadName = string.Format(Labels.StatementReportName, criteria.FromDate, criteria.ToDate);
            return File(
                _statementReportService.ExportSMAStatement(criteria, (StatementReportModel)Session[SessionKeyStatement]).ToArray(),
                "application/vnd.ms-excel",
                fileDownloadName);
        }

        public ActionResult ExportTransferDetails(TransferDetailsCriteria criteria)
        {
            var fileDownloadName = string.Format(Labels.TransferDetails_ReportName, criteria.TransferDate);
            return File(
                _statementReportService.ExportTransferDetails(criteria, (StatementReportModel)Session[SessionKeyTransferDetails]).ToArray(),
                "application/vnd.ms-excel",
                fileDownloadName);
        }
    }
}
