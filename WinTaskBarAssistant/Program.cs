using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinTaskBarAssistant
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TaskBar());
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // 어셈블리가 .exe 파일과 같은 폴더에 있으면 이 메서드는 호출되지 않는다.
            Assembly thAss = Assembly.GetExecutingAssembly();

            // Get Assembly Name
            var AssName = args.Name.Substring(0, args.Name.IndexOf(',')) + ".dll";
            //MessageBox.Show("AssName : " + AssName.ToString());

            var resources = thAss.GetManifestResourceNames().Where(s => s.EndsWith(AssName));
            if (resources.Count() > 0)
            {
                var resourceName = resources.First();

                //MessageBox.Show("Resource Name : " + resourceName.ToString());

                using (Stream stream = thAss.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        return null;
                    }

                    var block = new byte[stream.Length];
                    stream.Read(block, 0, block.Length);
                    return Assembly.Load(block);
                }
            }
            return null;
        }
    }
}
