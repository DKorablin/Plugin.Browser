using System.Reflection;
using System.Runtime.InteropServices;

[assembly: ComVisible(false)]
[assembly: Guid("7476853a-3a40-4d5f-a5b5-a00f1dc4d24c")]
[assembly: System.CLSCompliant(true)]

#if NETCOREAPP
[assembly: AssemblyMetadata("ProjectUrl", "https://dkorablin.ru/project/Default.aspx?File=86")]
#else

[assembly: AssemblyTitle("Plugin.Browser")]
[assembly: AssemblyDescription("Using ebedded Microsoft © Internet Explorer © brower in the Windows Forms application")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif
[assembly: AssemblyCompany("Danila Korablin")]
[assembly: AssemblyProduct("Web Browser Windows Plugin")]
[assembly: AssemblyCopyright("Copyright © Danila Korablin 2009-2024")]
#endif