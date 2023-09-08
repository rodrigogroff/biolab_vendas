using Microsoft.AspNetCore.Http;
using System.IO;

namespace Master.Service.Helper
{
    public class HelperFileManager
    {
        public string USER_IMAGE = "UserImage";

        public string currentFileOrFolder { get; set; }

        public void AddFileOrFolder(string dir)
        {
#if RELEASE
            currentFileOrFolder += "/" + dir;
#else
            currentFileOrFolder += "\\" + dir;
#endif
        }

        public void CreateDirIfNotExists()
        {
            if (!Directory.Exists(currentFileOrFolder))
            {
                Directory.CreateDirectory(currentFileOrFolder);
            }
        }


        public string BuildFilePath(string filesDir, string mType, long fkCompany, string id)
        {
            currentFileOrFolder = filesDir;

            AddFileOrFolder(fkCompany.ToString());
            CreateDirIfNotExists();
            AddFileOrFolder(mType);
            CreateDirIfNotExists();
            AddFileOrFolder(id);
            CreateDirIfNotExists();

            return currentFileOrFolder;
        }

        public bool SaveUserImageFile(string filesDir, long fkCompany, string idUser, IFormFile postedFile)
        {
            if (string.IsNullOrEmpty(filesDir) || string.IsNullOrEmpty(idUser) || postedFile == null)
            {
                return false;
            }

            BuildFilePath(filesDir, USER_IMAGE, fkCompany, idUser);
            AddFileOrFolder(postedFile.FileName);

            using (Stream fileStream = new FileStream(currentFileOrFolder, FileMode.Create))
            {
                postedFile.CopyTo(fileStream);
            }

            return true;
        }
    }
}
