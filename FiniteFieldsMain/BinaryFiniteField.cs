using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteFields
{
    internal class BinaryFiniteField : FiniteField
    {
        public BinaryFiniteField(int n, PolinomOverSimpleField q) : base(2, n, q) { }

        public FiniteFieldElement FromNumberToElement(int number)
        {
            if (number > (int)Math.Pow(2, n)) throw new Exception("Попытка оперировать числами большей размерности");
            int counter = 0;
            var temp = number;
            int[] polinomOfEl = new int[n];
            while(temp > 1)
            {
                polinomOfEl[counter] = temp % 2;
                temp = temp / 2;
                counter++;
            }
            return new FiniteFieldElement(this, polinomOfEl);
        }
        public int FromElementToNumber(FiniteFieldElement element)
        {
            if (element.F != this) throw new Exception("Некорректный элемент, поле, над которым заданы числа не совпадает с использованным полем");
            int temp = 0;
            var counter = 0;
            foreach(var coeff in element.RepresentedPol.listOfCoeff)
            {
                temp += (coeff * (int)Math.Pow(2, counter));
                counter++;
            }
            return temp;
        }


    }
}
