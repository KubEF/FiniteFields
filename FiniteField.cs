﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteFields
{
    /// <summary>
    /// Finite Field of p^n elements class. It presents in the form F_p[X]/(q).
    /// </summary>

    public class FiniteField
    {
        public int p { get; }
        public int n { get; }
        public PolinomOverSimpleField q { get; }

        public FiniteField(int p, int n, PolinomOverSimpleField q)
        {
            if (q.deg == n & p > 1)
            {
                this.n = n;
                this.p = p;
                this.q = q;
            }
            else
            {
                throw new Exception("Невозможно построить поле с заданными параметрами");
            }
        }
        public FiniteFieldElement GetZero()
        {
            return new FiniteFieldElement(this, new PolinomOverSimpleField(new int[1] { 0 }, p));
        }
        public FiniteFieldElement GetOne()
        {
            return new FiniteFieldElement(this, new PolinomOverSimpleField(new int[1] { 1 }, p));
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            FiniteField F = (obj as FiniteField)!;
            return F.n == this.n & F.p == this.p & this.q.Equals(F.q);
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
    public class FiniteFieldElement
    {
        public FiniteField F { get; }
        public PolinomOverSimpleField RepresentedPol { get; }
        public FiniteFieldElement(FiniteField F, PolinomOverSimpleField representedPol)
        {
            this.F = F;
            if(representedPol.deg < F.n )
                RepresentedPol = representedPol;
            else
            {
                throw new Exception("Передан некорректный представляющий многочлен");
            }

        }
        public static FiniteFieldElement operator +(FiniteFieldElement a) => a;
        public static FiniteFieldElement operator -(FiniteFieldElement a) => new FiniteFieldElement(a.F, -a.RepresentedPol);
        
        
        public static FiniteFieldElement operator +(FiniteFieldElement a, FiniteFieldElement b)
        {
            if (!a.F.Equals(b.F)) throw new Exception("Попытка сложить числа из разных полей");
            return new FiniteFieldElement(a.F, a.RepresentedPol + b.RepresentedPol);
        }
        public static FiniteFieldElement operator -(FiniteFieldElement a, FiniteFieldElement b) => a + (-b);

        public static FiniteFieldElement operator *(FiniteFieldElement a, FiniteFieldElement b)
        {
            if (!a.F.Equals(b.F)) throw new Exception("Попытка сложить числа из разных полей");
            PolinomOverSimpleField resultRepresentedPol = (a.RepresentedPol * b.RepresentedPol) % a.F.q;
            return new FiniteFieldElement(a.F, resultRepresentedPol);
        }
        public static FiniteFieldElement operator /(FiniteFieldElement a, FiniteFieldElement b) => a * b.GetReverse();
        public FiniteFieldElement Pow (int y)
        {
            y = Math.Sign(y) * ((Math. Abs(y) + 1) % (int)Math.Pow(F.p, F.n));
            if (y == 1) return this;
            else if(y == 0) return new FiniteFieldElement(F, new PolinomOverSimpleField(new int[1] {1}, F.p));
            else
            {
                if (y > 1)
                {
                    return Pow(y / 2);
                }
                else 
                {
                    return Pow(Math.Abs(y)).GetReverse();
                }
            }
        }
        public FiniteFieldElement GetReverse()
        {
            return Pow((int)Math.Pow(F.p, F.n) - 2);
        }

        public static bool operator ==(FiniteFieldElement a, FiniteFieldElement b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(FiniteFieldElement a, FiniteFieldElement b)
        {
            return !a.Equals(b);
        }
        public override bool Equals(object? obj)
        {
            FiniteFieldElement elem = (obj as FiniteFieldElement)!;
            return F.Equals(elem.F) & RepresentedPol.Equals(elem.RepresentedPol);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
