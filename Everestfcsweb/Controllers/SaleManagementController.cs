using Everestfcsweb.Enum;
using Everestfcsweb.Entities;
using Everestfcsweb.Models;
using Everestfcsweb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using Everestfcsweb.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis;

namespace Everestfcsweb.Controllers
{
    [Authorize]
    [RequireHttps]
    public class SaleManagementController : BaseController
    {
        private readonly Fuelprodataservices bl;
        public SaleManagementController(IConfiguration config)
        {
            bl = new Fuelprodataservices(config);
        }

        #region Offline sales
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new ListModel { Text = x.Text, Value = x.Value, GroupId = x.GroupId, GroupName = x.GroupName }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            var data = await bl.Getallofflinesalesdata(SessionUserData.Token,SessionUserData.Usermodel.Tenantid);
            return View(data);
        }
        [HttpGet]
        public IActionResult CustomerOfflineSale()
        {
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new ListModel { Text = x.Text, Value = x.Value, GroupId = x.GroupId, GroupName = x.GroupName }).ToList();
            ViewData["Systemstationslists"] = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.Stations, SessionUserData.Usermodel.Tenantid).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return View();
        }
        [HttpGet]
        public IActionResult CustomerOfflineSalePaymode()
        {
            ViewData["Systempaymentmodeslists"] = bl.GetSystemDropDownData(SessionUserData.Token, ListModelType.Paymentmodes).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).Where(x => new[] {"Card", "IVoucher" }.Any(s => x.Text.Contains(s))).ToList();
            return PartialView();
        }

        public IActionResult ResetCart()
        {
            SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            HttpContext.Session.Clear();
            return Json(GetSaleItem().ProductList);
        }

        public JsonResult GetCustomerCustomerOfflineSale()
        {
            String[] str = new String[] { "", "" };
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart.Count() > 0)
            {
                str[0] = GetSaleItem().ProductList;
            }
            return Json(str[0]);
        }
        public IActionResult AddProductdatatocart(ProductModel model)
        {
            String[] str = new String[] { "", "" };
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem { Product = model, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
                int index = isExist(model.ProductVariationId);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = model, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            str[0] = GetSaleItem().ProductList;

            return Json(str[0]);
        }
        //get items in cart
        public SaleDetail GetSaleItem()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            SaleDetail sDetail = new SaleDetail();
            string ProductList = "";
            decimal SubTotal = 0;
            decimal TotalItem = 0;

            if (cart != null)
            {
                ProductList += "<table class=\"table table-bordered table-sm table-compact table-striped font-weight-bold\" style=\"font-size:12px;\"><thead><tr><th hidden></th><th width=\"24%\" class=\"name\" >Product</th><th width=\"20%\">Price</th><th width=\"15%\">Qty</th><th width=\"20%\">Amount</th><th width=\"10%\">Remove</th></tr></thead><tbody>";
                foreach (var t in cart)
                {
                    var price = t.Product.ProductPrice;
                    string SellRate = price.ToString();
                    string Total = (Convert.ToDecimal(SellRate) * t.Quantity).ToString();
                    ProductList += string.Format("<tr class=\"font-weight-bold p-0\" style=\"height:4px !important;font-size: 12px !important;\"><td hidden>{2}</td><td class=\"name\">{0}</td><td>{5}</td><td><input style=\"height:unset;padding:2px;width:14p%;font-size: 12px !important;\" type=\"number\" class=\"form-control qty\" onchange=\"UpdateSaleItem('{2}',this.value)\" value=\"{1}\"></td><td><input style=\"height:unset;padding:2px;width:17p%;font-size: 12px !important;\" type=\"number\" class=\"form-control\" onchange=\"UpdateAmountItem('{2}',this.value)\"  value=\"{4}\"></td><td style=\"text-align: center;\"><a onclick=\"DeleteSaleItem('{2}')\" class=\"delete\"><i class=\"fa fa-trash text-danger\"></i></a></td></tr>", t.Product.ProductName, Math.Round(t.Quantity, 2), t.Product.ProductVariationId, t.Product.ProductCode, Math.Round(t.Quantity * t.Product.ProductPrice, 2), t.Product.ProductPrice);
                    SubTotal += Convert.ToDecimal(Total);
                    TotalItem += t.Quantity;

                }
                ProductList += string.Format("</tbody>");
                ProductList += "</tbody></table>";
            }
            if (ProductList == "")
            {
                ProductList += string.Format("<table class=\"table table-bordered table-sm table-compact table-striped font-weight-bold\" style=\"font-size:12px;\"><thead><tr><th hidden></th><th width=\"24%\" class=\"name\" >Product</th><th width=\"20%\">Price</th><th width=\"15%\">Qty</th><th width=\"20%\">Amount</th><th width=\"10%\">Remove</th></tr></thead><tbody>");
            }
            sDetail.ProductList = ProductList;
            return sDetail;
        }

        public async Task<IActionResult> GetCardDetails(PostcardDetails model)
        {
            model.TenantId = SessionUserData.Usermodel.Tenantid;
            decimal balance = 0;
            hty ty = new hty();
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            var t = await bl.GetCustomerCardDetails(SessionUserData.Token, model);
            if (t.RespStatus != 0)
            {
                return Json(t.RespMessage);
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder().Append("<div>");
                stringBuilder.Append(string.Format(@"CUSTOMER: <b>{0}</b><br/>AGREEMENT: <b>{1}</b><br/>ACCOUNT NO: <b>{2}</b><br/>MASK: <b>{3}</b><br/>", t.CustomerAccountCardDetail.CustomerName, t.CustomerAccountCardDetail.Agreementtypename, t.CustomerAccountCardDetail.AccountNumber, t.CustomerAccountCardDetail.CardSNO));
                if (model.PaymantModeName.ToLower().Contains("card"))
                {
                    if (t.CustomerAccountCardDetail.Agreementtypename.ToLower().Contains("prepaid"))
                    {
                        balance = t.CustomerAccountCardDetail.CustomerAccountBalance;
                        stringBuilder.Append(string.Format("ACCOUNT BALANCE: <b>{0}. {1}</b><br/>", t.CustomerAccountCardDetail.Currency, t.CustomerAccountCardDetail.CustomerAccountBalance.ToString("#,##0.00")));
                    }
                    else
                    {
                        balance = t.CustomerAccountCardDetail.CustomerAccountBalance > t.CustomerAccountCardDetail.CustomerBalance ? t.CustomerAccountCardDetail.CustomerBalance : t.CustomerAccountCardDetail.CustomerAccountBalance;
                        stringBuilder.Append(string.Format("CARD LIMIT: <b>{0}. {1}</b><br/>CUSTOMER BALANCE: <b>{2}. {3}</b><br/>", t.CustomerAccountCardDetail.Currency, t.CustomerAccountCardDetail.CustomerAccountBalance.ToString("#,##0.00"), t.CustomerAccountCardDetail.Currency, t.CustomerAccountCardDetail.CustomerBalance.ToString("#,##0.00")));
                    }
                }
                else if (model.PaymantModeName.ToLower().Contains("voucher"))
                {
                    if (t.CustomerAccountCardDetail.CustomerAccountRewards.FirstOrDefault(r => r.RewardName == "Points") != null) { stringBuilder.Append(string.Format("REWARD BALANCE: <b>{0}</b><br/>", t.CustomerAccountCardDetail.CustomerAccountRewards.FirstOrDefault(r => r.RewardName == "Points").RewardRedeemValue.ToString("#,##0.00"))); balance = t.CustomerAccountCardDetail.CustomerAccountRewards.FirstOrDefault(r => r.RewardName == "Points").RewardRedeemValue; }
                    else { stringBuilder.Append("VOUCHER BALANCE: <b>0</b><br/>"); balance = 0; }
                }
                List<CustomerAccountEquipmentItem>? CustomerAccountEquipment = new List<CustomerAccountEquipmentItem>();
                if (t.CustomerAccountCardDetail.CustomerAccountEquipment !=null)
                {
                    CustomerAccountEquipment= t.CustomerAccountCardDetail.CustomerAccountEquipment;
                }
                else
                {
                    CustomerAccountEquipment=new List<CustomerAccountEquipmentItem>();
                }
                ty = new hty { Uid = t.CustomerAccountCardDetail.CardUID, CustomerAccountEquipment = CustomerAccountEquipment, Agreementtypename = t.CustomerAccountCardDetail.Agreementtypename, Quantity = cart.Sum(r => ((r.Quantity))), Balance = balance, TotalTicket = cart.Sum(r => ((r.Quantity * r.Product.ProductPrice) - r.Product.ProductDiscount)), AccountId = t.CustomerAccountCardDetail.AccountId, AccountNumber = t.CustomerAccountCardDetail.AccountNumber, output = stringBuilder.Append("</div>").ToString() };
                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        var prt = t.CustomerAccountCardDetail.CustomerAccountProducts.FirstOrDefault(r => r.ProductvariationId==item.Product.ProductVariationId);
                        if (prt!=null)
                        {
                            item.Product.ProductPrice = prt.ProductPrice;
                            item.Product.ProductDiscount = prt.Discountvalue;
                        }
                    }
                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(ty);
        }
        public IActionResult GetSaleItems()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return Json(GetSaleItemDetails().ProductList);
        }
        public SaleDetail GetSaleItemDetails()
        {
            SaleDetail sDetail = new SaleDetail();
            String ProductList = ""; decimal tatolprice = 0;
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ProductList += "<table class=\"table table-bordered table-sm table-compact table-striped font-weight-light\" id=\"sales-posted\" style=\"font-size:12px;\"><thead><tr><th hidden></th><th>Product</th><th style='text-align:right;'>Quantity</th><th style='text-align:right;'>Price</th><th style='text-align:right;'>Discount</th><th style='text-align:right;'>Amount</th></thead><tbody>";
                foreach (var t in cart)
                {
                    ProductList += String.Format("<tr class=\"text-sm font-weight-bold p-0\" style=\"height:4px !important;font-size: 12px !important;\"><td hidden>{2}</td><td>{0}</td><td style='text-align:right;'>{1}</td><td style='text-align:right;'>{3}</td><td style='text-align:right;'>{4}</td><td style='text-align:right;'>{5}</td></tr>", t.Product.ProductName, Math.Round(t.Quantity, 3), t.Product.ProductVariationId, Math.Round(t.Product.ProductPrice, 2), Math.Round(t.Quantity * t.Product.ProductDiscount, 2), Math.Round(t.Quantity * (t.Product.ProductPrice - t.Product.ProductDiscount), 2), t.Product.ProductVariationId);
                    tatolprice += Math.Round(t.Quantity * (t.Product.ProductPrice - t.Product.ProductDiscount), 2);
                }
                ProductList += String.Format("<tr class=\"font-weight-bold\" style='font-size: 12px;'><td colspan='4'>TOTAL<input id='tt-app' value='" + tatolprice + "' hidden /></td><td id='total-amount-pay' style='text-align:right;'>" + tatolprice + "</td></tr>");
                ProductList += String.Format("</tbody>");
                ProductList += "</tbody></table>";
            }
            sDetail.ProductList = ProductList;
            return sDetail;
        }

        //Delete Sale Item
        public IActionResult DeleteSaleItem(long ProductVariationId)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(ProductVariationId);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(GetSaleItem().ProductList);
        }
        //Update Sale Item
        public IActionResult UpdateSaleItem(long ProductVariationId, decimal Qty)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(ProductVariationId);
            if (Qty <= 0)
            {
                cart.RemoveAt(index);
            }
            if (index != -1)
            {
                cart[index].Quantity=Qty;
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(GetSaleItem().ProductList);
        }
        public IActionResult UpdateAmountItem(long ProductVariationId, decimal Amt)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            int index = isExist(ProductVariationId);
            if (Amt <= 0)
            {
                cart.RemoveAt(index);
            }
            if (index != -1)
            {
                cart[index].Quantity = Amt / cart[index].Product.ProductPrice;
                //item.Price = Amt / item.Quantity
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return Json(GetSaleItem().ProductList);
        }
        private int isExist(long id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductVariationId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
        public async Task<IActionResult> RegisterOfflineSale(SalesTransactionRequest model)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            List<SaleTicketLines> ticketlines = new List<SaleTicketLines>();
            foreach (var item in cart)
            {
                ticketlines.Add(new SaleTicketLines() { ProductVariationDiscount = item.Product.ProductDiscount * item.Quantity, ProductVariationId = item.Product.ProductVariationId, ProductVariationPrice = item.Product.ProductPrice, TotalMoneySold = item.Quantity * item.Product.ProductPrice, ProductvariationUnits = item.Quantity, CreatedbyId = model.CreatedbyId, Createdby = model.Createdby, DateCreated = model.Datecreated });
            }
            model.TicketLines = ticketlines;
            model.TotalMoneySold = ticketlines.Sum(r => ((r.ProductvariationUnits * r.ProductVariationPrice) - r.ProductVariationDiscount));
            model.PaymentList = new List<SalePaymentList>() { new SalePaymentList { PaymentModeId = model.PaymentModeId, MpesaCode = "", MpesaMSISDN = "", TotalPaid = model.Amount, TotalUsed = ticketlines.Sum(r => ((r.ProductvariationUnits * r.ProductVariationPrice) - r.ProductVariationDiscount)), DateCreated = model.Datecreated } };
            string outp = JsonConvert.SerializeObject(model);
            HttpContext.Session.SetObjectAsJson("cart", cart);
            var resp = await bl.PostSaleTransactionData(SessionUserData.Token,model);
            HttpContext.Session.SetObjectAsJson("cart", cart);
            return Json(resp);
        }
        [HttpPost]
        public async Task<IActionResult> ReverseSale(ReverseSaleRequestData model)
        {
            var resp = await bl.PostReverseSaleTransactionData(SessionUserData.Token, model);
            return Json(resp);
        }
        
        #endregion


        #region drop down data
        [HttpGet]
        public JsonResult GetSystemDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, ListModelType.SystemStationStaff, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        [HttpGet]
        public JsonResult GetSystemStationStaffDropDownDataById(long Id)
        {
            var Resp = bl.GetSystemDropDownDataById(SessionUserData.Token, SessionUserData.Usermodel.Tenantid, ListModelType.SystemStationStaff, Id).Result.Select(x => new SelectListItem { Text = x.Text, Value = x.Value }).ToList();
            return Json(Resp);
        }
        #endregion

    }
}
