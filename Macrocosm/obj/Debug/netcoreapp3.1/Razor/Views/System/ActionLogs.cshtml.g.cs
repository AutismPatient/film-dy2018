#pragma checksum "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1172c576ea6a306f4805cafc6f02055f79fe51b7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_System_ActionLogs), @"mvc.1.0.view", @"/Views/System/ActionLogs.cshtml")]
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
#line 1 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\_ViewImports.cshtml"
using Macrocosm;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\_ViewImports.cshtml"
using Macrocosm.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
using Macrocosm.Tool;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1172c576ea6a306f4805cafc6f02055f79fe51b7", @"/Views/System/ActionLogs.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3f7ca097da82343e27fc1e87bd29fd5924c3473", @"/Views/_ViewImports.cshtml")]
    public class Views_System_ActionLogs : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ResultModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
  
    ViewData["Title"] = "ActionLogs";
    Layout = "~/Views/Shared/AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<ol class=\"breadcrumb pl-0\">\r\n    <li>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 190, "\"", 226, 1);
#nullable restore
#line 9 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue("", 197, Url.Action("index","system"), 197, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <i class=\"material-icons\">home</i> Home\r\n        </a>\r\n    </li>\r\n    <li>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 328, "\"", 364, 1);
#nullable restore
#line 14 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue("", 335, Url.Action("index","system"), 335, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
            <i class=""material-icons"">library_books</i> 工作台
        </a>
    </li>
    <li class=""active"">
        <i class=""material-icons"">archive</i> 网站日志
    </li>
</ol>
<div class=""row clearfix"">
    <div class=""col-xs-12 col-sm-12 col-md-12 col-lg-12"">
        <div class=""card"">
            <div class=""header"">
                <h2>日志数据</h2>
                <ul class=""header-dropdown m-r--5"">
                    <li class=""dropdown"">
                        <a href=""javascript:void(0);"" class=""dropdown-toggle"" data-toggle=""dropdown"" role=""button"" aria-haspopup=""true"" aria-expanded=""false"">
                            <i class=""material-icons"">more_vert</i>
                        </a>
                        <ul class=""dropdown-menu pull-right"">
                            <li><a");
            BeginWriteAttribute("href", " href=\"", 1181, "\"", 1222, 1);
#nullable restore
#line 33 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue("", 1188, Url.Action("ActionLogs","system"), 1188, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""waves-effect waves-block"">刷新</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
            <div class=""body"">
                <div class=""table-responsive"">
                    <table class=""table table-hover dashboard-task-infos"">
                        <thead>
                            <tr>
                                <th>编号</th>
                                <th>请求地址</th>
                                <th>请求时间</th>
                                <th>请求IP（独立）</th>
                                <th>结果</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
");
#nullable restore
#line 52 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                             foreach (var key in (Model.Data as List<Action_logs>))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <tr>\r\n                                <td>");
#nullable restore
#line 55 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                               Write(key.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 56 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                               Write(key.Path);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 57 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                               Write(Convert.ToInt32(key.DateLine).UnixToLacalTime().ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 58 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                               Write(key.ClientIP);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                <td><span");
            BeginWriteAttribute("class", " class=\"", 2427, "\"", 2481, 2);
            WriteAttributeValue("", 2435, "label", 2435, 5, true);
#nullable restore
#line 59 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue(" ", 2440, key.Status == 1 ? "bg-green":"bg-red", 2441, 40, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 59 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                                                                                             Write(key.Status == 1 ? "正常" : "不通过");

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                                <td>\r\n                                    <a");
            BeginWriteAttribute("href", " href=\"", 2606, "\"", 2688, 1);
#nullable restore
#line 61 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue("", 2613, Url.Action("DeleteLogs","system",new { id = key.Id,page = Model.PageSize}), 2613, 75, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn bg-pink btn-block btn-xs waves-effect\">\r\n                                        <i class=\"material-icons\">warning</i> 删除\r\n                                    </a>\r\n                                </td>\r\n                            </tr>\r\n");
#nullable restore
#line 66 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                </div>\r\n                <nav>\r\n                    <ul class=\"pager\">\r\n                        <li class=\"previous \">\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 3200, "\"", 3274, 1);
#nullable restore
#line 73 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue("", 3207, Url.Action("ActionLogs","system",new {page = Model.PageSize - 1 }), 3207, 67, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 73 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                                                                                                      Write(Model.PageSize <= 1 ? "style=pointer-events:none;" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"waves-effect\"><span aria-hidden=\"true\">←</span> Older</a>\r\n                        </li>\r\n                        <li class=\"next\">\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 3505, "\"", 3579, 1);
#nullable restore
#line 76 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
WriteAttributeValue("", 3512, Url.Action("ActionLogs","system",new {page = Model.PageSize + 1 }), 3512, 67, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 76 "D:\Users\Administrator\worker\tfs-work\film-dy2018\Macrocosm\Views\System\ActionLogs.cshtml"
                                                                                                      Write(Model.Count < Model.PageSize ? "style=pointer-events:none;" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(" class=\"waves-effect\">Newer <span aria-hidden=\"true\">→</span></a>\r\n                        </li>\r\n                    </ul>\r\n                </nav>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ResultModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
