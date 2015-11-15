using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace Saturn72.Extensions
{
    public static class AspNetExt
    {
        public static FileInfo GetAspNetDeploymentPath(FileInfo fileInfo, string destinationDirectory)
        {
            FileInfo result;
            if (NetCommonHelper.GetTrustLevel() != AspNetHostingPermissionLevel.Unrestricted)
            {
                //all plugins will need to be copied to ~/Plugins/bin/
                //this is aboslutely required because all of this relies on probingPaths being set statically in the web.config

                //were running in med trust, so copy to custom bin folder
                var shadowCopyFolder = Directory.CreateDirectory(destinationDirectory);
                result = InitializeMediumTrust(fileInfo, shadowCopyFolder);
            }
            else
            {
                var directory = AppDomain.CurrentDomain.DynamicDirectory;
                Debug.WriteLine(fileInfo.FullName + " to " + directory);
                //were running in full trust so copy to standard dynamic folder
                result = InitializeFullTrust(fileInfo, new DirectoryInfo(directory));
            }

            return result;
        }

        /// <summary>
        ///     Used to initialize plugins when running in Full Trust
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="destinationDirectory"></param>
        /// <returns></returns>
        private static FileInfo InitializeFullTrust(FileInfo fileInfo, DirectoryInfo destinationDirectory)
        {
            var shadowfile = new FileInfo(Path.Combine(destinationDirectory.FullName, fileInfo.Name));
            try
            {
                File.Copy(fileInfo.FullName, shadowfile.FullName, true);
            }
            catch (IOException)
            {
                Debug.WriteLine(shadowfile.FullName + " is locked, attempting to rename");
                //this occurs when the files are locked,
                //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                try
                {
                    var oldFile = shadowfile.FullName + Guid.NewGuid().ToString("N") + ".old";
                    File.Move(shadowfile.FullName, oldFile);
                }
                catch (IOException exc)
                {
                    throw new IOException(shadowfile.FullName + " rename failed, cannot initialize file", exc);
                }
                //ok, we've made it this far, now retry the shadow copy
                File.Copy(fileInfo.FullName, shadowfile.FullName, true);
            }
            return shadowfile;
        }

        /// <summary>
        ///     Used to initialize plugins when running in Medium Trust
        /// </summary>
        /// <param name="plug"></param>
        /// <param name="destinationDirectory"></param>
        /// <returns></returns>
        public static FileInfo InitializeMediumTrust(FileInfo plug, DirectoryInfo destinationDirectory)
        {
            var shouldCopy = true;
            var shadowCopiedPlug = new FileInfo(Path.Combine(destinationDirectory.FullName, plug.Name));

            //check if a shadow copied file already exists and if it does, check if it's updated, if not don't copy
            if (shadowCopiedPlug.Exists)
            {
                //it's better to use LastWriteTimeUTC, but not all file systems have this property
                //maybe it is better to compare file hash?
                var areFilesIdentical = shadowCopiedPlug.CreationTimeUtc.Ticks >= plug.CreationTimeUtc.Ticks;
                if (areFilesIdentical)
                {
                    Debug.WriteLine("Not copying; files appear identical: '{0}'", shadowCopiedPlug.Name);
                    shouldCopy = false;
                }
                else
                {
                    //delete an existing file

                    //More info: http://www.nopcommerce.com/boards/t/11511/access-error-nopplugindiscountrulesbillingcountrydll.aspx?p=4#60838
                    Debug.WriteLine("New plugin found; Deleting the old file: '{0}'", shadowCopiedPlug.Name);
                    File.Delete(shadowCopiedPlug.FullName);
                }
            }

            if (shouldCopy)
            {
                try
                {
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
                }
                catch (IOException)
                {
                    Debug.WriteLine(shadowCopiedPlug.FullName + " is locked, attempting to rename");
                    //this occurs when the files are locked,
                    //for some reason devenv locks plugin files some times and for another crazy reason you are allowed to rename them
                    //which releases the lock, so that it what we are doing here, once it's renamed, we can re-shadow copy
                    try
                    {
                        var oldFile = shadowCopiedPlug.FullName + Guid.NewGuid().ToString("N") + ".old";
                        File.Move(shadowCopiedPlug.FullName, oldFile);
                    }
                    catch (IOException exc)
                    {
                        throw new IOException(shadowCopiedPlug.FullName + " rename failed, cannot initialize plugin",
                            exc);
                    }
                    //ok, we've made it this far, now retry the shadow copy
                    File.Copy(plug.FullName, shadowCopiedPlug.FullName, true);
                }
            }

            return shadowCopiedPlug;
        }
    }
}
