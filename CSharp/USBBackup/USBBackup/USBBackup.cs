using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace USBBackup
{
    public class USBBackup
    {
        List<DriveInfo> lDrivers = DriveInfo.GetDrives().ToList();

        public USBBackup()
        { 
        
        }

        public List<string> ListDrives()
        {
            List<string> tStr = new List<string>();
            foreach (DriveInfo Dri in lDrivers)
            {
                tStr.Add(Dri.Name);
            }

            return tStr;
        }

        public List<string> ListDirectory(DriveInfo tDri)
        {
            DirectoryInfo Dir = new DirectoryInfo(tDri.Name);
            List<string> tStr = new List<string>();

            if (!Dir.Exists)
            {
                tStr.Add("");
                return tStr;
            }

            foreach (DirectoryInfo Fil in Dir.GetDirectories())
            {
                tStr.Add(Fil.Name);
            }
            return tStr;
        }

        public List<string> ListFiles(DriveInfo tDri)
        {
            DirectoryInfo Dir = new DirectoryInfo(tDri.Name);
            return ListFiles(Dir, "*.*");
        }

        public List<string> ListFiles(DirectoryInfo tDri)
        {
            return ListFiles(tDri, "*.*");
        }

        public List<string> ListFiles(DirectoryInfo tDri, string tTipo)
        {
            List<string> tStr = new List<string>();

            if (!tDri.Exists)
            {
                tStr.Add("");
                return tStr;
            }

            foreach (FileInfo Fil in tDri.GetFiles(tTipo))
            {
                tStr.Add(Fil.Name);
            }
            return tStr;
        }

        public void Copy(FileInfo tFile)
        { 
        
        
        }


        public void Paste(DirectoryInfo tDir)
        { 
        
        
        }
    }
}
