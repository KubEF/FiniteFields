using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteFields
{
    internal class BinaryFiniteFieldElement : FiniteFieldElement
    {
        public BinaryFiniteFieldElement(FiniteField F, PolinomOverSimpleField representedPol) : base(F, representedPol)
        {
            if(F.p != 2)
            {
                throw new ArgumentException("p != 2, поле не вида F_{2^n}");
            }
        }
        public BinaryFiniteFieldElement(FiniteField F, int[] representedPol) : base(F, representedPol)
        {
            if(F.p != 2)
            {
                throw new ArgumentException("p != 2, поле не вида F_{2^n}");
            }
        }



    }
}
