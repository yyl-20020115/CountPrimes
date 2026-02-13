namespace CountPrimes;

internal class Program
{
    static bool IsPrime(long n)
    {
        if (n <= 1) return false;
        if (n <= 3) return true;
        if (n % 2 == 0 || n % 3 == 0) return false;
        for (long i = 5; i * i <= n; i += 6)
        {
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
        }
        return true;
    }
    const double Pi2 = Math.PI * Math.PI;

    const double Gamma = 0.577215664901532860606512090082402431042159335;

    static double NPi(int n)
    {
        var t = (1L << n);
        var s = t - 1;
        var core = Math.Log(s);
        return t * (Gamma * n + 2 * Math.PI * Math.Log(2))
            / ((core * core + Pi2));
    }
    static double NPi(double n)
    {
        var t = Math.Pow(2.0, n);
        var s = t - 1;
        var core = Math.Log(s);
        return t * (Gamma * n + 2 * Math.PI * Math.Log(2))
            / ((core * core + Pi2));
    }

    static double DPi(double x) => NPi(Math.Log2(x));
    static void Main(string[] args)
    {
        long total = 0;

        for (int c = 1; c < 1000; c++)
        {
            var p = DPi(c);
            var b = IsPrime(c);
            if (b) total++;
            var d = p - total;
            if (b)
            {
                Console.WriteLine($"count(p) <= {c} : {total}\tPi(p)={p:N8}\terr={d / total:N8}");
            }
            else
            {
                Console.WriteLine($"count(p) <= {c} : {total}\tPi(p)={p:N8}");
            }
        }

        long i = 0;
        total = 0;
        for (int n = 1; n <= 35; n++)
        {
            long m = 1L << (n);
            for (; i <= m; i++)
            {
                if (IsPrime(i)) total++;
            }
            var im = NPi(n);
            var d = im - total;
            Console.WriteLine($"count(p) <= 2^{n} : {total}\tPi(p)={im:N8}\terr={d / total:N8}");
        }
    }
}
