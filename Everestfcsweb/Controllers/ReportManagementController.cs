using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using Everestfcsweb.Entities;
using Everestfcsweb.Entities.Reports;
using Everestfcsweb.Enum;
using Everestfcsweb.Models;
using Everestfcsweb.Services;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;
using Org.BouncyCastle.Crypto.Agreement;
using System.Data;
using System.Text;


namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class ReportManagementController : BaseController
    {
        private readonly FuelproReportDataServices bl;
        public ReportManagementController(IConfiguration config)
        {
            bl = new FuelproReportDataServices(config);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        public async Task<FileResult> GenerateSaleRecieptPdf(long FinanceTransactionId = 0, long AccountId = 0)
        {
            var data = await bl.Getsingleofflinesalesdata(SessionUserData.Token, FinanceTransactionId, AccountId);
            data.TenantName = data.TenantName ?? string.Empty;
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            string strPDFFileName = string.Format(data.TransactionRefenece + dTime.ToString("yyyyMMdd") + ".pdf");
            Document doc = new Document();
            doc.SetMargins(0, 0, 10, 0);
            PdfPTable tableLayout = new PdfPTable(4);
            doc.SetMargins(10, 10, 10, 0);
            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            Font fontInvoice = new Font(bf, 12, Font.NORMAL);
            Paragraph paragraph = new Paragraph(data.TenantName.ToUpper(), fontInvoice);
            paragraph.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph);
            Font fontreciept = new Font(bf, 9, Font.NORMAL);
            Paragraph paragraph1 = new Paragraph(data.StationName.ToUpper(), fontreciept);
            paragraph1.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph1);
            //Paragraph paragraph2 = new Paragraph("STATION ADDRESS", fontreciept);
            //paragraph2.Alignment = Element.ALIGN_CENTER;
            //doc.Add(paragraph2);
            Font fontreciept1 = new Font(bf, 8, Font.BOLD);
            Paragraph paragraph3 = new Paragraph(data.Saledescription.ToUpper(), fontreciept);
            paragraph3.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph3);
            Paragraph paragraph4 = new Paragraph("TERM#: " + data.TransactionCode, fontreciept);
            paragraph4.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph4);
            Paragraph paragraph5 = new Paragraph("REF#: " + data.TransactionRefenece, fontreciept);
            paragraph5.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph5);
            Paragraph paragraph6 = new Paragraph("----------------------------------------------------------", fontreciept);
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            doc.Add(Add_Content_To_PDF(tableLayout, data));
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            Paragraph paragraph21 = new Paragraph("POINTS: " + data.CurrentPoints.ToString("#,##0.00"), fontreciept);
            paragraph21.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph21);
            Paragraph paragraph22 = new Paragraph("TOTAL POINTS: " + data.CumulativePoints.ToString("#,##0.00"), fontreciept);
            paragraph22.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph22);
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            Paragraph paragraph23 = new Paragraph("CARD BALANCE: " + data.CardBalance.ToString("#,##0.00"), fontreciept);
            paragraph23.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph23);
            Paragraph paragraph24 = new Paragraph("CUSTOMER BALANCE: " + data.CustomerBalance.ToString("#,##0.00"), fontreciept);
            paragraph24.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph24);
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            Paragraph paragraph8 = new Paragraph("CUSTOMER: " + data.Customername.ToUpper(), fontreciept);
            paragraph8.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph8);
            Paragraph paragraph9 = new Paragraph("AGREEMENT: " + data.Agreementtypename.ToUpper(), fontreciept);
            paragraph9.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph9);
            Paragraph paragraph10 = new Paragraph("CARD: " + data.CustomerCard, fontreciept);
            paragraph10.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph10);
            Paragraph paragraph11 = new Paragraph("ACCOUNT TYPE: " + data.Groupingname, fontreciept);
            paragraph11.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph11);
            Paragraph paragraph12 = new Paragraph("SALE REF: " + data.SaleRefence, fontreciept);
            paragraph12.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph12);
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            Paragraph paragraph13 = new Paragraph("DATE: " + data.DateCreated.ToString("dd-MM-yyyy HH:mm:ss"), fontreciept);
            paragraph13.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph13);
            Paragraph paragraph14 = new Paragraph("SERVED BY: " + data.Attendantname, fontreciept);
            paragraph14.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph14);
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            Paragraph paragraph7 = new Paragraph("APPROVAL", fontreciept1);
            paragraph7.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph7);
            Paragraph paragraph18 = new Paragraph("Cardolder acknowledges reciept of goods and/or", fontreciept);
            paragraph18.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph18);
            Paragraph paragraph20 = new Paragraph(" services in the amount of total shown hereon.", fontreciept);
            paragraph20.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph20);
            Paragraph p3 = new Paragraph();
            p3.SpacingAfter = 6;
            doc.Add(p3);
            Paragraph paragraph19 = new Paragraph("Cardolder Signature:_______________________.", fontreciept);
            paragraph19.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph19);
            paragraph6.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph6);
            Paragraph paragraph15 = new Paragraph("CUSTOMER COPY", fontreciept1);
            paragraph15.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph15);
            Paragraph paragraph16 = new Paragraph("THANK YOU", fontreciept1);
            paragraph16.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph16);
            Paragraph paragraph17 = new Paragraph("Powered by Uttambsolutions", fontreciept1);
            paragraph17.Alignment = Element.ALIGN_CENTER;
            doc.Add(paragraph17);
            doc.Close();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            return File(workStream, "application/pdf", strPDFFileName);
        }
        
        
        #region Sales Transactions Report
        [HttpGet]
        public IActionResult SalesTransactions()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers,SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Postpaid Statement Report
        [HttpGet]
        public IActionResult PostpaidStatement()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Customer Payments
        [HttpGet]
        public IActionResult CustomerPayments()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Prepaid Customer Statement
        [HttpGet]
        public IActionResult PrepaidStatement()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Prepaid Customer Topups
        [HttpGet]
        public IActionResult CustomerTopups()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region  Points Monthly Cumulative
        [HttpGet]
        public IActionResult MonthlyCumulative()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Points Award Statement
        [HttpGet]
        public IActionResult AwardStatement()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Points Customer Award
        [HttpGet]
        public IActionResult CustomerAwards()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["SystemProductslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemProduct, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region Customer Point Redeem
        [HttpGet]
        public IActionResult CustomerRedeems()
        {
            ViewData["Systemcustomerslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomers, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        #endregion

        #region General report Generation Method
        [HttpPost]
        public async Task<IActionResult> Generateanysystemreportdata(string ReportName, DateTime startdate, DateTime enddate, long customer = 0, long agreement = 0, long group = 0, long station = 0, long paymode = 0, long account = 0, long attendant = 0, long product = 0)
        {
            if (ReportName == "SaleTransactionReport")
            {
                var data = await bl.Generatesalestransactiondata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Salestransactionreport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 14).Merge();
                    mergedCell.Value = "SALES TRANSACTION REPORT";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;

                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Product: ";
                    worksheet.Cell(currentRow, 4).Value = data.ProductName;
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "Paymentmode: ";
                    worksheet.Cell(currentRow, 4).Value = data.PaymentModeName;
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Account: ";
                    worksheet.Cell(currentRow, 2).Value = data.AccountName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Station: ";
                    worksheet.Cell(currentRow, 2).Value = data.StationName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Attendant: ";
                    worksheet.Cell(currentRow, 2).Value = data.AttendantName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;

                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Txn Date";
                    worksheet.Cell(currentRow, 2).Value = "Posting Date";
                    worksheet.Cell(currentRow, 3).Value = "Transaction Code";
                    worksheet.Cell(currentRow, 4).Value = "Station";
                    worksheet.Cell(currentRow, 5).Value = "Attendant";
                    worksheet.Cell(currentRow, 6).Value = "Customer";
                    worksheet.Cell(currentRow, 7).Value = "Mask";
                    worksheet.Cell(currentRow, 8).Value = "Equipment";
                    worksheet.Cell(currentRow, 9).Value = "Product";
                    worksheet.Cell(currentRow, 10).Value = "Price";
                    worksheet.Cell(currentRow, 11).Value = "Units";
                    worksheet.Cell(currentRow, 12).Value = "Discount";
                    worksheet.Cell(currentRow, 13).Value = "Amount";
                    worksheet.Cell(currentRow, 14).Value = "Net Amount";
                    worksheet.Range(currentRow, 1, currentRow, 14).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 14).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 14).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.SalesDetails != null)
                    {
                        foreach (var report in data.SalesDetails)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 2).Value = report.PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 3).Value = report.TransactionCode;
                            worksheet.Cell(currentRow, 4).Value = report.Station;
                            worksheet.Cell(currentRow, 5).Value = report.Attendant;
                            worksheet.Cell(currentRow, 6).Value = report.Customer;
                            worksheet.Cell(currentRow, 7).Value = report.Mask;
                            worksheet.Cell(currentRow, 8).Value = report.Equipment;
                            worksheet.Cell(currentRow, 9).Value = report.Products;
                            worksheet.Cell(currentRow, 10).Value = report.Price;
                            worksheet.Cell(currentRow, 11).Value = report.Units;
                            worksheet.Cell(currentRow, 12).Value = report.Discount;
                            worksheet.Cell(currentRow, 13).Value = report.SaleAmount;
                            worksheet.Cell(currentRow, 14).Value = report.SaleAmount - report.Discount;
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 10).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 14).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 14).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 11).Value = data.SalesDetails.Sum(x => x.Units).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 12).Value = data.SalesDetails.Sum(x => x.Discount).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 13).Value = data.SalesDetails.Sum(x => x.SaleAmount).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 14).Value = (data.SalesDetails.Sum(x => x.SaleAmount) - data.SalesDetails.Sum(x => x.Discount)).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Salestransactionreport.xlsx");
                    }
                }
            }
            else if (ReportName == "PostpaidstatementReport")
            {
                var data = await bl.Generatecustomerpostpaidstatementdata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Postpaidstatementreport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 13).Merge();
                    mergedCell.Value = "CUSTOMER POSTPAID STATEMENT REPORT";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;

                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;


                    worksheet.Cell(currentRow, 1).Value = "Paymentmode: ";
                    worksheet.Cell(currentRow, 2).Value = data.PaymentModeName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;


                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Txn Date";
                    worksheet.Cell(currentRow, 2).Value = "Posting Date";
                    worksheet.Cell(currentRow, 3).Value = "Txn Code";
                    worksheet.Cell(currentRow, 4).Value = "Memo";
                    worksheet.Cell(currentRow, 5).Value = "Id Number";
                    worksheet.Cell(currentRow, 6).Value = "Account";
                    worksheet.Cell(currentRow, 7).Value = "Station";
                    worksheet.Cell(currentRow, 8).Value = "Product";
                    worksheet.Cell(currentRow, 9).Value = "Price";
                    worksheet.Cell(currentRow, 10).Value = "Qty";
                    worksheet.Cell(currentRow, 11).Value = "Discount";
                    worksheet.Cell(currentRow, 12).Value = "Amount";
                    worksheet.Cell(currentRow, 13).Value = "Balance";
                    worksheet.Range(currentRow, 1, currentRow, 13).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 13).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 12).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.PostPaidCustomerStatementData != null)
                    {
                        foreach (var report in data.PostPaidCustomerStatementData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.ActualDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 2).Value = report.Datecreated.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 3).Value = report.TransactionCode;
                            worksheet.Cell(currentRow, 4).Value = report.Description;
                            worksheet.Cell(currentRow, 5).Value = "";
                            worksheet.Cell(currentRow, 6).Value = report.Equipment;
                            worksheet.Cell(currentRow, 7).Value = report.Station;
                            worksheet.Cell(currentRow, 8).Value = report.Product;
                            worksheet.Cell(currentRow, 9).Value = report.Price;
                            worksheet.Cell(currentRow, 10).Value = report.Units;
                            worksheet.Cell(currentRow, 11).Value = report.Discount;
                            worksheet.Cell(currentRow, 12).Value = report.TotalAmount;
                            worksheet.Cell(currentRow, 13).Value = report.Balance;
                        }
                        //// Calculate totals
                        //currentRow++;

                        //worksheet.Cell(currentRow, 1).Value = "Total: ";
                        //worksheet.Range(currentRow, 1, currentRow, 10).Merge().Style.Font.Bold = true;
                        //worksheet.Range(currentRow, 1, currentRow, 13).Style.Fill.BackgroundColor = XLColor.Gray;
                        //worksheet.Range(currentRow, 1, currentRow, 13).Style.Font.FontColor = XLColor.White;

                        //worksheet.Cell(currentRow, 11).Value = data.PostPaidCustomerStatementData.Sum(x => x.Units).ToString("#,##0.00");
                        //worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                        //worksheet.Cell(currentRow, 12).Value = data.PostPaidCustomerStatementData.Sum(x => x.Discount).ToString("#,##0.00");
                        //worksheet.Cell(currentRow, 12).Style.Font.Bold = true;

                        //worksheet.Cell(currentRow, 13).Value = data.PostPaidCustomerStatementData.Sum(x => x.SaleAmount).ToString("#,##0.00");
                        //worksheet.Cell(currentRow, 13).Style.Font.Bold = true;

                        //worksheet.Cell(currentRow, 14).Value = (data.PostPaidCustomerStatementData.Sum(x => x.SaleAmount) - data.SalesDetails.Sum(x => x.Discount)).ToString("#,##0.00");
                        //worksheet.Cell(currentRow, 14).Style.Font.Bold = true;

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Postpaidstatementreport.xlsx");
                    }
                }
            }
            else if (ReportName == "CustomerpaymentReport")
            {
                var data = await bl.GetCustomerPaymentReportData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Customerpaymentreport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 9).Merge();
                    mergedCell.Value = "CUSTOMER PAYMENT REPORT";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;

                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;


                    worksheet.Cell(currentRow, 1).Value = "Paymentmode: ";
                    worksheet.Cell(currentRow, 2).Value = data.PaymentModeName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;

                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Txn Date";
                    worksheet.Cell(currentRow, 2).Value = "Posting Date";
                    worksheet.Cell(currentRow, 3).Value = "Reference";
                    worksheet.Cell(currentRow, 4).Value = "Customer";
                    worksheet.Cell(currentRow, 5).Value = "Description";
                    worksheet.Cell(currentRow, 6).Value = "Payment Ref";
                    worksheet.Cell(currentRow, 7).Value = "Mode";
                    worksheet.Cell(currentRow, 8).Value = "Attendant";
                    worksheet.Cell(currentRow, 9).Value = "Amount";
                    worksheet.Range(currentRow, 1, currentRow, 9).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 9).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 9).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.CustomerPaymentData != null)
                    {
                        foreach (var report in data.CustomerPaymentData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 2).Value = report.PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 3).Value = report.TopupReference;
                            worksheet.Cell(currentRow, 4).Value = report.Customer;
                            worksheet.Cell(currentRow, 5).Value = report.Description;
                            worksheet.Cell(currentRow, 6).Value = report.Reference;
                            worksheet.Cell(currentRow, 7).Value = report.TopupMode;
                            worksheet.Cell(currentRow, 8).Value = report.Attendant;
                            worksheet.Cell(currentRow, 9).Value = report.Amount;
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 8).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 9).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 9).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 9).Value = data.CustomerPaymentData.Sum(x => x.Amount).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;


                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customerpaymentreport.xlsx");
                    }
                }
            }
            else if (ReportName == "PrepaidStatementReport")
            {
                var data = await bl.GetCustomerPrepaidStatementReportData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Prepaidstatementreport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 12).Merge();
                    mergedCell.Value = "PREPAID CUSTOMER STATEMENT";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;

                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;


                    worksheet.Cell(currentRow, 1).Value = "Account: ";
                    worksheet.Cell(currentRow, 2).Value = data.AccountName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;

                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Txn Date";
                    worksheet.Cell(currentRow, 2).Value = "Posting Date";
                    worksheet.Cell(currentRow, 3).Value = "Txn Code";
                    worksheet.Cell(currentRow, 3).Value = "Memo";
                    worksheet.Cell(currentRow, 4).Value = "Id Number";
                    worksheet.Cell(currentRow, 5).Value = "Account";
                    worksheet.Cell(currentRow, 6).Value = "Station";
                    worksheet.Cell(currentRow, 7).Value = "Product";
                    worksheet.Cell(currentRow, 8).Value = "Price";
                    worksheet.Cell(currentRow, 9).Value = "Qty";
                    worksheet.Cell(currentRow, 10).Value = "Discount";
                    worksheet.Cell(currentRow, 11).Value = "Amount";
                    worksheet.Cell(currentRow, 12).Value = "Balance";
                    worksheet.Range(currentRow, 1, currentRow, 12).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 12).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 12).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.CustomerPrepaidStatementData != null)
                    {
                        foreach (var report in data.CustomerPrepaidStatementData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.TransactionDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 2).Value = report.PostingDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 3).Value = report.TransactionCode;
                            worksheet.Cell(currentRow, 4).Value = report.Memo;
                            worksheet.Cell(currentRow, 5).Value = "";
                            worksheet.Cell(currentRow, 6).Value = report.Station;
                            worksheet.Cell(currentRow, 7).Value = report.Product;
                            worksheet.Cell(currentRow, 8).Value = report.Price.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 9).Value = report.QTY.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 10).Value = report.Discount.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 11).Value = report.TotalAmount.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 12).Value = report.Balance.ToString("#,##0.00");
                        }
                        //// Calculate totals
                        //currentRow++;

                        //worksheet.Cell(currentRow, 1).Value = "Total: ";
                        //worksheet.Range(currentRow, 1, currentRow, 8).Merge().Style.Font.Bold = true;
                        //worksheet.Range(currentRow, 1, currentRow, 9).Style.Fill.BackgroundColor = XLColor.Gray;
                        //worksheet.Range(currentRow, 1, currentRow, 9).Style.Font.FontColor = XLColor.White;

                        //worksheet.Cell(currentRow, 9).Value = data.CustomerPrepaidStatementData.Sum(x => x.Amount).ToString("#,##0.00");
                        //worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PrepaidStatementReport.xlsx");
                    }
                }
            }
            else if (ReportName == "PrepaidCustomerTopupreport")
            {
                var data = await bl.GetCustomerTopupReportData(SessionUserData.Token,SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Prepaidcustomertopupreport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 12).Merge();
                    mergedCell.Value = "PREPAID CUSTOMER TOPUPS";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;

                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;


                    worksheet.Cell(currentRow, 1).Value = "Topupmode: ";
                    worksheet.Cell(currentRow, 2).Value = data.PaymentModeName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;

                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Txn Date";
                    worksheet.Cell(currentRow, 2).Value = "Txn Code";
                    worksheet.Cell(currentRow, 3).Value = "Customer";
                    worksheet.Cell(currentRow, 4).Value = "Id Number";
                    worksheet.Cell(currentRow, 5).Value = "Account";
                    worksheet.Cell(currentRow, 6).Value = "Station";
                    worksheet.Cell(currentRow, 7).Value = "Attendant";
                    worksheet.Cell(currentRow, 8).Value = "Mode";
                    worksheet.Cell(currentRow, 9).Value = "Reference";
                    worksheet.Cell(currentRow, 10).Value = "Notes";
                    worksheet.Cell(currentRow, 11).Value = "Amount";
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.CustomerTopupData != null)
                    {
                        foreach (var report in data.CustomerTopupData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.TopUpDate.ToString("yyyy-MM-dd HH:mm:ss");
                            worksheet.Cell(currentRow, 2).Value = report.TransactionCode;
                            worksheet.Cell(currentRow, 3).Value = report.Customer;
                            worksheet.Cell(currentRow, 4).Value = "";
                            worksheet.Cell(currentRow, 5).Value = report.AccountNumber;
                            worksheet.Cell(currentRow, 6).Value = report.Station;
                            worksheet.Cell(currentRow, 7).Value = report.Attendant;
                            worksheet.Cell(currentRow, 8).Value = report.TopupMode;
                            worksheet.Cell(currentRow, 9).Value = report.TopupReference;
                            worksheet.Cell(currentRow, 10).Value = report.Description;
                            worksheet.Cell(currentRow, 11).Value = report.Amount.ToString("#,##0.00");
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 10).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 11).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 11).Value = data.CustomerTopupData.Sum(x => x.Amount).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Prepaidcustomertopupreport.xlsx");
                    }
                }
            }
            else if (ReportName == "CustomerPointCumulativereport")
            {
                var data = await bl.Getcustomercumulativereportdata(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Customerpointcumulativereport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 6).Merge();
                    mergedCell.Value = "MONTHLY POINTS CUMULATIVE";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;

                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Statiomn: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 2).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = "End Date: ";
                    worksheet.Cell(currentRow, 2).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;




                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Station";
                    worksheet.Cell(currentRow, 2).Value = "Customer";
                    worksheet.Cell(currentRow, 3).Value = "Mask";
                    worksheet.Cell(currentRow, 4).Value = "Volume";
                    worksheet.Cell(currentRow, 5).Value = "Amount";
                    worksheet.Cell(currentRow, 6).Value = "Points";
                    worksheet.Range(currentRow, 1, currentRow, 6).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 6).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 6).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.CustomerPointCumulativeData != null)
                    {
                        foreach (var report in data.CustomerPointCumulativeData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.StationName;
                            worksheet.Cell(currentRow, 2).Value = report.CustomerName;
                            worksheet.Cell(currentRow, 3).Value = report.CardNumber;
                            worksheet.Cell(currentRow, 4).Value = report.CumSalesVol.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 5).Value = report.CumSalesAmt.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 6).Value = report.CumPoints.ToString("#,##0.00");
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 3).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 6).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 6).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 4).Value = data.CustomerPointCumulativeData.Sum(x => x.CumSalesVol).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 5).Value = data.CustomerPointCumulativeData.Sum(x => x.CumSalesAmt).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 6).Value = data.CustomerPointCumulativeData.Sum(x => x.CumPoints).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerCumulativePointReport.xlsx");
                    }
                }
            }
            else if (ReportName == "CustomerAwardsReport")
            {
                var data = await bl.GetCustomerPointAwardReportData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("CustomerAwardsReport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 11).Merge();
                    mergedCell.Value = "CUSTOMER POINT AWARD";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;
                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Station: ";
                    worksheet.Cell(currentRow, 2).Value = data.StationName;
                    worksheet.Cell(currentRow, 3).Value = "Attendant: ";
                    worksheet.Cell(currentRow, 4).Value = data.AttendantName;
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Account: ";
                    worksheet.Cell(currentRow, 2).Value = data.AccountName;
                    worksheet.Range(currentRow, 1, currentRow, 2).Style.Font.Bold = true;
                    currentRow++;


                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Date";
                    worksheet.Cell(currentRow, 2).Value = "Customer";
                    worksheet.Cell(currentRow, 3).Value = "Id Number";
                    worksheet.Cell(currentRow, 4).Value = "Account";
                    worksheet.Cell(currentRow, 5).Value = "Station";
                    worksheet.Cell(currentRow, 6).Value = "Attendant";
                    worksheet.Cell(currentRow, 7).Value = "Product";
                    worksheet.Cell(currentRow, 8).Value = "Price";
                    worksheet.Cell(currentRow, 9).Value = "Units";
                    worksheet.Cell(currentRow, 10).Value = "Amount";
                    worksheet.Cell(currentRow, 11).Value = "Points";
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.CustomerPointRewardData != null)
                    {
                        foreach (var report in data.CustomerPointRewardData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.TransactionDate;
                            worksheet.Cell(currentRow, 2).Value = report.Customer;
                            worksheet.Cell(currentRow, 3).Value = report.IDNumber;
                            worksheet.Cell(currentRow, 4).Value = report.Account;
                            worksheet.Cell(currentRow, 5).Value = report.Station;
                            worksheet.Cell(currentRow, 6).Value = report.Attendant;
                            worksheet.Cell(currentRow, 7).Value = report.Product;
                            worksheet.Cell(currentRow, 8).Value = report.Price;
                            worksheet.Cell(currentRow, 9).Value = report.Units.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 10).Value = report.SaleValue.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 11).Value = report.AwardValue.ToString("#,##0.00");
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 8).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 8).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 8).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 9).Value = data.CustomerPointRewardData.Sum(x => x.Units).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 10).Value = data.CustomerPointRewardData.Sum(x => x.SaleValue).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 11).Value = data.CustomerPointRewardData.Sum(x => x.AwardValue).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerPointAwardReport.xlsx");
                    }
                }
            }
            else if (ReportName == "CustomerAwardstatementReport")
            {
                var data = await bl.GetCustomerPointStatementReportData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, agreement, group, station, paymode, account, attendant, product);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("CustomerAwardAtatementReport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 11).Merge();
                    mergedCell.Value = "CUSTOMER POINT AWARD STATEMENT";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;
                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;

                    worksheet.Cell(currentRow, 1).Value = "Agreement: ";
                    worksheet.Cell(currentRow, 2).Value = data.AgreementName;
                    worksheet.Cell(currentRow, 3).Value = "End Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 4).Style.Font.Bold = true;
                    currentRow++;



                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Transaction Date";
                    worksheet.Cell(currentRow, 2).Value = "Description";
                    worksheet.Cell(currentRow, 3).Value = "Customer";
                    worksheet.Cell(currentRow, 4).Value = "ID Number";
                    worksheet.Cell(currentRow, 5).Value = "Card";
                    worksheet.Cell(currentRow, 6).Value = "Equipment";
                    worksheet.Cell(currentRow, 7).Value = "Station";
                    worksheet.Cell(currentRow, 8).Value = "Staff";
                    worksheet.Cell(currentRow, 9).Value = "Redeem";
                    worksheet.Cell(currentRow, 10).Value = "Award";
                    worksheet.Cell(currentRow, 11).Value = "Balance";
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.PointsStatementData != null)
                    {
                        foreach (var report in data.PointsStatementData)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.Date;
                            worksheet.Cell(currentRow, 2).Value = report.Description;
                            worksheet.Cell(currentRow, 3).Value = report.Customer;
                            worksheet.Cell(currentRow, 4).Value = report.Customeridnumber;
                            worksheet.Cell(currentRow, 5).Value = report.Mask;
                            worksheet.Cell(currentRow, 6).Value = report.Equipment;
                            worksheet.Cell(currentRow, 7).Value = report.StationName;
                            worksheet.Cell(currentRow, 8).Value = report.StaffName;
                            worksheet.Cell(currentRow, 9).Value = report.Debit.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 10).Value = report.Credit.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 11).Value = report.Balance.ToString("#,##0.00");
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 8).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 11).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 11).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 9).Value = data.PointsStatementData.Sum(x => x.Debit).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 10).Value = data.PointsStatementData.Sum(x => x.Credit).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 11).Value = data.PointsStatementData.Sum(x => x.Balance).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerPointAwardStatementReport.xlsx");
                    }
                }
            }
            else if (ReportName == "CustomerRedeemsReport")
            {
                var data = await bl.GetCustomerPointRedeemReportData(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, startdate, enddate, customer, station, account, attendant);
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("CustomerRedeemsReport");
                    var currentRow = 1;

                    var mergedCell = worksheet.Range(currentRow, 1, currentRow, 8).Merge();
                    mergedCell.Value = "CUSTOMER POINT REDEEM";
                    mergedCell.Style.Font.Bold = true;
                    mergedCell.Style.Fill.BackgroundColor = XLColor.Gray;
                    mergedCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    mergedCell.Style.Font.FontColor = XLColor.White;
                    currentRow++;
                    // Add header rows
                    worksheet.Cell(currentRow, 1).Value = "Customer: ";
                    worksheet.Cell(currentRow, 2).Value = data.CustomerName;
                    worksheet.Cell(currentRow, 3).Value = "Start Date: ";
                    worksheet.Cell(currentRow, 4).Value = data.StartDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Cell(currentRow, 5).Value = "End Date: ";
                    worksheet.Cell(currentRow, 6).Value = data.EndDate.ToString("yyyy-MM-dd HH:mm:ss");
                    worksheet.Range(currentRow, 1, currentRow, 6).Style.Font.Bold = true;
                    currentRow++;


                    // Add header for sales details
                    worksheet.Cell(currentRow, 1).Value = "Date";
                    worksheet.Cell(currentRow, 2).Value = "Customer";
                    worksheet.Cell(currentRow, 3).Value = "Id Number";
                    worksheet.Cell(currentRow, 4).Value = "Account";
                    worksheet.Cell(currentRow, 5).Value = "Station";
                    worksheet.Cell(currentRow, 6).Value = "Attendant";
                    worksheet.Cell(currentRow, 7).Value = "Redeem Value";
                    worksheet.Cell(currentRow, 8).Value = "Redeem Amount";
                    worksheet.Range(currentRow, 1, currentRow, 8).Style.Font.Bold = true;
                    worksheet.Range(currentRow, 1, currentRow, 8).Style.Fill.BackgroundColor = XLColor.Gray;
                    worksheet.Range(currentRow, 1, currentRow, 8).Style.Font.FontColor = XLColor.White;

                    // Add sales details
                    if (data.CustomerPointRedeem != null)
                    {
                        foreach (var report in data.CustomerPointRedeem)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = report.TransactionDate;
                            worksheet.Cell(currentRow, 2).Value = report.Customer;
                            worksheet.Cell(currentRow, 3).Value = report.IDNumber;
                            worksheet.Cell(currentRow, 4).Value = report.AccountNumber;
                            worksheet.Cell(currentRow, 5).Value = report.Station;
                            worksheet.Cell(currentRow, 6).Value = report.Attendant;
                            worksheet.Cell(currentRow, 7).Value = report.RedeemedValue.ToString("#,##0.00");
                            worksheet.Cell(currentRow, 8).Value = report.RedeemedAmount.ToString("#,##0.00");
                        }
                        // Calculate totals
                        currentRow++;

                        worksheet.Cell(currentRow, 1).Value = "Total: ";
                        worksheet.Range(currentRow, 1, currentRow, 6).Merge().Style.Font.Bold = true;
                        worksheet.Range(currentRow, 1, currentRow, 6).Style.Fill.BackgroundColor = XLColor.Gray;
                        worksheet.Range(currentRow, 1, currentRow, 6).Style.Font.FontColor = XLColor.White;

                        worksheet.Cell(currentRow, 7).Value = data.CustomerPointRedeem.Sum(x => x.RedeemedValue).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 8).Value = data.CustomerPointRedeem.Sum(x => x.RedeemedAmount).ToString("#,##0.00");
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomerPointRedeemReport.xlsx");
                    }
                }
            }
            return null;
        }

        #endregion


        #region StationShift Summary
        #region Shift Pump Reading
        [HttpGet]
        public IActionResult ShiftPumpReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshiftpumpreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshiftpumpreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Shift Tank Reading
        [HttpGet]
        public IActionResult ShiftTankReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshifttankreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshifttankreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Shift Lube Lpg Reading
        [HttpGet]
        public IActionResult ShiftlubepgReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshiftlubelpgreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshiftlubelpgreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Shift Expenses Reading
        [HttpGet]
        public IActionResult ShiftExpensesReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshiftexpensesreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshiftexpensesreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Shift Expenses Reading
        [HttpGet]
        public IActionResult ShiftCreditSaleReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshiftcreditsalereadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshiftcreditsalereadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Shift Cash Drops Reading
        [HttpGet]
        public IActionResult ShiftCashDropReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshiftcashdropreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshiftcashdropreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Shift Purchases Reading
        [HttpGet]
        public IActionResult ShiftPurchasesReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generateshiftpurchasesreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generateshiftpurchasesreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Station Shift Reading
        [HttpGet]
        public IActionResult StationShiftReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generatestationshiftreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generatestationshiftreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion

        #region Station Shift Summary Reading
        [HttpGet]
        public IActionResult ShiftSummaryReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationshiftlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Systemstationshifts, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generatestationshiftsummaryreadingdata(ReportParameters model)
        {
            var Resp = await bl.Generatestationshiftsummaryreadingdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion
        #region Station Shift Customer Statement Reading
        [HttpGet]
        public IActionResult ShiftCustomerStatementReading()
        {
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationattendantlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            ViewData["Stationcreditcustomerlists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCreditCustomers, SessionUserData.Usermodel.Stations.FirstOrDefault().StationId).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return PartialView();
        }
        public async Task<JsonResult> Generatestationshiftcustomerstatementreportdata(ReportParameters model)
        {
            var Resp = await bl.Generatestationshiftcustomerstatementreportdata(SessionUserData.Token, model);
            return Json(Resp);
        }
        #endregion
        #endregion






        #region Other Methods
        protected PdfPTable Add_Content_To_PDF(PdfPTable tableLayout, SingleFinanceTransactions data)
        {
            float[] headers = { 50, 30, 30, 40 };
            tableLayout.SetWidths(headers); //Set the pdf headers  
            tableLayout.WidthPercentage = 35; //Set the PDF File witdh percentage  
            tableLayout.WidthPercentage = 35; //Set the PDF File witdh percentage  
            tableLayout.HeaderRows = 1;
            var count = 1;
            //Add header  
            AddCellToHeader(tableLayout, "PRODUCT");
            AddCellToHeader(tableLayout, "UNIT");
            AddCellToHeader(tableLayout, "PRICE");
            AddCellToHeader(tableLayout, "AMOUNT");

            foreach (var item in data.Ticketlines)
            {
                if (count >= 1)
                {
                    //Add body  
                    AddCellToBody(tableLayout, item.Productvariationname, count);
                    AddCellToBody(tableLayout, item.Units.ToString("#,##0.00"), count);
                    AddCellToBody(tableLayout, item.Price.ToString("#,##0.00"), count);
                    AddCellToBody(tableLayout, item.SaleAmount.ToString("#,##0.00"), count);
                    count++;
                }
            }
            AddCellToFooter(tableLayout, "SUBTOTAL:", data.SubTotal.ToString("#,##0.00"));
            AddCellToFooter(tableLayout, "TOTAL:", data.Total.ToString("#,##0.00"));
            AddCellToFooter(tableLayout, "NET TOTAL:", data.NetTotal.ToString("#,##0.00"));
            return tableLayout;
        }
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 8, 1, BaseColor.DarkGray)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3,
                BackgroundColor = new BaseColor(255, 255, 255)
            });
        }

        private static void AddCellToBody(PdfPTable tableLayout, string cellText, int count)
        {
            if (count % 2 == 0)
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 7, 1, BaseColor.DarkGray)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 3,
                    BackgroundColor = new BaseColor(255, 255, 255)
                });
            }
            else
            {
                tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 7, 1, BaseColor.DarkGray)))
                {
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Padding = 3,
                    BackgroundColor = new BaseColor(255, 255, 255)
                    //BackgroundColor = new BaseColor(211, 211, 211)
                });
            }
        }
        private static void AddCellToFooter(PdfPTable tableLayout, string label, string value)
        {
            PdfPCell cell = new PdfPCell(new Phrase(label, new Font(Font.TIMES_ROMAN, 8, 1, BaseColor.DarkGray)))
            {
                Colspan = 3, // Column span for the label
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 3,
                BackgroundColor = new BaseColor(255, 255, 255)
            };
            tableLayout.AddCell(cell);

            PdfPCell valueCell = new PdfPCell(new Phrase(value, new Font(Font.TIMES_ROMAN, 8, 1, BaseColor.DarkGray)))
            {
                Colspan = 1, // Column span for the value
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 3,
                BackgroundColor = new BaseColor(255, 255, 255)
            };
            tableLayout.AddCell(valueCell);
        }
        #endregion

        #region drop down data by Id
        [HttpGet]
        public JsonResult GetSystemDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetSystemDropDownAgreementDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemCustomerAgreement, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetSystemDropDownAgreementAccountDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemAgreementAccount, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetSystemStationStaffDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, ListModelType.SystemStationStaff, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }

        private void PopulateRow(ExcelWorksheet ws, int rowIndex, string[] rowData)
        {
            for (int i = 0; i < rowData.Length; i++)
            {
                ws.Cells[rowIndex, i + 1].Value = rowData[i];
            }
        }
        #endregion
    }
}

