// ============================================================================
// File: Models/NewsCategory.cs
// Mô tả: Model chuẩn của hệ thống đại diện cho danh mục tin tức.
//         Đây là kiểu dữ liệu chuẩn mà tất cả các nguồn tin đều phải 
//         chuyển đổi sang thông qua Adapter.
// ============================================================================

namespace AdapterPatternDemo.Models
{
    /// <summary>
    /// Model chuẩn của hệ thống - Danh mục tin tức.
    /// Mọi nguồn tin bên ngoài đều phải ánh xạ dữ liệu về kiểu này.
    /// </summary>
    public class NewsCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        public NewsCategory() { }

        public NewsCategory(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }

        public override string ToString()
        {
            return $"  [CategoryId={CategoryId}, CategoryName=\"{CategoryName}\"]";
        }
    }
}
