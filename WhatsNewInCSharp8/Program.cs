using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Console;

namespace WhatsNewInCSharp8
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 1. Default Interface Implementation

            // 2. Pattern Matching
            PatternMatching();

            // 3. Using Declarations
            UsingDeclarations();

            // 4. Static Local Function
            StaticLocalFunction();

            // 5. Indices and Ranges
            IndicesAndRanges();

            // 6. Null Coalescing Assignment
            NullCoalescingAssignment();

            // 7. StackAlloc in nested expression
            StackAllocInNestedExpressions();

            ////////////////////

            // 8. Non Nullable Reference Types

            // 9. Async Streams
            await AsyncStreams();
        }

        private static void StackAllocInNestedExpressions()
        {
            Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };
            var ind = numbers.IndexOfAny(stackalloc[] { 2, 4, 6, 8 });
            Console.WriteLine(ind);  // output: 1
        }

            private static void NullCoalescingAssignment()
        {
            List<int> numbers = null;
            int? a = null;

            // syntax now useful also in assignment not in expression
            (numbers ??= new List<int>()).Add(5);
            numbers.Add(a ??= 0);
        }

        private static void IndicesAndRanges()
        {
            var words = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            WriteLine($"Last one: {words[^1]}");
            var range = 1..4; // System.Index
            var range1_4 = words[range]; // System.Range
            var range__2__0 = words[^2..^0];
            var allWords = words[..]; 
            var firstPhrase = words[..4]; 
            var lastPhrase = words[6..];
        }

        private static void StaticLocalFunction()
        {
            int y = 5;
            int x = 7;
            _ = Add(x, y);

            static int Add(int left, int right) => left + right;
        }

        private static void UsingDeclarations()
        {
            // implicit block definition
            using var reader = new StreamReader("File.txt");
        }

        static void PatternMatching()
        {
            // switch expression
            var x = 1;
            var y = x switch
            {
                1 => 10, // case:
                2 => 20,
                _ => 30 // discard
            };

            // property patterns
            IDoMany s = default;
            var f = s switch
            {
                { What: "WW" } => "ew",
                { Azz: "EE" } => "ff",
                _ => "oo"
            };

            // positional pattern
            var ff = s switch
            {
                var (w, a) when w == "WW" && a == "" => "",
                var (w, a) when a == "AA" && w == "" => "",
                var (_,a) when a == "BB" => "",
                _ => ""
            };

            // tuple patterns
            var aa = "";
            var bb = "";
            var xd = (aa, bb) switch
            {
                ("", "") => "Hello",
                (_, _) => "OI"
            };
        }

        public static async System.Collections.Generic.IAsyncEnumerable<int> GenerateSequence()
        {
            for (int i = 0; i < 20; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }

        private static async Task AsyncStreams()
        {
            await foreach (var number in GenerateSequence())
            {
                Console.WriteLine(number);
            }
        }
    }
}
