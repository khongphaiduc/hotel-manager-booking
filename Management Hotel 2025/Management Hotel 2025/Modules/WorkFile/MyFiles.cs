
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

            Console.WriteLine($"Thư mục đã được tạo hoặc tồn tại: {result.FullName}");
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
        public async Task<string> SaveFiles(IFormFile file, string folderPath)
        {
            if (file == null || file.Length == 0)
                return "Không có file nào để lưu.";

            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentException("Đường dẫn thư mục không hợp lệ", nameof(folderPath));

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folderPath))
            {
                folderPath = this.CreateFolder(folderPath);
            }

            // Lấy tên file gốc
            string fileName = file.FileName;

            // Tạo đường dẫn đầy đủ   ,Nó tự động thêm dấu \ hoặc / tùy hệ điều hành và tránh việc bạn phải tự viết tay.
            string filePath = Path.Combine(folderPath, fileName);

            // Lưu file vào server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;   // trả về file Name để lưu vào database 
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

