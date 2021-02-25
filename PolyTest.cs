using System.IO;
using System;

class Program {

    static void Title (String s) {
        Console.Read ();
        Console.WriteLine ();
        Console.WriteLine (s);
        Console.WriteLine (new String ('-', 79));
        Console.Read ();
    }

    static void Main() {
        Polynom[] p;
        Polynom pr;
        Polynom p1, p2, p3;
        double[] x = {-7.0, -5.0, -1.0, 1.0, 2.0, 4.0, 8.0};
        double[] sinFact = { 0, 1.0, 0, -(1.0/6.0), 0, (1.0/120.0), 0,
                -(1.0/5040.0), 0, (1.0/362880.0), 0, -(1.0/39916800.0), 0,
                (1.0/6227020800), 0, -(1.0/1307674368000.0), 0,
                (1.0/355687428096000.0), 0, -(1.0/121645100408832000.0) };
        int i;
        int nCalls = 1000000;
        DateTime t1, t2;
        TimeSpan s;
        double r, d;
        Random rnd;

        Title ("Coefficient Test (getDegree(), getCoeff(), setCoeff())");

        pr = new Polynom (x);
        Console.WriteLine ("degree(p): " + pr.getDegree ());
        for (i=0; i<=pr.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + pr.getCoeff (i));
        }
        x[1] = 1.0;
        for (i=0; i<=pr.getDegree (); i++) {
            pr.setCoeff (i, i);
        }
        Console.WriteLine ();
        for (i=0; i<=pr.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + pr.getCoeff (i));
        }

        Title ("OutOfBound Test (Exception expected)");

        pr = new Polynom (1.0);
        try {
            Console.WriteLine (pr.getCoeff (-1));
        } catch (Exception e) {
            Console.WriteLine ("Exception: " + e);
        }
        try {
            Console.WriteLine (pr.getCoeff (2));
        } catch (Exception e) {
            Console.WriteLine ("Exception: " + e);
        }

        Title ("Addition Test (add())");

        Console.WriteLine ("(1.0, 2.0, 3.0) + (3.0 + 2.0 + 1.0)");
        p1 = new Polynom (1.0, 2.0, 3.0);
        p2 = new Polynom (3.0, 2.0, 1.0);
        p3 = Polynom.add (p1, p2);
        Console.WriteLine ("degree: " + p3.getDegree ());
        for (i=0; i<=p3.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + p3.getCoeff (i));
        }
        Console.WriteLine ();

        Console.WriteLine ("(1.0, 2.0, 3.0) + (1)");
        p1 = new Polynom (1.0, 2.0, 3.0);
        p2 = new Polynom (1.0);
        p3 = Polynom.add (p1, p2);
        Console.WriteLine ("degree: " + p3.getDegree ());
        for (i=0; i<=p3.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + p3.getCoeff (i));
        }
        Console.WriteLine ();

        Console.WriteLine ("(1.0) + (-1.0, 2.0, 3.0)");
        p1 = new Polynom (1.0);
        p2 = new Polynom (-1.0, 2.0, 3.0);
        p3 = Polynom.add (p1, p2);
        Console.WriteLine ("degree: " + p3.getDegree ());
        for (i=0; i<=p3.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + p3.getCoeff (i));
        }
        Console.WriteLine ();

        Console.WriteLine ("(1.0, 2.0, 3.0)) + (1.0, -2.0, -3.0)");
        p1 = new Polynom (1.0,  2.0,  3.0);
        p2 = new Polynom (1.0, -2.0, -3.0);
        p3 = Polynom.add (p1, p2);
        Console.WriteLine ("degree: " + p3.getDegree ());
        for (i=0; i<=p3.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + p3.getCoeff (i));
        }
        Console.WriteLine ();

        Console.WriteLine ("(1.0, 2.0, 3.0)) + (-1.0, -2.0, -3.0)");
        p1 = new Polynom ( 1.0,  2.0,  3.0);
        p2 = new Polynom (-1.0, -2.0, -3.0);
        p3 = Polynom.add (p1, p2);
        Console.WriteLine ("degree: " + p3.getDegree ());
        for (i=0; i<=p3.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + p3.getCoeff (i));
        }
        Console.WriteLine ();

/*
        Console.WriteLine ("degree(p3): " + p3.getDegree ());
        for (i=0; i<=2; i++) {
            Console.WriteLine ("a_"+i+": " + p3.getCoeff (i));
        }
*/

        Title ("Lagrange Test (mul(), eval())");

        pr = new Polynom (1.0);
        p = new Polynom[x.Length];
        for (i=0; i<x.Length; i++) {
            p[i] = new Polynom (-x[i], 1.0);
        }
        for (i=0; i<p.Length; i++) {
            pr = Polynom.mul (pr, p[i]);
        }
        Console.WriteLine ("degree: " + pr.getDegree ());
        for (i=0; i<=pr.getDegree (); i++) {
            Console.WriteLine ("a_"+i+": " + pr.getCoeff (i));
        }
        for (i=0; i<x.Length; i++) {
            Console.WriteLine ("pr("+x[i]+"): " + pr.eval(x[i]));
        }

        Title ("sin(x) Tests");

        pr = new Polynom (sinFact);
        Console.WriteLine ("degree: " + pr.getDegree ());
        Console.WriteLine ("sin(0.0) : " + pr.eval (0.0));
        Console.WriteLine ("sin(pi/2): " + pr.eval (Math.PI/2.0));
        Console.WriteLine ("sin(pi)  : " + pr.eval (Math.PI));

        Title ("Performance (" + nCalls + " eval() calls)");

        rnd = new Random ();
        r = rnd.NextDouble ();
        d = 0.0;
        t1 = DateTime.Now;
        for (i=0; i<nCalls; i++) {
            d = pr.eval (r);
        }
        t2 = DateTime.Now;
        s  = t2 - t1;
        Console.WriteLine ("d: " + d);
        Console.WriteLine ("duration for " + nCalls + " calls of eval(): " + s);
    }
}

