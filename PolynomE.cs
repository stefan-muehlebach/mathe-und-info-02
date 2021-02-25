using System;
using System.Collections.Generic;
using System.Text;

    public class Polynom
    {
        //a0 + a1 x + a2 x^2 + a3 x^3 + .....

        private double[] Coeffs;

        public Polynom(int degree)
        {
            Coeffs = new double[degree];
        }

        public Polynom(params double[] inPoly)
        {
            Coeffs = inPoly;
        }

        public double eval(double x)
        {
            double result = Coeffs[Coeffs.Length-1];
            for (int i = Coeffs.Length-2; i >= 0; i--)
            {
                result = result * x + Coeffs[i];
            }
            
            return result;
        }

        public double getCoeff(int i) {
             return Coeffs[i];
        }

        public void setCoeff(int i, double a) {
             Coeffs[i] = a;
        }

        public int getDegree()
        {
            return Coeffs.Length-1;
        }

        public double[] getCoeffs()
        {
            return Coeffs;
        }

        public static Polynom add(Polynom polA, Polynom polB)
        {
            var len = polA.Coeffs.Length > polB.Coeffs.Length ? polA.Coeffs.Length : polB.Coeffs.Length;
            var newPol = new double[len];

            for (int i = 0; i < polA.Coeffs.Length; i++)
            {
                newPol[i] += polA.Coeffs[i];
            }
            for (int i = 0; i < polB.Coeffs.Length; i++)
            {
                newPol[i] += polB.Coeffs[i];
            }

            return new Polynom(newPol);
        }
        public static Polynom mul(Polynom polA, Polynom polB)
        {
            var len = polA.getDegree() + polB.getDegree() + 1;
            var newPol = new double[len];

            for (int i = 0; i < polA.Coeffs.Length; i++)
            {
                for (int j = 0; j < polB.Coeffs.Length; j++)
                {
                    newPol[i + j] += polA.Coeffs[i] * polB.Coeffs[j];
                }
            }

            return new Polynom(newPol);
        }

        public static Polynom operator *(Polynom polA, Polynom polB)
        {
            return mul(polA, polB);
        }

        public static Polynom operator +(Polynom polA, Polynom polB)
        {
            return add(polA, polB);
        }
    }
