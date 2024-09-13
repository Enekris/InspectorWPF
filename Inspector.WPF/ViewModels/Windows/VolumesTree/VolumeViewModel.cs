using Inspector.Models;

namespace Inspector.ViewModels.Windows.VolumesTree
{
    public class VolumeViewModel
    {
        public string VolumeNumber { get; }
        public bool DestructionMark { get; set; } = false;
        public bool ForDestruction { get; set; } = false;
        public string FullVolumeName { get; }
        public List<DocumentsWpf> DocumentsCollection { get; }
        public List<SertificatesWpf> SertificatesCollection { get; }
        public List<LastElement> LastElements { get; set; }
        public VolumeViewModel(int volumeNumber, List<DocumentsWpf> documents, List<SertificatesWpf> sertificates, bool destructionMarkOnVolume, bool forDestructionOnVolume)
        {
            VolumeNumber = "Том " + volumeNumber;
            DocumentsCollection = documents;
            SertificatesCollection = sertificates;
            DestructionMark = destructionMarkOnVolume;
            ForDestruction = forDestructionOnVolume;
            AddLastElements();
        }

        private void AddLastElements()
        {
            LastElements = [];
            foreach (var item in DocumentsCollection)
            {
                LastElements.Add(new LastElement(item.InvNumberWithNameForTree, item.DestructionMark, item.ForDestruction));
            }

            foreach (var item in SertificatesCollection)
            {
                var element = new LastElement(item.InvNumberWithName, item.DestructionMark, item.ForDestruction);
                var page = "\t";
                var number = $"\t{item.Number}";
                var name = item.Name == null ? "" : $"\t{item.Name}";
                if (item.Page != null)
                {
                    if (item.Page / 10 < 1)
                    {
                        page = $"000{item.Page}\t";
                    }
                    else if (item.Page / 100 < 1)
                    {
                        page = $"00{item.Page}\t";
                    }
                    else if (item.Page / 1000 < 1)
                    {
                        page = $"0{item.Page}\t";
                    }
                    else if (item.Page / 10000 < 1)
                    {
                        page = $"{item.Page}\t";
                    }
                }

                element.InvNumberWithNameForTree = $"{page}Атт.{number}{name}";
                LastElements.Add(element);
            }

            LastElements = LastElements.OrderBy(e => e.InvNumberWithNameForTree).ToList();
            foreach (var item in LastElements)
            {
                item.InvNumberWithNameForTree = item.InvNumberWithNameForTree.TrimStart('0');
            }
        }


    }


}
