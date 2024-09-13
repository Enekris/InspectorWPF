﻿namespace Inspector.Domains.Entities
{
    public class InvertoriesDb : BaseEntity
    {
        public string Number { get; set; }
        public string? Name { get; set; }
        public bool ForDestruction { get; set; } = false;
        public bool DestructionMark { get; set; } = false;
        public string? Note { get; set; }
        public ICollection<DocumentsRaspOVVDb> DocumentRaspOVVDb { get; set; } = [];
        public ICollection<DocumentsActReportDb> DocumentActReportDb { get; set; } = [];
        public ICollection<DocumentsThirdDb> DocumentThirdDb { get; set; } = [];
        public ICollection<DocumentsFirstDb> DocumentFirstDb { get; set; } = [];
        public ICollection<DocumentsSecondDb> DocumentSecondDb { get; set; } = [];
        public ICollection<SertificatesDb> SertificatesDb { get; set; } = [];
        public ICollection<DocumentsOthersDb> DocumentsOthersDb { get; set; } = [];
        public InvertoriesDb()
        {

        }

    }
}
