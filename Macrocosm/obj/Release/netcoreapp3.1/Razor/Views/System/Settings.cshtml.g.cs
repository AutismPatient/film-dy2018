#pragma checksum "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c314c3b17b7d92e5b9e798afc53e291ee29d3347"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_System_Settings), @"mvc.1.0.view", @"/Views/System/Settings.cshtml")]
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
#line 1 "E:\team\GraduationTeam\Macrocosm\Views\_ViewImports.cshtml"
using Macrocosm;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\team\GraduationTeam\Macrocosm\Views\_ViewImports.cshtml"
using Macrocosm.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
using Macrocosm.Tool;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c314c3b17b7d92e5b9e798afc53e291ee29d3347", @"/Views/System/Settings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3f7ca097da82343e27fc1e87bd29fd5924c3473", @"/Views/_ViewImports.cshtml")]
    public class Views_System_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<System_Config>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "int", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "bool", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "string", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("settingsForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return false"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
  
    ViewData["Title"] = "Settings";
    Layout = "~/Views/Shared/AdminLayout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("<ol class=\"breadcrumb pl-0\">\r\n    <li>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 196, "\"", 232, 1);
#nullable restore
#line 9 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 203, Url.Action("index","system"), 203, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <i class=\"material-icons\">home</i> Home\r\n        </a>\r\n    </li>\r\n    <li>\r\n        <a");
            BeginWriteAttribute("href", " href=\"", 334, "\"", 370, 1);
#nullable restore
#line 14 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 341, Url.Action("index","system"), 341, 29, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
            <i class=""material-icons"">library_books</i> 工作台
        </a>
    </li>
    <li class=""active"">
        <i class=""material-icons"">archive</i> 设置
    </li>
</ol>
<div class=""card"">
    <div class=""header"">
        <h2>爬虫任务常用设置 <small>*添加完配置需要加代码以实现*</small></h2>
        <ul class=""header-dropdown m-r--5"">
            <li>
                <button type=""button"" id=""add-config"" style=""margin-top: -12px;"" class=""btn btn-success waves-effect"" data-toggle=""modal"" data-target=""#SettingsModal"">添加配置</button>
            </li>
            <li class=""dropdown"">
                <a href=""javascript:void(0);"" class=""dropdown-toggle"" data-toggle=""dropdown"" role=""button"" aria-haspopup=""true"" aria-expanded=""false"">
                    <i class=""material-icons"">more_vert</i>
                </a>
                <ul class=""dropdown-menu pull-right"">
                    <li><a");
            BeginWriteAttribute("href", " href=\"", 1272, "\"", 1311, 1);
#nullable restore
#line 34 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 1279, Url.Action("Settings","system"), 1279, 32, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\" waves-effect waves-block\">刷新</a></li>\r\n                </ul>\r\n            </li>\r\n        </ul>\r\n    </div>\r\n    <div class=\"body\">\r\n        <div class=\"row clearfix\">\r\n");
#nullable restore
#line 41 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
             foreach (var m in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-6\">\r\n                    <b class=\"text-trun\"");
            BeginWriteAttribute("title", " title=\"", 1623, "\"", 1638, 1);
#nullable restore
#line 44 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 1631, m.Desc, 1631, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 44 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                                                    Write(m.Desc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n");
#nullable restore
#line 45 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                     switch (m.DataType)
                    {
                        case "string":

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"form-group\">\r\n                                <div class=\"form-line\">\r\n                                    <input type=\"text\" class=\"form-control\" data-id=\"");
#nullable restore
#line 50 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                                                                                Write(m.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("value", " value=\"", 1960, "\"", 1976, 1);
#nullable restore
#line 50 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 1968, m.Value, 1968, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 53 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                            break;
                        case "int":

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"form-group\">\r\n                                <div class=\"form-line\">\r\n                                    <input type=\"number\" class=\"form-control\" data-id=\"");
#nullable restore
#line 57 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                                                                                  Write(m.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("value", " value=\"", 2333, "\"", 2349, 1);
#nullable restore
#line 57 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 2341, m.Value, 2341, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 60 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                            break;
                        case "bool":

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"switch\" style=\"padding-top:15px;\">\r\n                                <label>关<input type=\"checkbox\" ");
#nullable restore
#line 63 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                                                           Write(m.Value == "true" ? "checked" : "");

#line default
#line hidden
#nullable disable
            WriteLiteral(" data-id=\"");
#nullable restore
#line 63 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                                                                                                         Write(m.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"><span class=\"lever switch-col-green\"></span>开</label>\r\n                            </div>\r\n");
#nullable restore
#line 65 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                            break;
                        default:

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"form-group\">\r\n                                <div class=\"form-line\">\r\n                                    <input type=\"text\" class=\"form-control\" data-id=\"");
#nullable restore
#line 69 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                                                                                Write(m.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"");
            BeginWriteAttribute("value", " value=\"", 3059, "\"", 3075, 1);
#nullable restore
#line 69 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
WriteAttributeValue("", 3067, m.Value, 3067, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 72 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
                            break;
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n");
#nullable restore
#line 75 "E:\team\GraduationTeam\Macrocosm\Views\System\Settings.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        </div>
    </div>
</div>
<div class=""modal fade"" id=""SettingsModal"" tabindex=""-1"" role=""dialog"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""defaultModalLabel"">请填写信息</h4>
            </div>
            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c314c3b17b7d92e5b9e798afc53e291ee29d334714266", async() => {
                WriteLiteral("\r\n                <input type=\"hidden\" name=\"adminId\"");
                BeginWriteAttribute("value", " value=\"", 3709, "\"", 3717, 0);
                EndWriteAttribute();
                WriteLiteral(@" />
                <div class=""modal-body"">
                    <div class=""row clearfix"">
                        <div class=""col-sm-6"">
                            <div class=""form-group"">
                                <div class=""form-line"">
                                    <input type=""text"" name=""Key"" class=""form-control"" placeholder=""配置键值"" required>
                                </div>
                            </div>
                        </div>
                        <div class=""col-sm-6"">
                            <div class=""form-group"">
                                <div class=""form-line"">
                                    <input type=""text"" name=""Value"" class=""form-control"" placeholder=""值"" required>
                                </div>
                            </div>
                        </div>
                        <div class=""col-sm-12"">
                            <div class=""form-group"">
                                <div class=""form-line"">
  ");
                WriteLiteral(@"                                  <input type=""text"" name=""Desc"" class=""form-control"" placeholder=""描述"" required>
                                </div>
                            </div>
                        </div>
                        <div class=""col-sm-12"">
                            <p>
                                <b>数据类型:</b>
                            </p>
                            <div class=""form-group"">
                                <select class=""form-control"" style=""border-bottom: 1px solid #ddd;"" name=""DataType"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c314c3b17b7d92e5b9e798afc53e291ee29d334716443", async() => {
                    WriteLiteral("int");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c314c3b17b7d92e5b9e798afc53e291ee29d334717700", async() => {
                    WriteLiteral("bool");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c314c3b17b7d92e5b9e798afc53e291ee29d334718958", async() => {
                    WriteLiteral("string");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""modal-footer"">
                    <button type=""submit"" class=""btn btn-primary waves-effect"">保存更改</button>
                    <button type=""button"" class=""btn btn-default waves-effect"" data-dismiss=""modal"">取消</button>
                </div>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
        </div>
    </div>
</div>
<script>
    $(function () {
        var form = $('#settingsForm');
        form.validate({
            highlight: function (input) {
                $(input).parents('.form-line').addClass('error');
            },
            unhighlight: function (input) {
                $(input).parents('.form-line').removeClass('error');
            },
            errorPlacement: function (error, element) {
                $(element).parents('.input-group').append(error);
                $(element).parents('.form-group').append(error);
            }
        });
        form.find(""button[type=submit]"").click(function (e) {
            if (!form.valid()) {
                return;
            }
            var button = $(this);
            $.ajax({
                async: true,
                url: ""/system/AddConfig"",
                data: form.serialize(),
                type: ""post"",
                dataType: 'json',
                beforeSend: function () {");
            WriteLiteral(@"
                    button.text(""Loading"");
                    button.attr(""disabled"", ""disabled"");
                },
                success: function (data) {
                    if (data.status_code === 200) {
                        nof(""alert-success"", data.msg);
                        setTimeout(function () { $(""#SettingsModal"").modal(""hide""); window.location.reload(true); }, 1500);
                    } else {
                        nof(""alert-danger"", data.msg);
                    }
                },
                timeout: 10000,
                error: function (data) {
                    nof(""alert-danger"", ""request error."");
                }
            });
            button.removeAttr(""disabled"");
            button.text(""保存更改"");
        });
        // 更改
        $("".card .body"").find(""input"").change(function (e) {
            var id = $(this).data(""id"");
            var val = $(this).val();
            var type = $(this).attr(""type"");
            if (type === """);
            WriteLiteral(@"checkbox"") { // 复选框
                if ($(this).is("":checked"")) {
                    val = ""true"";
                } else {
                    val = ""false"";
                }
            }
            edit(id, val,$(this));
        });
        function edit(id, val, e) {
            var button = e;
            $.ajax({
                async: true,
                url: ""/system/EditConfig"",
                data: {
                    id: id,
                    val: val,
                },
                type: ""post"",
                dataType: 'json',
                beforeSend: function () {
                    button.attr(""disabled"", ""disabled"");
                },
                success: function (data) {
                    if (data.status_code === 200) {
                        nof(""alert-success"", data.msg);
                    } else {
                        nof(""alert-danger"", data.msg);
                    }
                },
                timeout: 10000,
      ");
            WriteLiteral("          error: function (data) {\r\n                    nof(\"alert-danger\", \"request error.\");\r\n                }\r\n            });\r\n            button.removeAttr(\"disabled\");\r\n        }\r\n    })\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<System_Config>> Html { get; private set; }
    }
}
#pragma warning restore 1591