using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class DocumentsWpf
    {
        //что уникально помечаю
        public int Id { get; set; }
        public string InvNumber { get; set; }
        public DateTime? Data { get; set; }
        public string? Name { get; set; }
        public int? VolumeId { get; set; }
        public int? InventoryId { get; set; }
        public int? Page { get; set; }
        public bool ForDestruction { get; set; }
        public bool DestructionMark { get; set; }
        public string? Note { get; set; }
        public VolumesWpf VolumeWpf { get; set; }
        public InvertoriesWpf InventoryWpf { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }

        [NotMapped]
        public string InvNumberWithName
        {
            get
            {
                if (InvNumber == "Пусто")
                {
                    return string.Format(InvNumber);
                }
                else if (Name == "" || Name == null)
                {
                    return string.Format("{0}", InvNumber);
                }
                else
                {
                    return string.Format("{0}\n{1}", InvNumber, Name);
                }

            }


        }
        [NotMapped]
        public string InvNumberWithNameForTree { get; set; }

        public DocumentsWpf()
        {

        }
    }
}

