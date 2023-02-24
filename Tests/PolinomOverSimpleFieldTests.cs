using FiniteFields;
namespace Tests;

public class PolinomOverSimpleFieldTests
{
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void EqualsTest1()
    {
        PolinomOverSimpleField p1 = new PolinomOverSimpleField(new int[] { 1, 2, 3, 4 }, 5);
        PolinomOverSimpleField p2 = new PolinomOverSimpleField(new int[] { 1, 2, 3, 4 }, 5);


        Assert.That(p2, Is.EqualTo(p1));
    }
    [Test]
    public void EqualsTest2()
    {
        PolinomOverSimpleField p1 = new PolinomOverSimpleField(new int[] { 1, 2, 3, 3 }, 5);
        PolinomOverSimpleField p2 = new PolinomOverSimpleField(new int[] { 1, 2, 3, 4 }, 5);


        Assert.That(p2, Is.Not.EqualTo(p1));
    }
    [Test]
    public void EqualsTest3()
    {
        PolinomOverSimpleField p1 = new PolinomOverSimpleField(new int[] { 1, 2, 3, 4 }, 7);
        PolinomOverSimpleField p2 = new PolinomOverSimpleField(new int[] { 1, 2, 3, 4 }, 5);


        Assert.That(p2, Is.Not.EqualTo(p1));
    }
    [Test]
    public void GoToNegativeTest()
    {
        PolinomOverSimpleField expect = new(new int[] { 4, 3, 0, 2 }, 5);
        PolinomOverSimpleField polinom = new(new int[] { 1, 2, 0, 3 }, 5);

        Assert.That(-polinom, Is.EqualTo(expect));
    }
    [Test]
    public void AdditionalTest1()
    {
        PolinomOverSimpleField expect =   new(new int[] { 0, 2, 1, 3, 1 }, 5);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1, 2 }, 5);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4, 3, 1 }, 5);

        var actual = polinom1 + polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void AdditionalTest2()
    {
        PolinomOverSimpleField expect =   new(new int[] { 0, 2, 1, 3 }, 5);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1, 2, 0, 1 }, 5);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4, 3, 4 }, 5);

        var actual = polinom1 + polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void SubstractionTest1()
    {
        PolinomOverSimpleField expect =   new(new int[] { 3, 0, 3, 2, 2 }, 5);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1, 2, 0, 1 }, 5);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4, 3, 4 }, 5);

        var actual = polinom1 - polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void SubstractionTest2()
    {
        PolinomOverSimpleField expect =   new(new int[] { 3, 0, 3, 2, 1 }, 5);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1, 2, 0 }, 5);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4, 3, 4 }, 5);

        var actual = polinom1 - polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void SubstractionTest3()
    {
        PolinomOverSimpleField expect =   new(new int[] { 3, 0, 3, 2 }, 5);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1, 2, 0, 4 }, 5);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4, 3, 4 }, 5);

        var actual = polinom1 - polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void MultiplicationTest1()
    {
        PolinomOverSimpleField expect =   new(new int[] { 4, 0, 2, 4 }, 5);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1 }, 5);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4 }, 5);

        var actual = polinom1 * polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void MultiplicationTest2()
    {
        PolinomOverSimpleField expect =   new(new int[] { 4, 5, 3, 4 }, 7);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1 }, 7);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4 }, 7);

        var actual = polinom1 * polinom2;

        Assert.That(actual, Is.EqualTo(expect));
    }
    [Test]
    public void RemainsTest1()
    {
        PolinomOverSimpleField expect   = new(new int[] { 5 }, 7);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1 }, 7);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4 }, 7);

        var actual = polinom2 % polinom1;

        Assert.That(actual, Is.EqualTo(expect));
    }
     [Test]
    public void RemainsTest2()
    {
        
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1 }, 7);
        PolinomOverSimpleField polinom2 = new(new int[] { 1, 1, 4 }, 7);

        var actual = polinom1 % polinom2;

        Assert.That(actual, Is.EqualTo(polinom1));
    }
     [Test]
    public void RemainsTest3()
    {
        PolinomOverSimpleField expect   = new(new int[] { 0 }, 7);
        PolinomOverSimpleField polinom1 = new(new int[] { 4, 1 }, 7);
        PolinomOverSimpleField polinom2 = new(new int[] { 3, 1, 4 }, 7);

        var actual = polinom2 % polinom1;

        Assert.That(actual, Is.EqualTo(expect));
    }
}