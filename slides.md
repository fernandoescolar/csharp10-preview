# Markdown Demo

---

## External 1.1

Content 1.1

Note: This will only appear in the speaker notes window.

---

## External 1.2

Content 1.2

---

## External 2

Content 2.1

---

## External 3.1

Content 3.1

---

## External 3.2

Content 3.2

---

## External 3.3

![External Image](https://s3.amazonaws.com/static.slid.es/logo/v2/slides-symbol-512x512.png)

---

## Code 1

```js [1-2|3|4]
let a = 1;
let b = 2;
let c = x => 1 + 2 + x;
c(3);
```

---

### Code 2

```csharp [1-2|3|4]
const int a = 1;
var b = 2;
var c = x => 1 + 2 + x;
c(3);
```

---

```csharp []
using System.IO.Compression;

#pragma warning disable 414, 3021

namespace MyApplication
{
    [Obsolete("...")]
    class Program : IInterface
    {
        public static List<int> JustDoIt(int count)
        {
            Span<int> numbers = stackalloc int[length];
            Console.WriteLine($"Hello {Name}!");
            return new List<int>(new int[] { 1, 2, 3 })
        }
    }
}
```