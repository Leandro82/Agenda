using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Deployment.Application;

namespace Teste
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        [STAThread]
        private static bool IsAppAlreadyRunning()
        {
            Process currentProcess = Process.GetCurrentProcess();

            if (Process.GetProcessesByName(currentProcess.ProcessName).Any(p => p.Id != currentProcess.Id && !p.HasExited))
            {
                return true;
            }

            return false;
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show("A nova versão do aplicativo não pode ser baixada no momento. \n\nVerifique sua conexão de rede ou tente novamente mais tarde. Error: " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show("Não é possível procurar por uma nova versão do aplicativo. A implantação do ClickOnce está corrompida. Por favor, reimplemente o aplicativo e tente novamente. Error: " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show("Este aplicativo não pode ser atualizado. Provavelmente não é um aplicativo ClickOnce. Error: " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;
                    if (!info.IsUpdateRequired && IsAppAlreadyRunning() == false)
                    {
                        var peq = new frmVersao(doUpdate);
                        if (Application.OpenForms.OfType<frmVersao>().Count() > 0)
                        {
                            Application.OpenForms[peq.Name].Focus();
                        }
                        else
                        {
                            peq.ShowDialog();
                        }
                    }
                    
                }
                else
                {
                    if (AuxClas.icone != "Ok" && IsAppAlreadyRunning() == false)
                    {
                        Principal pr = new Principal();
                        AuxClas.icone = "";
                        pr.Show();
                        pr.Atualizar();
                        Application.Run();
                    }
                    else
                    { 
                        Principal pr = new Principal();
                        AuxClas.icone = "Ok";
                        pr.Show();
                        pr.Atualizar();
                    }
                }

            }
        }
    }
}
