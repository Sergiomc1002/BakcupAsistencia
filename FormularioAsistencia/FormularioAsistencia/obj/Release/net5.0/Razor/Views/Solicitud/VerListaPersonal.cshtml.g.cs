#pragma checksum "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19d8f755eddaece58e061a941cc2c324de35be05"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Solicitud_VerListaPersonal), @"mvc.1.0.view", @"/Views/Solicitud/VerListaPersonal.cshtml")]
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
#line 1 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/_ViewImports.cshtml"
using FormularioAsistencia;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/_ViewImports.cshtml"
using FormularioAsistencia.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/_ViewImports.cshtml"
using FormularioAsistencia.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19d8f755eddaece58e061a941cc2c324de35be05", @"/Views/Solicitud/VerListaPersonal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d7cefccb54be67e80ce0f66323497a9456add0f2", @"/Views/_ViewImports.cshtml")]
    public class Views_Solicitud_VerListaPersonal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<VerListaPersonalViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Descargar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Solicitud", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "VerMas", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\n");
            WriteLiteral("\n");
#nullable restore
#line 4 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
  
    ViewBag.Title = "Solicitudes";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <h1>\n        Solicitudes de ");
#nullable restore
#line 9 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                  Write(User.Identity.Name.Split("@")[0]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"


    </h1>

<div class=""scrollit"">
    <table class=""table table-admin"">
        <thead>
            <tr>
                <th>
                    Curso
                </th>
                <th>
                    Cédula
                </th>
                <th>
                    Carné
                </th>
                <th>
                    Primer Apellido
                </th>
                <th>
                    Segundo Apellido
                </th>
                <th>
                    Nombre
                </th>
                <th>
                    Teléfono 1
                </th>
                <th>
                    Correo Electónico
                </th>
                <th>
                    Carrera
                </th>
                <th>
                    Nivel
                </th>
                <th>
                    N° Créditos
                </th>
                <th>

                    Ponderado


                </th>
                <th>
            ");
            WriteLiteral(@"        Tiene Cuenta OAF
                </th>
                <th>
                    N° de Cuenta
                </th>
                <th>
                    Banco
                </th>
                <th>
                    Horas Asistente
                </th>
                <th>
                    Horas Estudiante
                </th>
                <th>

                </th>
                <th>

                </th>

            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 82 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
             foreach (var item in Model.Solicitudes)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\n                <td>\n                    ");
#nullable restore
#line 86 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(Model.Asistencias.Where(x=>x.IdAsistencia==item.Asistencia).FirstOrDefault().Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 89 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Cedula);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 93 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Carne);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 97 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Apellido1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 101 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Apellido2);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 105 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 109 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Telefono1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 113 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.CorreoSolicitante);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 117 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.CarreraQueCursa);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n\n                <td>\n                    ");
#nullable restore
#line 121 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.Nivel);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n                    ");
#nullable restore
#line 124 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
               Write(item.NumeroDeCreditos);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n");
#nullable restore
#line 127 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                     if (item.PromedioRevisado == 0)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 130 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 133 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                   Write(item.PromedioRevisado);

#line default
#line hidden
#nullable disable
#nullable restore
#line 133 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                              
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n");
#nullable restore
#line 138 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                     if (item.CuentaBancaria)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-check\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 142 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-times\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 146 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\n                <td>\n");
#nullable restore
#line 149 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                     if (item.CuentaBancaria)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 153 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 156 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                   Write(item.NumeroDeCuenta);

#line default
#line hidden
#nullable disable
#nullable restore
#line 156 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                            
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\n\n                <td>\n");
#nullable restore
#line 161 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                     if (item.CuentaBancaria)
                    {


#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 165 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }
                    else
                    {
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 168 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                   Write(item.Banco);

#line default
#line hidden
#nullable disable
#nullable restore
#line 168 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                   
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\n                <td>\n");
#nullable restore
#line 172 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                     if (item.TieneHA)
                    {

                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 175 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                   Write(item.CantidadHA);

#line default
#line hidden
#nullable disable
#nullable restore
#line 175 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                        
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 180 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                </td>\n                <td>\n");
#nullable restore
#line 184 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                     if (item.TieneHE)
                    {

                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 187 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                   Write(item.CantidadHE);

#line default
#line hidden
#nullable disable
#nullable restore
#line 187 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                        
                    }
                    else
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <i class=\"fa fa-minus\" aria-hidden=\"true\"></i>\n");
#nullable restore
#line 192 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n                </td>\n\n                <td>\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19d8f755eddaece58e061a941cc2c324de35be0518136", async() => {
                WriteLiteral(" Descargar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 198 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                                                           WriteLiteral(item.SolicitudId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n                </td>\n                <td>\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "19d8f755eddaece58e061a941cc2c324de35be0520691", async() => {
                WriteLiteral(" Ver Más");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id_a", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 202 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
                                                                          WriteLiteral(item.SolicitudId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id_a"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id_a", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id_a"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n                </td>\n\n\n            </tr>\n");
#nullable restore
#line 207 "/home/sergio/SolicitudAsistencia/FormularioAsistencia/FormularioAsistencia/Views/Solicitud/VerListaPersonal.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\n\n    </table>\n</div>\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<VerListaPersonalViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
