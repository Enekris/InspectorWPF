namespace Inspector.ViewModels.Windows.VolumesTree
{
    public class LastElement
    {
        public LastElement(string invname, bool destructionMark, bool forDestruction)
        {
            InvNumberWithNameForTree = invname;
            DestructionMark = destructionMark;
            ForDestruction = forDestruction;
        }
        public bool ForDestruction { get; set; } = false;
        public bool DestructionMark { get; set; } = false;
        public string InvNumberWithNameForTree { get; set; }
    }
}
