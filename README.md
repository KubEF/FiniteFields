# Библиотека для работы с конечными полями

Сама библиотека представляет собой по сути 3 класса: PolinomOverSimpleField, FiniteField, FiniteFieldsElement.

Первый позволяет работать с многочленами над конечными полями $F_p[X]$, где $p$ -- простое число. Там реализованы сложение, вычитание, умножение, взятие остатка,
и сравнение (равны или нет). На основе этих операций далее будет построено поле $F_{p^n}$, которое представлено в виде $F_p[X]/(q)$, где $q$ -- это неприводимый многочлен т.ч.
$F_p[X]/(q) \simeq F_{p^n}$ (такой точно есть из курса алгебры 3 семестра <<МатОбеса>> СПбГУ).

Второй является по сути исключительно служебным, хранит в себе всю информацию о поле, с которым работаем (по сути всего 3 переменные $p, n, q$).

Третий представляет собой элементы этого поля с реализованными операциями.

Все арифметические операции представлены в наиболее простой и интуитивной форме.

## Пример использования

```c#
// создание полинома q = x^8 + x^4 + x^3 + x + 1 над простым полем F_2
var q = new PolinomOverSimpleField(new int[] {1, 1, 0, 1, 1, 0, 0, 0, 1}, 2);
// создание поля F_{2^8}
var Field = new FiniteField(2, 8, q);
// получение 1 из этого поля
var one = Field.GetOne();
// сложение двух единиц
var result = one + one;
```

Так же в библиотеке есть отдельный инструмент для работы над полем $F_{2^n}$:

```c#
var GF256 = new BinaryFiniteField(8, q);
// преобразование числа в байты
var Byte = GF256.FromNumberToElement(200);
// и наоборот
var Number = GF256.FromElementToNumber(Byte);
```
