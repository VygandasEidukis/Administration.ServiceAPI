namespace EPS.Administration.Models.Device
{
    public class FileDefinition : IRevisionable
    {
        public int Id { get; set; }
        public int Revision { get; set; }
        public string FileName { get; set; }
        public string StoredFileName { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return FileName;
        }
    }
}
