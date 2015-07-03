namespace  Manage.ViewModel
{
    public class RecommendedDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int TypeID { get; set; }
        public string Url { get; set; }
        public int DataID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }
        public System.DateTime Publish { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Content { get; set; }
        public bool IsDel { get; set; }
    }
}
