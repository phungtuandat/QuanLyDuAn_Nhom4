#pragma checksum "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b88dd613010107ad4b9f4a28a773a21e945c96ce"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Search), @"mvc.1.0.view", @"/Views/Home/Search.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\_ViewImports.cshtml"
using ShoPTN;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\_ViewImports.cshtml"
using ShoPTN.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b88dd613010107ad4b9f4a28a773a21e945c96ce", @"/Views/Home/Search.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f5321e2c643f6c5a4b8cd8dc803ad010bd422460", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Search : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ICollection<ShoPTN.Models.SanPham>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("card-img-top"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("300"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("300"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ChiTiet", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-size:20px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddToCart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Cart", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("font-size:15px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary ml-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary mt-2"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
  
    ViewBag.Title = "Tìm kiếm được ";
    var ListHangSx = (List<HangSx>)ViewBag.HangSx;
    var ListCate = (List<DanhMucCon>)ViewBag.CateChild;
    var ListTinhTrang = (List<TinhTrang>)ViewBag.TinhTrang;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    img {
        transition: transform .2s; /* Animation */
        margin: 0 auto;
    }
    img:hover {
        transform: scale(0.9);
    }
    img.card-img-top {
        width: 400px;
        height: 300px;
    }
    div.nameproduct {
        height: 110px;
    }
</style>
");
#nullable restore
#line 24 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"card border border-info mt-5 shadow\">\r\n        <h5 class=\"card-header bg-info text-white text-center\" style=\"font-size:20px;font-weight:bold;\">");
#nullable restore
#line 27 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                                   Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 27 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                                                  Write(Model.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        <div class=\"card-body\">\r\n            <div class=\"row\">\r\n");
#nullable restore
#line 30 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                 if (Model.Count == 0)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <span class=\"text-white badge badge-info m-auto\" style=\"font-size: 18px;\">Hiện tại không có sản phẩm</span>\r\n");
#nullable restore
#line 33 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                }
                else
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 36 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                     foreach (var item in Model.OrderByDescending(m => m.GiaBan))// sắp xếp giảm dần và lấy 4 sản ph
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <!--col-md-3 mỗi card chửa 3 cột-->\r\n                        <div class=\"card col-md-3 border border-0\">\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b88dd613010107ad4b9f4a28a773a21e945c96ce10196", async() => {
                WriteLiteral("\r\n                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "b88dd613010107ad4b9f4a28a773a21e945c96ce10484", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 4, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                AddHtmlAttributeValue("", 1535, "~/", 1535, 2, true);
#nullable restore
#line 41 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
AddHtmlAttributeValue("", 1537, item.FolderName.Replace("wwwroot/",""), 1537, 39, false);

#line default
#line hidden
#nullable disable
                AddHtmlAttributeValue("", 1576, "/", 1576, 1, true);
#nullable restore
#line 41 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
AddHtmlAttributeValue("", 1577, item.HinhAnh, 1577, 13, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 41 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
AddHtmlAttributeValue("", 1597, item.TenSanPham, 1597, 16, false);

#line default
#line hidden
#nullable disable
                EndAddHtmlAttributeValues(__tagHelperExecutionContext);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                               WriteLiteral(item.IdProduct);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                            <div class=\"card-body\">\r\n                                <div class=\"nameproduct\">\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b88dd613010107ad4b9f4a28a773a21e945c96ce15319", async() => {
#nullable restore
#line 45 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                                                                      Write(item.TenSanPham);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 45 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                       WriteLiteral(item.IdProduct);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </div>\r\n");
#nullable restore
#line 47 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                 if (item.GiaKhuyenMai == 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <p><span class=\"badge badge-info text-white shadow\" style=\"font-size:20px\">");
#nullable restore
#line 49 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                                          Write(item.GiaBan.ToString("N0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ</span></p>\r\n");
#nullable restore
#line 50 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <p class=\"mr-auto\">\r\n                                        <span class=\"badge badge-info text-white shadow\" style=\"font-size:20px\">\r\n                                            ");
#nullable restore
#line 55 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                       Write(item.GiaBan.ToString("N0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ\r\n                                            <span><strike class=\"text-white\" style=\"font-size:15px\">");
#nullable restore
#line 56 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                               Write(item.GiaKhuyenMai.Value.ToString("N0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ</strike></span>\r\n                                        </span>\r\n                                    </p>\r\n");
#nullable restore
#line 59 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                <p class=\"card-text badge badge-success mt-2\" style=\"font-size:15px;\">");
#nullable restore
#line 61 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                                 Write(item.HangSx.TenHang);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 62 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                 if (item.OutOfSock == 1)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <p class=\"card-text badge badge-danger mt-2\" style=\"font-size:15px;\">Hết hàng</p>\r\n");
#nullable restore
#line 65 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                }
                                else if (item.OutOfSock == 2)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <p class=\"badge badge-primary mt-2\" style=\"font-size:15px;\">Còn hàng</p>\r\n");
#nullable restore
#line 69 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <p class=\"badge badge-outline-warning mt-2\" style=\"font-size:15px;\">Sắp về</p>\r\n");
#nullable restore
#line 73 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <p class=\"card-text text-danger mt-2\">Số lượng: <b>");
#nullable restore
#line 74 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                              Write(item.SoLuong);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></p>
                                <!-- trạng thái còn hàng hết hàng-->
                                <!--một máy có thể có nhiều cấu hình nên một máy có nhiều tập hợp thông tin cấu hình-->
                                <hr />
                                <div class=""row ml-2"">
                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b88dd613010107ad4b9f4a28a773a21e945c96ce22809", async() => {
                WriteLiteral("\r\n                                        <input type=\"hidden\"");
                BeginWriteAttribute("value", " value=\"", 4262, "\"", 4285, 1);
#nullable restore
#line 80 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
WriteAttributeValue("", 4270, item.IdProduct, 4270, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" name=""id"" />
                                        <input type=""hidden"" value=""0"" name=""quanlity"" />
                                        <button type=""submit"" value=""Mua hàng"" class=""btn btn-danger"">Đặt hàng <i class=""fa fa-cart-plus text-white""></i></button>
                                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n                                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b88dd613010107ad4b9f4a28a773a21e945c96ce25228", async() => {
                WriteLiteral("Chi tiết <i class=\"fa fa-info text-white\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 85 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                       WriteLiteral(item.IdProduct);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n");
#nullable restore
#line 89 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 89 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                     
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 94 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"card border border-info mt-5 shadow\">\r\n        <h5 class=\"card-header bg-info text-white text-center\" style=\"font-size:20px;font-weight:bold;\">");
#nullable restore
#line 98 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
                                                                                                   Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        <div class=\"card-body\">\r\n            <p class=\"text-center text-danger\">Không có sản phẩm</p>\r\n        </div>\r\n    </div> \r\n");
#nullable restore
#line 103 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\Search.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b88dd613010107ad4b9f4a28a773a21e945c96ce29339", async() => {
                WriteLiteral("<< Trở về trang chủ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_11);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ICollection<ShoPTN.Models.SanPham>> Html { get; private set; }
    }
}
#pragma warning restore 1591
