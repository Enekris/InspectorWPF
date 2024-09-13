using System.ComponentModel.DataAnnotations.Schema;

namespace Inspector.Models
{
    public class HardwaresWpf
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public int NameId { get; set; }
        public string? Model { get; set; }
        public int? CabinetId { get; set; }
        public int? OvtId { get; set; }
        public bool UsageInfo { get; set; }
        public int? DocLocationFirstId { get; set; }
        public int? DocLocationSecondId { get; set; }
        public int? DocLocationThirdId { get; set; }
        public string? Appointment { get; set; }
        public string? Note { get; set; }

        public OVTsWpf OVTWpf { get; set; }
        public HardwareFilterNameWpf FilterWpf { get; set; }
        public DocumentsFirstWpf DocumentFirstWpf { get; set; }
        public DocumentsSecondWpf DocumentSecondWpf { get; set; }
        public DocumentsThirdWpf documentThirdWpf { get; set; }
        public CabinetsWpf CabinetWpf { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; }
        [NotMapped]
        public string? Treetittle { get; set; }

        public HardwaresWpf()
        {

        }


    }
}
