using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        DriveInfo[] drives;
        static readonly string filePath = @"D:\DeletedFiles.txt";
        public Service1()
        {
            InitializeComponent();

            drives = DriveInfo.GetDrives().Where<DriveInfo>(drive => drive.DriveType == DriveType.Fixed).ToArray<DriveInfo>();          
        }

        protected override void OnStart(string[] args)
        {
            foreach (var drive in drives)
            {
                FileSystemWatcher watcher = new FileSystemWatcher(drive.RootDirectory.ToString());
                watcher.IncludeSubdirectories = true;
                watcher.Deleted += watcher_Deleted;
                watcher.Error += watcher_Error;
                watcher.EnableRaisingEvents = true;
            }
        }

        private void watcher_Error(object sender, ErrorEventArgs e)
        {
            if(this.CanPauseAndContinue == true)
            {
                this.OnStart(new string[0]);
            }
        }

        async void watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            using (var stream = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.Read))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    await streamWriter.WriteLineAsync(e.FullPath);
                }
            }
        }
    }
}
