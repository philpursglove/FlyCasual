namespace Editions
{
    public class XWAEdition : SecondEdition
    {
        public override string Name { get { return "XWA Edition"; } }
        public override string NameShort { get { return "XWAEdition"; } }
        public override int MaxPoints { get { return 50; } }

        public XWAEdition() : base() { }
    }
}