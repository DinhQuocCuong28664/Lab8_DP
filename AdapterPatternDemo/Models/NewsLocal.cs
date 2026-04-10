// ============================================================================
// File: Models/NewsLocal.cs
// Mô tả: Model chuẩn của hệ thống đại diện cho một bài tin tức.
//         Đây là kiểu dữ liệu chuẩn mà tất cả các nguồn tin đều phải
//         chuyển đổi sang thông qua Adapter.
// ============================================================================

namespace AdapterPatternDemo.Models
{
    /// <summary>
    /// Model chuẩn của hệ thống - Bài tin tức.
    /// Mọi nguồn tin bên ngoài đều phải ánh xạ dữ liệu về kiểu này.
    /// </summary>
    public class NewsLocal
    {
        public int NewsId { get; set; }
        public string NewsTitle { get; set; } = string.Empty;
        public string NewsContent { get; set; } = string.Empty;
        public int CategoryId { get; set; }

        public NewsLocal() { }

        public NewsLocal(int newsId, string newsTitle, string newsContent, int categoryId)
        {
            NewsId = newsId;
            NewsTitle = newsTitle;
            NewsContent = newsContent;
            CategoryId = categoryId;
        }

        public override string ToString()
        {
            return $"  [NewsId={NewsId}, Title=\"{NewsTitle}\", Content=\"{NewsContent}\", CategoryId={CategoryId}]";
        }
    }
}
