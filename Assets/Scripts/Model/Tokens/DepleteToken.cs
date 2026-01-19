using Ship;

namespace Tokens
{
    public class DepleteToken : GenericToken
    {
        public bool WasApplied = false;

        public DepleteToken(GenericShip host) : base(host)
        {
            Name = ImageName = "Deplete Token";
            Temporary = false;
            PriorityUI = 28;
            TokenColor = TokenColors.Red;
            TokenShape = TokenShapes.Square;
        }
    }
}
