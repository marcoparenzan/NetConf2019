using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinFormsCoreLib.Controls;
using WinFormsCoreLib.Forms;
using WinFormsCoreLib.ControlsExtensions;

namespace WinFormIoTHubGateway
{
    public static partial class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = FormBase.New();
            form.IsMdiContainer = true;
            form.WindowState = FormWindowState.Maximized;
            form.Text = "IoTHub Gateway";
            form.Menu = new MainMenu(new[] {
                new MenuItem("&File", new[]{
                    new MenuItem("&WaveEditor", (s,e)=>{
                        var c = WaveEditor.CreateNew();
                        c.MdiParent = form;
                        c.Show();
                    }),
                    new MenuItem("-"),
                    new MenuItem("E&xit", (s,e)=>{
                        Application.Exit(); 
                    })
                })
            });

            Application.Run(form);
        }
    }
}