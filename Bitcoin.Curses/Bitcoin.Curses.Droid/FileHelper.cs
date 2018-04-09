using System.IO;
using Bitcoin.Curses.Droid;
using Bitcoin.Curses.Services.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Bitcoin.Curses.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //File.Delete(Path.Combine(path, filename));

            return Path.Combine(path, filename);
        }
    }
}