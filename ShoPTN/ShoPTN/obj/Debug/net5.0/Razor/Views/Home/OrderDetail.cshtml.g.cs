#pragma checksum "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b9a497bd6a937c59df6a25dd589ae2e71d68564f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_OrderDetail), @"mvc.1.0.view", @"/Views/Home/OrderDetail.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b9a497bd6a937c59df6a25dd589ae2e71d68564f", @"/Views/Home/OrderDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f5321e2c643f6c5a4b8cd8dc803ad010bd422460", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_OrderDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<ShoPTN.Models.DatHangChiTiet>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("width", new global::Microsoft.AspNetCore.Html.HtmlString("300"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("height", new global::Microsoft.AspNetCore.Html.HtmlString("300"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("col-4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
  
    ViewBag.Title = "Đơn chi tiết";
    // danh sách Thongtinkythuat

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"card mt-3 shadow border border-info\">\r\n    <h5 class=\"card-header bg-info text-white text-center font-weight-bold mt-3\">");
#nullable restore
#line 8 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                                            Write(ViewBag.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    <div class=\"card-body\">\r\n        <div class=\"card-deck\">\r\n");
#nullable restore
#line 11 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
             foreach (var item in Model)
            {
                // truy xuất giữa các bảng

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-12\">\r\n                    <div class=\"row mt-3\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "b9a497bd6a937c59df6a25dd589ae2e71d68564f5210", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 4, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 562, "~/", 562, 2, true);
#nullable restore
#line 16 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
AddHtmlAttributeValue("", 564, item.LapTop.FolderName.Replace("wwwroot/",""), 564, 46, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 610, "/", 610, 1, true);
#nullable restore
#line 16 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
AddHtmlAttributeValue("", 611, item.LapTop.HinhAnh, 611, 20, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "alt", 1, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 16 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
AddHtmlAttributeValue("", 638, item.LapTop.TenSanPham, 638, 23, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        <div class=\"card-body col-7\">\r\n                            <h5 class=\"text-center\">Mã ID: ");
#nullable restore
#line 18 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                      Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <h5 class=\"font-weight-light\">Mã Đặt hàng: ");
#nullable restore
#line 19 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                                  Write(item.DatHangId);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <hr />\r\n                            <h5>Thông tin hóa đơn</h5>\r\n                            <hr />\r\n                            <h5>Tên sản phẩm: <b style=\"font-weight:bold;\">");
#nullable restore
#line 23 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                                      Write(item.LapTop.TenSanPham);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h5>\r\n                            <h5 class=\"font-weight-light text-dark\">Đơn giá: <b style=\"font-weight:bold\">");
#nullable restore
#line 24 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                                                                    Write(item.DonGia.Value.ToString("N0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ</b></h5>\r\n                            <h5 class=\"font-weight-light text-dark\">Số lượng: <b style=\"font-weight:bold\">");
#nullable restore
#line 25 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                                                                     Write(item.SoLuong);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h5>\r\n                            <hr />\r\n                            <h5 class=\"font-weight-lighter text-right text-dark\">Thành tiền: <b>");
#nullable restore
#line 27 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
                                                                                           Write(item.ThanhTien.Value.ToString("N0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ</b></h5>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 31 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderDetail.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<ShoPTN.Models.DatHangChiTiet>> Html { get; private set; }
    }
}
#pragma warning restore 1591
