using System;

class Polynom
    {
        private System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        private double[] _Polynom;

        /// <summary>
        /// Erstellen eines leeren Polynomes mit einem bestimmten Grad.
        /// </summary>
        /// <param name="koeff">Grad des Polynomes</param>
        public Polynom(int grad)
        {

            _Polynom = new double[grad];
        }

        /// <summary>
        /// Erstellt ein Polynom mit den angegebenen Koeffizienten.
        /// </summary>
        /// <param name="values">Koeffizienten</param>
        public Polynom(params double[] values)
        {
            _Polynom = values;
        }

        /// <summary>
        /// Wertet das Polynom an der Stelle x aus und retourniert das Resultat.
        /// </summary>
        /// <param name="a">Float für X</param>
        /// <returns>Berechneter Wert</returns>
        public double eval(double a)
        {
            int grad = this.getDegree();
            double val = this._Polynom[grad];

            for (int i = grad-1; i >= 0; i--)
            {
                val = this._Polynom[i] + val * a;
            }

            return val;
        }

        /// <summary>
        /// Ermittelt den Grad eines Polynomes
        /// </summary>
        /// <returns>INT, Grad des Polynomes</returns>
        public int getDegree()
        {
            return _Polynom.Length - 1;
        }

        /// <summary>
        /// Retourniert den Koeffizienten von pos
        /// </summary>
        /// <param name="pos">Position des Koeffizienten</param>
        /// <returns>FLOAT - Koeffizient</returns>
        public double getCoeff(int pos)
        {
            return _Polynom[pos];
        }

        /// <summary>
        /// Setzt den Koeffizienten von pos auf value
        /// </summary>
        /// <param name="pos">Position des Koeffizienten</param>
        /// <param name="value">Wert des Koeffizienten</param>
        public void setCoeff(int pos, double value)
        {
            _Polynom[pos] = value;
        }




        #region Addition
        /// <summary>
        /// Addieren zweier Polynome
        /// </summary>
        /// <param name="p1">erstes Polynom</param>
        /// <param name="p2">zweites Polynom</param>
        /// <returns></returns>
        public static Polynom add(Polynom p1, Polynom p2)
        {
            return p1.getDegree() < p2.getDegree() ? addSorted(p1, p2) : addSorted(p2, p1);
        }

        /// <summary>
        /// Addieren zweier Polynome entsprechend ihren Grades
        /// </summary>
        /// <param name="lPoly">Polynom des kleineren Grades</param>
        /// <param name="hPoly">Polynom des höheren Grades</param>
        /// <returns></returns>
        private static Polynom addSorted(Polynom lPoly, Polynom hPoly)
        {
            int i = 0;
            foreach(double f in lPoly._Polynom)
            {
                hPoly._Polynom[i] = hPoly._Polynom[i] + lPoly._Polynom[i];
                i++;
            }
            return hPoly;
        }
        #endregion
        #region Multiplikation

        public static Polynom mul(Polynom p1, Polynom p2)
        {
            return p1.getDegree() < p2.getDegree() ? mulSorted(p1, p2) : mulSorted(p2, p1);
        }


        /// <summary>
        /// Multiplikation zweier Polynome entsprechend ihren Grades
        /// </summary>
        /// <param name="lPoly">Polynom des kleineren Grades</param>
        /// <param name="hPoly">Polynom des höheren Grades</param>
        /// <returns></returns>
        private static Polynom mulSorted(Polynom lPoly, Polynom hPoly)
        {
            //Länge des resultierenden Polynomes bestimmen
            int grad = lPoly.getDegree() + hPoly.getDegree() + 1;

            // Variable für das Resultat
            double[] res = new double[grad];

            // Berechnen
            for(int i = 0; i <= hPoly.getDegree(); i++)
                {
                    for(int k = 0; k <= lPoly.getDegree(); k++)
                    {
                        int currGrad = i + k;
                        res[currGrad] = res[currGrad] + (hPoly._Polynom[i] * lPoly._Polynom[k]);
                    }
                }

            //Nullable Array in ein normales Array konvertieren
            return new Polynom(res);
        }
        #endregion


    }
