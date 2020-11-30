namespace EPS.Administration.DAL.Data
{
    public class FileDefinitionData : IRevisionableEntity
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public int BaseId { get; set; }
        public string FileName { get; set; }
        public string StoredFileName { get; set; }
    }
}
