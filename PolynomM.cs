using System;

/*
 * Polynom --
 *
 * Klasse fuer die Darstellung von Polynomen beliebigen Grades.
 */

class Polynom {
    private double[] a;
    
    /* Polynom --
     *
     * Erstellt ein Polynom des angegebenen Grades; alle Koeffizienten werden
     * mit 0.0 initialisiert.
     */
    public Polynom (int degree) {
        a = new double[degree+1];
    }
    
    /* Polynom --
     *
     * Erstellt ein Polynom mit den angegebenen Koeffizienten, wobei p[0]
     * der Koeffizient von x^0 ist.
     */
    public Polynom (params double[] p) {
        int i;
        
        a = new double[p.Length];
        
        for (i=0; i<p.Length; i++) {
            a[i] = p[i];
        }
    }
    
    /* getCoeff --
     *
     * Retourniert den Koeffizienten von x^i.
     */
    public double getCoeff (int i) {
        return a[i];
    }
    
    /* setCoeff --
     *
     * Setzt den Koeffizienten von x^i auf b.
     */
    public void setCoeff (int i, double b) {
        a[i] = b;
    }
    
    /* getDegree --
     *
     * Liefert den Grad des Polynoms.
     */
    public int getDegree () {
        return a.Length-1;
    }
    
    /* eval --
     *
     * Wertet das Polynom an der Stelle x aus. Verwendet das Horner-Schema
     * zur effizienten Auswertung des Polyoms.
     */
    public double eval (double x) {
        double s = a[a.Length-1];
        
        for (int i=a.Length-2; i>=0; i--) {
            s = s * x;
            if (a[i] != 0.0) {
                s = s + a[i];
            }
        }
        
        return s;
    }

    /* eval2 --
     *
     * Wertet das Polynom an der Stelle x aus. Fuer die Auswertung wird die
     * Methode Math.Pow() verwendet.
     */
    public double eval2 (double x) {
        double s = 0.0;

        for (int i=0; i<a.Length; i++) {
            if (a[i] != 0.0) {
                s = s + a[i] * Math.Pow (x, i);
            }
        }

        return s;
    }

    /* add --
     *
     * Addiert zwei Polynome und liefert ein Polynom als Resultat zurueck.
     */
    public static Polynom add (Polynom p1, Polynom p2) {
        int l1, l2, i, j;
        Polynom t1;
        Polynom p;
        double[] a;
        
        if (p1.getDegree () < p2.getDegree ()) {
            t1 = p1;
            p1 = p2;
            p2 = t1;
        }
        l1 = p1.a.Length;
        l2 = p2.a.Length;

        a = new double[l1];

        for (i = 0; i<l2; i++) {
            a[i] = p1.a[i] + p2.a[i];
        }
        for ( ; i<l1; i++) {
            a[i] = p1.a[i];
        }
        for (i--; (i >= 0) && (a[i] == 0); i--) { }

        if (i < 0) {
            p = new Polynom (0);
        } else {
            p = new Polynom (i);
            for (j=0; j<=i; j++) {
                p.a[j] = a[j];
            }
        }
        
        return p;
    }
    
    public static Polynom operator+ (Polynom p1, Polynom p2) {
        return add (p1, p2);
    }

    /* mul --
     *
     * Multipliziert zwei Polynome und liefert als Resultat ein Polynom
     * zurueck.
     */
    public static Polynom mul (Polynom p1, Polynom p2) {
        int i, j;
        Polynom p;
        
        p = new Polynom (p1.getDegree() + p2.getDegree ());
        for (i=0; i<p1.a.Length; i++) {
            if (p1.a[i] == 0.0) {
                continue;
            }
            for (j=0; j<p2.a.Length; j++) {
                if (p2.a[j] == 0.0) {
                    continue;
                }
                p.a[i+j] += p1.a[i] * p2.a[j];
            }
        }
        
        return p;
    }
    
    public static Polynom operator* (Polynom p1, Polynom p2) {
        return mul (p1, p2);
    }

    /* ToString --
     *
     * Erstellt eine Text-Repraesentation des Polynoms.
     *
     * Beispiel: 1.0, 2.0, -1.0
     * Fuer    : 1 + 2x - x^2
     */
    public override string ToString () {
        return String.Join (", ", a);
    }
}

