#pragma checksum "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "380d0737f097d0b77e05ef9057260617a146f174"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Renter_Details), @"mvc.1.0.view", @"/Views/Renter/Details.cshtml")]
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
#line 1 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.ViewModels.RenterRoomVM;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using SkyKotApp.Data.Default;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\_ViewImports.cshtml"
using KotClassLibrary.Helpers;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"380d0737f097d0b77e05ef9057260617a146f174", @"/Views/Renter/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b663632127d798b9031fd6db3064819e3692e33", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Renter_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<RenterRoom>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Pay", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<h3>Your Rooms</h3>\r\n\r\n<div class=\"card\" style=\"width: 30rem;\">\r\n    <div class=\"card-img-top\">\r\n");
#nullable restore
#line 7 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
         if (Model.Room.RoomImages.Any())
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n");
#nullable restore
#line 10 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                  var carouselId = "carousel-" + Model.Room.RoomId;

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div");
            BeginWriteAttribute("id", " id=\"", 281, "\"", 297, 1);
#nullable restore
#line 11 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
WriteAttributeValue("", 286, carouselId, 286, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"carousel slide col-12 p-0\" data-ride=\"carousel\">\r\n                    <div class=\"carousel-inner\">\r\n");
#nullable restore
#line 13 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                          var teller3 = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                         foreach (var image in Model.Room.RoomImages)
                        {
                            var img = "/images/Room/" + (image.Path ?? "default_room.jpg");
                            if (teller3 == 0)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"carousel-item active\"");
            BeginWriteAttribute("style", " style=\"", 785, "\"", 905, 10);
            WriteAttributeValue("", 793, "cursor:pointer;", 793, 15, true);
            WriteAttributeValue(" ", 808, "height:", 809, 8, true);
            WriteAttributeValue(" ", 816, "30vh;", 817, 6, true);
            WriteAttributeValue(" ", 822, "background-image", 823, 17, true);
            WriteAttributeValue(" ", 839, ":", 840, 2, true);
            WriteAttributeValue(" ", 841, "url(\'", 842, 6, true);
#nullable restore
#line 19 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
WriteAttributeValue("", 847, img, 847, 4, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 851, "\');background-position:", 851, 23, true);
            WriteAttributeValue(" ", 874, "center;background-size:", 875, 24, true);
            WriteAttributeValue(" ", 898, "cover;", 899, 7, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"modal\" data-target=\"#Modal\">\r\n                                </div>\r\n");
#nullable restore
#line 21 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"carousel-item\"");
            BeginWriteAttribute("style", " style=\"", 1148, "\"", 1268, 10);
            WriteAttributeValue("", 1156, "cursor:pointer;", 1156, 15, true);
            WriteAttributeValue(" ", 1171, "height:", 1172, 8, true);
            WriteAttributeValue(" ", 1179, "30vh;", 1180, 6, true);
            WriteAttributeValue(" ", 1185, "background-image", 1186, 17, true);
            WriteAttributeValue(" ", 1202, ":", 1203, 2, true);
            WriteAttributeValue(" ", 1204, "url(\'", 1205, 6, true);
#nullable restore
#line 24 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
WriteAttributeValue("", 1210, img, 1210, 4, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1214, "\');background-position:", 1214, 23, true);
            WriteAttributeValue(" ", 1237, "center;background-size:", 1238, 24, true);
            WriteAttributeValue(" ", 1261, "cover;", 1262, 7, true);
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"modal\" data-target=\"#Modal\">\r\n                                </div>\r\n");
#nullable restore
#line 26 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                            }
                            teller3++;

                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                    <a class=\"carousel-control-prev\"");
            BeginWriteAttribute("href", " href=\"", 1533, "\"", 1552, 2);
            WriteAttributeValue("", 1540, "#", 1540, 1, true);
#nullable restore
#line 31 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
WriteAttributeValue("", 1541, carouselId, 1541, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" role=""button"" data-slide=""prev"">
                        <span class=""carousel-control-prev-icon"" aria-hidden=""true""></span>
                        <span class=""sr-only"">Previous</span>
                    </a>
                    <a class=""carousel-control-next""");
            BeginWriteAttribute("href", " href=\"", 1822, "\"", 1841, 2);
            WriteAttributeValue("", 1829, "#", 1829, 1, true);
#nullable restore
#line 35 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
WriteAttributeValue("", 1830, carouselId, 1830, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" role=""button"" data-slide=""next"">
                        <span class=""carousel-control-next-icon"" aria-hidden=""true""></span>
                        <span class=""sr-only"">Next</span>
                    </a>
                </div>
            </div>
");
#nullable restore
#line 41 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
        }
        else
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <div style=\"cursor:pointer; height: 30vh; background-image : url(\'/images/Room/default_room.jpg\');background-position: center;background-size: cover;\">\r\n\r\n            </div>\r\n");
#nullable restore
#line 48 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n    <div class=\"card-body\">\r\n        <div class=\"media\">\r\n            <div class=\"media-body\">\r\n                <h6 class=\"my-0 text-white d-block\">");
#nullable restore
#line 54 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                                               Write(Model.Room.House.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                <div>\r\n                    <p>House Name : ");
#nullable restore
#line 56 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                               Write(Model.Room.House.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p>Price(Montly) : ");
#nullable restore
#line 57 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                                      Write(Model.AmountToPay);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <p>Room Number : ");
#nullable restore
#line 58 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                                Write(Model.Room.RoomNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    <div>From ");
#nullable restore
#line 59 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                         Write(Model.StartDate.ToLongDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral(" To ");
#nullable restore
#line 59 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                                                                Write(Model.EndDate.ToLongDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</div>
                </div>
            </div>
        </div>
        <div>
            <a class=""btn btn-info"" style=""cursor:pointer"">Details</a>
        </div>
    </div>
</div>
<table class=""table"">
      <thead>
            <tr>
              <th scope=""col"">From</th>
              <th scope=""col"">To</th>
              <th scope=""col"">Pay</th>
            </tr>
      </thead>
      <tbody>
");
#nullable restore
#line 77 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
           foreach(var contract in Model.RenterContracts)
          {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n              <td>");
#nullable restore
#line 80 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
             Write(contract.StartDate.ToLongDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n              <td>");
#nullable restore
#line 81 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
             Write(contract.StartDate.AddMonths(1).AddDays(-1).ToLongDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n");
#nullable restore
#line 82 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                 if (!contract.IsPayed)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "380d0737f097d0b77e05ef9057260617a146f17415697", async() => {
                WriteLiteral("\r\n                            <input name=\"renterContractId\"");
                BeginWriteAttribute("value", " value=\"", 3712, "\"", 3746, 1);
#nullable restore
#line 86 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
WriteAttributeValue("", 3720, contract.RenterContractId, 3720, 26, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"number\" hidden>\r\n                            <input class=\"btn btn-danger\" type=\"submit\" value=\"Pay\">\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </td>\r\n");
#nullable restore
#line 90 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                }
                else
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <td>\r\n                        <a class=\"btn btn-primary\">Payed</a>\r\n                    </td>\r\n");
#nullable restore
#line 96 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tr>\r\n");
#nullable restore
#line 98 "C:\Users\mbark\Documents\Github\skykot\SkyKotApp\SkyKotApp\Views\Renter\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("      </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<RenterRoom> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
