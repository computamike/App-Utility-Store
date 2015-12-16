using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Management;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web.Hosting;

namespace Open.GI.hypermart.Helpers
{

    /// <summary>
    /// File Share helper (create and manage file shares)
    /// </summary>
      public enum ManagementType : uint
    {
        /// <summary>
        /// The disk drive
        /// </summary>
        DiskDrive = 0x0,
        /// <summary>
        /// The print queue
        /// </summary>
        PrintQueue = 0x1,
        /// <summary>
        /// The device
        /// </summary>
        DEVICE = 0x2,
        /// <summary>
        /// The ipc
        /// </summary>
        IPC = 0x3,
        /// <summary>
        /// The dis k_ driv e_ admin
        /// </summary>
        DISK_DRIVE_ADMIN = 0x80000000,
        /// <summary>
        /// The prin t_ queu e_ admin
        /// </summary>
        PRINT_QUEUE_ADMIN = 0x80000001,
        /// <summary>
        /// The devic e_ admin
        /// </summary>
        DEVICE_ADMIN = 0x80000002,
        /// <summary>
        /// The ip c_ admin
        /// </summary>
        IPC_ADMIN = 0x8000003
    }


      /// <summary>
      /// 
      /// </summary>
    public class FileShareHelper
    {
        /// <summary>
        /// Create a Folder - used to create a file System for storing products\files\versions
        /// </summary>
        /// <param name="FolderName"></param>
        public static void CreateFolder(String FolderName)
        {
            var RootPath = HostingEnvironment.ApplicationPhysicalPath;
            CreateFolder(FolderName, RootPath);

        }

        /// <summary>
        /// Creates the share.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>
        /// <param name="ShareName">Name of the share.</param>
        /// <param name="Description">The description.</param>
        /// <exception cref="System.Exception">Unable to share directory.</exception>
        public static void CreateShare(string FolderName, string ShareName, string Description )
        {
            
            var RootPath = HostingEnvironment.ApplicationPhysicalPath;
            // Does folder exist?
            var StoreLocation = CreateFolder(FolderName, RootPath);

            ManagementClass managementClass = new ManagementClass("Win32_Share");
            ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
            ManagementBaseObject outParams;
            // Set the input parameters
            inParams["Description"] = Description;
            inParams["Name"] = ShareName;
            inParams["Path"] = StoreLocation;
            inParams["Type"] = ManagementType.DiskDrive; // Disk Drive
            //Another Type:
            //        DISK_DRIVE = 0x0
            //        PRINT_QUEUE = 0x1
            //        DEVICE = 0x2
            //        IPC = 0x3
            //        DISK_DRIVE_ADMIN = 0x80000000
            //        PRINT_QUEUE_ADMIN = 0x80000001
            //        DEVICE_ADMIN = 0x80000002
            //        IPC_ADMIN = 0x8000003
            //inParams["MaximumAllowed"] = int maxConnectionsNum;
            // Invoke the method on the ManagementClass object
            outParams = managementClass.InvokeMethod("Create", inParams, null);
            // Check to see if the method invocation was successful
            if ((uint) (outParams.Properties["ReturnValue"].Value) != 0)
            {
                throw new Exception("Unable to share directory.");
            }




            
        }

        private static string CreateFolder(string FolderName, string RootPath)
        {
            var StoreLocation = System.IO.Path.Combine(RootPath, FolderName);
            if (!System.IO.Directory.Exists(StoreLocation))
            {
                var ServiceProcess = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                DirectorySecurity StoreSecurity = new DirectorySecurity();

                StoreSecurity.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Write, AccessControlType.Deny));
                StoreSecurity.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Read, AccessControlType.Allow));
                StoreSecurity.AddAccessRule(new FileSystemAccessRule(ServiceProcess, FileSystemRights.FullControl, AccessControlType.Allow));

                System.IO.Directory.CreateDirectory(StoreLocation, StoreSecurity);
            }
            // Create FileShare
            return StoreLocation;
        }

        /// <summary>
        /// Deletes the share.
        /// </summary>
        /// <param name="FolderName">Name of the folder.</param>
        public static void DeleteShare(string FolderName )
        {
            var RootPath = HostingEnvironment.ApplicationPhysicalPath;
            var StoreLocation = System.IO.Path.Combine(RootPath, FolderName);
            using (System.Management.ManagementClass shareObj = new System.Management.ManagementClass("Win32_Share"))
            {
                System.Management.ManagementObjectCollection shares =
                shareObj.GetInstances();
                foreach (System.Management.ManagementObject share in shares)
                {
                    Console.WriteLine("Name: " + share["Name"].ToString());
                }
            }

            ManagementScope ms = new ManagementScope(@"\\localhost\root\cimv2");

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * from Win32_Share where Name ='" + FolderName+"'");

            ManagementObjectCollection result = searcher.Get();

            foreach (ManagementObject item in result)
            {
                item.InvokeMethod("Delete",null);
                
            }

        }
    }


 
}