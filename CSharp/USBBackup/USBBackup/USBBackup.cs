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
        DirectoryInfo Origin;
        DirectoryInfo Destiny;
        

        public USBBackup()
        { 
        
        }

        public void SetOrigin(DirectoryInfo tOrigin)
        {
            Origin = tOrigin;
        }

        public void SetDestiny(DirectoryInfo tDestiny)
        {
            Destiny = tDestiny;
        }

        public void Backup()
        {
            if (Directory.Exists(Destiny.FullName))
            {
                DirectoryCopy(Origin.FullName, Destiny.FullName, true);
                DirectoryDelete(Destiny.FullName, Origin.FullName, true);
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);

                if (File.Exists(temppath))
                {
                    DateTime filetemp = File.GetLastWriteTime(temppath);
                    if (file.LastWriteTime > filetemp)
                    {
                        try
                        {
                            file.CopyTo(temppath, true);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    try
                    {
                        file.CopyTo(temppath, false);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private static void DirectoryDelete(string sourceDirName, string destDirName, bool copySubDirs)
        {

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(sourceDirName))
            {
                return;
            }

            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);

                if (!File.Exists(temppath))
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);

                    if (!Directory.Exists(temppath))
                    {
                        try
                        {
                            subdir.Delete(true);
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (Directory.Exists(subdir.FullName))
                        DirectoryDelete(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
