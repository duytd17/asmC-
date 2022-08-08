using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asm_c_sharp_3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmQuanLyNV());
           
        }
        //Scaffold-DbContext 'Data Source=DESKTOP-D08A7VN;Initial Catalog=FINALASS_FPOLYSHOP_SM22_BL2_DUNGNA29;Persist Security Info=True;User ID=duytdph20598;Password=123456' Microsoft.EntityFrameworkCore.SqlServer -OutputDir DomainClass -context FpolyDBContext -Contextdir Context -DataAnnotations -Force
    }
}
