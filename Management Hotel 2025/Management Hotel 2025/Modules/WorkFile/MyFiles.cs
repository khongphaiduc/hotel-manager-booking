
namespace Management_Hotel_2025.Modules.WorkFile
{
    public class MyFiles : IMyFiles
    {
        //tạo folder 
        public string CreateFolder(string folderName)
        {

            if (string.IsNullOrWhiteSpace(folderName))
                throw new ArgumentException("Tên thư mục không hợp lệ", nameof(folderName));

            // Tạo đường dẫn đầy đủ trong wwwroot
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

            // Tạo thư mục nếu chưa tồn tại (Directory.CreateDirectory tự động bỏ qua nếu đã tồn tại)
            DirectoryInfo result = Directory.CreateDirectory(folderPath);

            Console.WriteLine($"Thư mục đã được tạo hoặc tồn tại vclllll: {result.FullName}");
            return result.FullName;
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("Đường dẫn file không hợp lệ", nameof(filePath));
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Console.WriteLine($"Đã xóa file: {filePath}");
            }
            else
            {
                Console.WriteLine($"File không tồn tại: {filePath}");
            }
        }


        public void DeleteFolder(string folderPath)
        {
            bool recursive = true;  // xóa toàn bộ  nội dùng trong thư mục
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentException("Đường dẫn không hợp lệ", nameof(folderPath));
            }

            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, recursive);
                Console.WriteLine($"Đã xóa thư mục: {folderPath}");
            }
            else
            {
                Console.WriteLine($"Thư mục không tồn tại: {folderPath}");
            }
        }

        // lưu file 
        public async Task<string> SaveFiles(IFormFile file, string pathfolder)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Không có file nào để lưu.", nameof(file));

            if (string.IsNullOrWhiteSpace(pathfolder))
                throw new ArgumentException("Đường dẫn thư mục không hợp lệ", nameof(pathfolder));

            if (!Directory.Exists(pathfolder))
            {
                // tạo folder  nếu chưa có 
                Directory.CreateDirectory(pathfolder);
            }

            string fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}"; // thêm guid ở tên file để tránh trung tên file 

            string filePath = Path.Combine(pathfolder, fileName);  //  nối tên file và đường dẫn của folder 

            //  lưu file vào đường dẫn đã tạo   
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }



        // kiểm tra xem file đã  tồn tại trong thư mục chưa 
        public bool IsFileExist(string filePath, IFormFile file)
        {
            if (file == null || string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Đường dẫn hoặc file không hợp lệ");
            string fullPath = Path.Combine(filePath, file.FileName);    // tạo đường dẫn đày đủ 
            return File.Exists(fullPath);                              // check file có tồn tại hay không
        }

        public string ChangeFileName(string FileName)
        {
            string extension = Path.GetExtension(FileName);             //  Path.GetExtension trả về phần mở rộng của file , ví dụ : "image.jpg" → ".jpg"
            string newFileName = $"{Guid.NewGuid()}{extension}";       // Guid.NewGuid() tạo ra một chuỗi duy nhất kiểu UUID
            return newFileName;
        }


        public string GetFilePath(string folderPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Đường dẫn hoặc tên file không hợp lệ");
            return Path.Combine(folderPath, fileName);
        }




    }
}

