using FiniteFields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class FiniteFieldElementTests
    {

        // q = x^8 + x^4 + x^3 + x + 1

        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void EqualsTest1()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element1 = new(field, new int[] { 1, 1, 1, 1, 1 });
            FiniteFieldElement element2 = new(field, new int[] { 1, 1, 1, 1, 1 });

            bool actual = element1 == element2;

            Assert.IsTrue(actual);
        }
          [Test]
        public void EqualsTest2()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element1 = new(field, new int[] { 1, 0, 1, 1, 1 });
            FiniteFieldElement element2 = new(field, new int[] { 1, 1, 1, 0 });

            bool actual = element1 != element2;

            Assert.IsTrue(actual);
        }
          [Test]
        public void AdditionalTest1()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element1 = new(field, new int[] { 1, 0, 1, 1, 1 });
            FiniteFieldElement element2 = new(field, new int[] { 1, 1, 1, 0 });
            FiniteFieldElement expect = new(field, new int[] { 0, 1, 0, 1, 1 });


            var actual = element1 + element2;

            Assert.That(actual, Is.EqualTo(expect));
        }
          [Test]
        public void AdditionalTest2()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element1 = new(field, new int[] { 1, 0, 1, 1, 1 });
            FiniteFieldElement element2 = new(field, new int[] { 1, 1, 1, 0 ,1});
            FiniteFieldElement expect = new(field, new int[] { 0, 1, 0, 1 });


            var actual = element1 + element2;

            Assert.That(actual, Is.EqualTo(expect));
        }
        [Test]
        public void MultiplicactionTest1()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element1 = new(field, new int[] { 1, 0, 1, 0, 1 });
            FiniteFieldElement element2 = new(field, new int[] { 1, 1, 1 });
            var expect = new FiniteFieldElement(field, new int[] { 1, 1, 0, 1, 0, 1, 1 });

            Assert.That(expect, Is.EqualTo(element1 * element2 ));

        }
        [Test]
        public void MultiplicactionTest2()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element1 = new(field, new int[] { 1, 0, 1, 0, 1, 1, 1, 1 });
            FiniteFieldElement element2 = new(field, new int[] { 1, 1, 1 });
            var expect = new FiniteFieldElement(field, new int[] { 1, 0, 1, 1, 1, 1, 1, 1 });

            Assert.That(expect, Is.EqualTo(element1 * element2 ));

        }
        [Test]
        public void PowTest1()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element = new(field, new int[] { 1, 0, 1});
            var expect = new FiniteFieldElement(field, new int[] {1, 0, 1, 0, 1, 0, 1 });

            Assert.That(expect, Is.EqualTo(element.Pow(3)));

        }
        [Test]
        public void PowTest2()
        {
            PolinomOverSimpleField q = new(new int[] { 1, 1, 0, 1, 1, 0, 0, 0, 1 }, 2);
            FiniteField field = new(2, 8, q);
            FiniteFieldElement element = new(field, new int[] { 1, 0, 1});
            var expect = new FiniteFieldElement(field, new int[] {1, 0, 0, 0, 0, 1, 0, 1 });

            Assert.That(expect, Is.EqualTo(element.Pow(6)));

        }

    }
}
