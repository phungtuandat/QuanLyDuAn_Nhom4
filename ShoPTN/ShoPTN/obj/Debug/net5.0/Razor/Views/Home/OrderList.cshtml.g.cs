#pragma checksum "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dad17be7fa76253837b3d518766d671aba9023c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_OrderList), @"mvc.1.0.view", @"/Views/Home/OrderList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dad17be7fa76253837b3d518766d671aba9023c8", @"/Views/Home/OrderList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f5321e2c643f6c5a4b8cd8dc803ad010bd422460", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_OrderList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ICollection<ShoPTN.Models.DatHang>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""card mt-3 border-warning"">
    <h5 class=""card-header bg-warning"" style=""font-weight:bold;"">Đơn hàng</h5>
    <div class=""card-body"">
        <div class=""row"">
            <a class=""bg-primary ml-2 text-white"" style=""width:180px;line-height:40px;text-align:center;"">
                Đơn hàng mới <span class=""badge badge-light"">");
#nullable restore
#line 7 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                        Write(Model.Where(m => m.TinhTrang == 0).ToList().Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            </a>\r\n\r\n            <a class=\"bg-danger ml-2 text-white\" style=\"width:180px;line-height:40px;text-align:center;\">\r\n                Đơn hàng đã hủy <span class=\"badge badge-light\">");
#nullable restore
#line 11 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                           Write(Model.Where(m => m.TinhTrang == 2).ToList().Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            </a>\r\n\r\n            <a class=\"bg-info ml-2 text-white\" style=\"width:180px;line-height:40px;text-align:center;\">\r\n                Đơn hàng đã nhận <span class=\"badge badge-light\">");
#nullable restore
#line 15 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                            Write(Model.Where(m => m.TinhTrang == 9).ToList().Count());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n            </a>\r\n        </div>\r\n        <div class=\"row mt-3\">\r\n");
#nullable restore
#line 19 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
             foreach(var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-3\">\r\n                    <div class=\"card\">\r\n                        <h5 class=\"card-header\">");
#nullable restore
#line 23 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                           Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <div class=\"card-body\">\r\n                            <!---->\r\n                            <p class=\"card-text\">Khách hàng :");
#nullable restore
#line 26 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                        Write(item.KhachHang.HoVaTen);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Ngày đặt hàng :");
#nullable restore
#line 27 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                           Write(item.NgayDatHang);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text\">Tổng tiền: ");
#nullable restore
#line 28 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                       Write(item.TongTien.ToString("N0"));

#line default
#line hidden
#nullable disable
            WriteLiteral("đ</p>\r\n                            <hr />\r\n                            <div>\r\n");
#nullable restore
#line 31 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                 if (item.TinhTrang == 0)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-info shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đơn hàng mới</span>\r\n");
#nullable restore
#line 34 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 1)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-success shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đang xác nhận</span>\r\n");
#nullable restore
#line 38 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 2)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-danger shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đã hủy</span>\r\n");
#nullable restore
#line 42 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 3)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-warning shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đang đóng gói</span>\r\n");
#nullable restore
#line 46 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 4)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-warning shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đang gửi vận chuyển</span>\r\n");
#nullable restore
#line 50 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 5)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-primary shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đang chuyển</span>\r\n");
#nullable restore
#line 54 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 6)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-danger shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Thất bại</span>\r\n");
#nullable restore
#line 58 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 7)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-warning shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đang chuyển hoàn</span>\r\n");
#nullable restore
#line 62 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else if (item.TinhTrang == 8)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-success shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Đã chuyển hoàn</span>\r\n");
#nullable restore
#line 66 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <span class=\"badge badge-primary shadow\" style=\"line-height:25px;width:150px;text-align:center;font-size:15px;\">Thành công</span>\r\n");
#nullable restore
#line 70 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </div>\r\n                            <hr />\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dad17be7fa76253837b3d518766d671aba9023c812343", async() => {
                WriteLiteral("Hủy");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 73 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                                   WriteLiteral(item.Id);

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
            WriteLiteral("\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "dad17be7fa76253837b3d518766d671aba9023c814408", async() => {
                WriteLiteral("Chi tiết");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 74 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
                                                                    WriteLiteral(item.Id);

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
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 78 "D:\DoAnASP,NETCORE\ShoPTN\ShoPTN\Views\Home\OrderList.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n</div> ");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ICollection<ShoPTN.Models.DatHang>> Html { get; private set; }
    }
}
#pragma warning restore 1591