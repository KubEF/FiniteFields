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
        public static int ReverseElemInPFielp(int elem, int dimension)
        {
            return MathDivRemains((int)Math.Pow(elem, dimension - 2), dimension);
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
            this.listOfCoeff = listOfCoeff;
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
            PolinomOverSimpleField tempPol = this;
            int realDeg = this.deg;
            while (tempPol[realDeg] == 0 & realDeg > 0)
            {
                realDeg--;
            }
            int[] listOfCoeff = new int[realDeg + 1];
            for (int i = 0; i <= realDeg; i++)
            {
                listOfCoeff[i] = tempPol[i];
            }
            return new PolinomOverSimpleField(listOfCoeff, this.dim);
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
                listOfResCoeff[i] = MyMath.MathDivRemains(-pol[i], pol.dim);
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
            string res = "";
            foreach(var coeff in this.listOfCoeff)
            {
                res += (coeff.ToString() + ", ");
            }
            res = res.Substring(0, res.Length - 2);
            return res;
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
