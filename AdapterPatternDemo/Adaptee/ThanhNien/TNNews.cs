// ============================================================================
// File: Adaptee/ThanhNien/TNNews.cs
// Mô tả: Model tin tức của thư viện Thanh Niên (bên thứ 3).
//         Kiểu dữ liệu KHÔNG tương thích với chuẩn NewsLocal của hệ thống.
//         Thuộc tính: id, title, content (khác với newsId, newsTitle, 
//         newsContent, categoryId).
// ============================================================================

namespace AdapterPatternDemo.Adaptee.ThanhNien
{
    /// <summary>
    /// Model tin tức của Thanh Niên - ADAPTEE MODEL.
    /// Có cấu trúc khác với NewsLocal chuẩn:
    /// - id (thay vì newsId)
    /// - title (thay vì newsTitle)
    /// - content (thay vì newsContent)
    /// - KHÔNG có categoryId riêng (được trả theo phương thức lọc)
    /// </summary>
    public class TNNews
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public TNNews() { }

        public TNNews(int id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }
    }
}
