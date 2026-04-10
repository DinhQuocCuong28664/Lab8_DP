// ============================================================================
// File: Target/NewsDAO.cs
// Mô tả: CONCRETE IMPLEMENTATION - Lớp truy vấn CSDL local (SQLite).
//         Đây là implementation trực tiếp của INewsDAO, truy vấn dữ liệu 
//         từ cơ sở dữ liệu nội bộ SQLite. Không cần Adapter vì dữ liệu 
//         đã theo chuẩn NewsCategory và NewsLocal.
// ============================================================================

using Microsoft.Data.Sqlite;
using AdapterPatternDemo.Models;

namespace AdapterPatternDemo.Target
{
    /// <summary>
    /// Lớp truy vấn CSDL local sử dụng SQLite (In-Memory).
    /// Implement trực tiếp INewsDAO vì dữ liệu local đã theo chuẩn hệ thống.
    /// Đóng vai trò là nguồn dữ liệu nội bộ (không cần Adapter).
    /// </summary>
    public class NewsDAO : INewsDAO
    {
        private readonly SqliteConnection _connection;

        /// <summary>
        /// Constructor: Khởi tạo CSDL SQLite In-Memory và seed dữ liệu mẫu.
        /// </summary>
        public NewsDAO()
        {
            // Sử dụng SQLite In-Memory để minh họa kết nối CSDL nội bộ
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
            InitializeDatabase();
        }

        /// <summary>
        /// Tạo bảng và chèn dữ liệu mẫu vào CSDL SQLite.
        /// </summary>
        private void InitializeDatabase()
        {
            using var command = _connection.CreateCommand();

            // Tạo bảng Category
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Category (
                    CategoryId INTEGER PRIMARY KEY,
                    CategoryName TEXT NOT NULL
                );";
            command.ExecuteNonQuery();

            // Tạo bảng News
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS News (
                    NewsId INTEGER PRIMARY KEY,
                    NewsTitle TEXT NOT NULL,
                    NewsContent TEXT NOT NULL,
                    CategoryId INTEGER,
                    FOREIGN KEY (CategoryId) REFERENCES Category(CategoryId)
                );";
            command.ExecuteNonQuery();

            // Seed dữ liệu mẫu - Danh mục
            command.CommandText = @"
                INSERT INTO Category (CategoryId, CategoryName) VALUES (1, 'Thời sự');
                INSERT INTO Category (CategoryId, CategoryName) VALUES (2, 'Công nghệ');
                INSERT INTO Category (CategoryId, CategoryName) VALUES (3, 'Thể thao');";
            command.ExecuteNonQuery();

            // Seed dữ liệu mẫu - Tin tức
            command.CommandText = @"
                INSERT INTO News (NewsId, NewsTitle, NewsContent, CategoryId) VALUES 
                    (1, '[Local] Quốc hội họp kỳ thứ 7', 'Nội dung kỳ họp Quốc hội...', 1);
                INSERT INTO News (NewsId, NewsTitle, NewsContent, CategoryId) VALUES 
                    (2, '[Local] AI phát triển mạnh mẽ', 'Trí tuệ nhân tạo đang thay đổi...', 2);
                INSERT INTO News (NewsId, NewsTitle, NewsContent, CategoryId) VALUES 
                    (3, '[Local] Việt Nam vô địch AFF Cup', 'Đội tuyển Việt Nam giành chiến thắng...', 3);
                INSERT INTO News (NewsId, NewsTitle, NewsContent, CategoryId) VALUES 
                    (4, '[Local] Chuyển đổi số quốc gia', 'Chương trình chuyển đổi số...', 2);";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Truy vấn toàn bộ danh mục từ CSDL SQLite.
        /// </summary>
        public List<NewsCategory> getAllCategory()
        {
            var categories = new List<NewsCategory>();

            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT CategoryId, CategoryName FROM Category";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new NewsCategory(
                    reader.GetInt32(0),
                    reader.GetString(1)
                ));
            }

            return categories;
        }

        /// <summary>
        /// Truy vấn tin tức theo danh mục từ CSDL SQLite.
        /// </summary>
        public List<NewsLocal> getNewsByCategory(int categoryId)
        {
            var newsList = new List<NewsLocal>();

            using var command = _connection.CreateCommand();
            command.CommandText = "SELECT NewsId, NewsTitle, NewsContent, CategoryId FROM News WHERE CategoryId = @catId";
            command.Parameters.AddWithValue("@catId", categoryId);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                newsList.Add(new NewsLocal(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3)
                ));
            }

            return newsList;
        }
    }
}
