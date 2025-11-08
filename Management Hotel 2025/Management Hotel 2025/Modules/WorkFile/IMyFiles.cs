namespace Management_Hotel_2025.Modules.WorkFile
{
    public interface IMyFiles
    {

        // lưu các file vào thư mục và trả về đường dẫn của thư mục đó 
        public Task<string> SaveFiles(IFormFile files, string folderName);

        // xóa file trong thư mục

        public void DeleteFile(string filePath);

        // tạo thư mục
        public string CreateFolder(string folderName);


        // xóa thư mục
        public void DeleteFolder(string folderName);

        bool IsFileExist(string filePath, IFormFile file);
        string GetFilePath(string folderPath, string fileName);
    }
}
