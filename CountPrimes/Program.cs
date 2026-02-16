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

    //const double Gamma = 0.577215664901532860606512090082402431042159335;

    const double Gamma = Math.PI / 2.0 - 1;
    static readonly double Log2 = Math.Log(2);
    static double NPi(int n)
    {
        var t = (1L << n);
        var core = Math.Log(t - 1);
        return t * (Gamma * n + 2 * Math.PI * Log2)
            / ((core * core + Pi2));
    }
    static double NPi(double n)
    {
        var t = Math.Pow(2.0, n);
        var core = Math.Log(t - 1);
        return t * (Gamma * n + 2 * Math.PI * Log2)
            / ((core * core + Pi2));
    }

    static double DPi(double x) => NPi(Math.Log2(x));

    static readonly double kk = 1.0 / Math.Tan(0.5 * Math.Atan(Math.Log(2.0)));
    static double GetPi(long max = 1000)
    {
        double p = 4.0;
        for (var n = max; n >= 1; n--)
        {
            p = (2.0 + (1.0 * n) * p / (2.0 * n + 1.0));
        }
        return p * 2;
    }
    static double GetP(double p, long max = 1000000)
    {
        var p0 = p;
        for (var n = max; n >= 1; n--)
        {
            p = (p0 + (1.0 * n) * p / (p0 * n + 1.0));
        }
        return p;
    }
    static double C(double x)
        => 1.0 + Math.Exp(6.0 * x) / (1.0 + 3.0 * Math.Exp(2.0 * x) + 3.0 * Math.Exp(4.0 * x) + Math.Exp(6.0 * x));
    static double GetGammaAt(double x = 0.0, double sign = -1.0)
        => sign * (kk * kk *
            ((Math.Exp(4.0 * x) - Math.Exp(2.0 * x)) / Math.Exp(6.0 * x))
            * Math.Log(C(x))
            + kk * Math.Log(C(x)) / Math.Exp(4.0 * x));

    static double GetGammaByLoop(long S = 10000)
    {
        var q = S * S;
        var s = 0.0;
        for (var n = 1L; n <= S; n++)
        {
            for (var k = 1L; k <= q; k++)
            {
                s += k / (1.0 * n * n * q + 1.0 * n * k);
            }
        }

        return s / q;
    }
    static double GetGamma(double x1 = -1.0, double x2 = 0, long d = 10000000)
    {
        var list = new List<double>();
        var delta = 1.0 / d;
        for (double x = x1; x <= x2; x += delta)
        {
            var g = GetGammaAt(x);
            list.Add(g);
        }
        return list.Max();
    }
    static void Main(string[] args)
    {
        //var pi = GetPi();
        for(long t = 2; t < 100; t++)
        {
            var pt = GetP(t);
            Console.WriteLine("P({0}) = {1}", t, pt);
        }

        var g = GetGamma();
        //g = GetGammaByLoop();
        Console.WriteLine($"Gamma: {g}");
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
