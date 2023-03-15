using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteFields
{
    public static class MyMath
    {
        public static int MathDivRemains(int left, int right)
        {
            if (left >= 0)
            {
                return left % right;
            }
            else
            {
                return ((Math.Abs(left) / right) + 1) * right + left;
            }
        }
        public static int PowInPField(int elem, int deg, int dim)
        {
            elem = MathDivRemains(elem, dim);
            if(deg % 2 == 0)
            {
                var temp = PowInPField(elem, deg/2, dim);
                return MathDivRemains(temp * temp, dim);
            }
            else
            {
                return MathDivRemains(elem * PowInPField(elem, deg - 1, dim), dim);
            }
        }
        public static int ReverseElemInPFielp(int elem, int dimension)
        {
            return PowInPField(elem, dimension - 2, dimension);
        }
        
    }
    public class PolinomOverSimpleField
    {
        public int[] listOfCoeff;
        public int dim { get; }
        public int deg { get; }
        public PolinomOverSimpleField(int[] listOfCoeff, int dimension) 
        {
            deg = listOfCoeff.Length - 1;
            dim = dimension;
            listOfCoeff.CopyTo(this.listOfCoeff, 0);
            ToSimpleField();
            
        }
        private void ToSimpleField()
        {
            for (int i = 0; i <= deg; i++)
            {
                listOfCoeff[i] = MyMath.MathDivRemains(listOfCoeff[i], dim);
            }
        }
        private PolinomOverSimpleField CutExtraZeros() 
        {
            int realDeg = this.deg;
            while (this[realDeg] == 0 & realDeg > 0)
            {
                realDeg--;
            }
            return new PolinomOverSimpleField(this.listOfCoeff.Take(realDeg + 1).ToArray(), this.dim);
        }
        public  int this[int key]
        {
            get
            {
                try
                {
                    if(key >= 0)
                        return listOfCoeff[key];
                    else
                        return 0;
                }
                catch(System.IndexOutOfRangeException ex)
                {
                    return 0;
                }
            }
                set{ listOfCoeff[key] = MyMath.MathDivRemains(value, dim); }
        }
        public static PolinomOverSimpleField operator -(PolinomOverSimpleField pol)
        {
            int[] listOfResCoeff = new int[pol.deg + 1];
            for (int i = 0; i <= pol.deg; i++)
            {
                listOfResCoeff[i] = pol.dim - pol[i];
            }
            return new PolinomOverSimpleField(listOfResCoeff, pol.dim);

        }
        public static PolinomOverSimpleField operator +(PolinomOverSimpleField pol) => pol;
        public static PolinomOverSimpleField operator +(PolinomOverSimpleField pol1, PolinomOverSimpleField pol2)
        {
            if (pol1.dim == pol2.dim)
            {
                int deg = Math.Max(pol1.deg, pol2.deg);
                int[] listOfResCoeff = new int[deg + 1];
                for (int i = 0; i <= deg; i++)
                {
                    listOfResCoeff[i] = pol1[i] + pol2[i];
                }
                return (new PolinomOverSimpleField(listOfResCoeff, pol1.dim)).CutExtraZeros();
            }
            else
            {
                throw new Exception("Размерности полей, над которыми построили многочлены разные");
            }
        }
        public static PolinomOverSimpleField operator -(PolinomOverSimpleField pol1, PolinomOverSimpleField pol2) 
        { 
            return pol1 + (-pol2);
        }
        public static PolinomOverSimpleField operator *(PolinomOverSimpleField pol1, PolinomOverSimpleField pol2)
        {
            if (pol1.dim == pol2.dim)
            {
                int deg = pol1.deg + pol2.deg;
                int[] listOfResCoeff = new int[deg + 1];
                for (int i = 0; i <= deg; i++)
                {
                   for(int j = 0; j <= Math.Min(i, pol1.deg); j++)
                    {
                        listOfResCoeff[i] += pol1[j] * pol2[i - j];
                    }
                }
                return new PolinomOverSimpleField(listOfResCoeff, pol1.dim);
            }
            else
            {
                throw new Exception("Размерности полей, над которыми построили многочлены разные");
            }
        }
        public static PolinomOverSimpleField operator %(PolinomOverSimpleField pol1, PolinomOverSimpleField pol2)
        {
            if (pol1.dim == pol2.dim)
            {
                int deg = Math.Min(pol1.deg, pol2.deg - 1);
                int[] listOfResCoeff = new int[deg + 1];
                PolinomOverSimpleField tempPol = pol1;
                while(tempPol.deg > deg)
                {
                    int[] majorSummand = new int[tempPol.deg - pol2.deg + 1];
                    int reverseEl = MyMath.ReverseElemInPFielp(pol2[pol2.deg], pol2.dim);
                    majorSummand[tempPol.deg - pol2.deg] = MyMath.MathDivRemains(tempPol[tempPol.deg] * reverseEl, pol2.dim);
                    PolinomOverSimpleField stepPol = new(majorSummand, pol2.dim);
                    tempPol = tempPol - (pol2 * stepPol);
                }

                return tempPol;
            }
            else
            {
                throw new Exception("Размерности полей, над которыми построили многочлены разные");
            }
        }
        public override string ToString()
        {
            return string.Join(", ", listOfCoeff);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is PolinomOverSimpleField)) return false;
            PolinomOverSimpleField p2 = (obj as PolinomOverSimpleField)!;
            if (p2.dim != this.dim || p2.deg != this.deg) return false;
            for(int i=0; i <= this.deg; i++)
            {
                if (p2[i] != this[i])
                {
                    return false;
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
    }
    
}
