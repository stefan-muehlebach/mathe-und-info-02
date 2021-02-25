class Polynom:

    def __init__(self, coefficients):
        self.coeffs = []

        if coefficients is None:
            coefficients = []

        if isinstance(coefficients, int):
            coefficients = list(map(int, str(coefficients)))

        for r in coefficients:
            self.coeffs.append(float(r))
        self.coeffs = self.coeffs

    def eval(self, x: float):
        # reverse the coeff list
        rev_self_coeffs = self.coeffs[::-1]
        value = 0

        for n in range(len(rev_self_coeffs)):
            value = value + rev_self_coeffs[n] * (x ** n)
        return value

    def mul(self, other):
        new_poly = Polynom(0)
        rev_self_coeffs = self.coeffs[::-1]
        rev_other_coeffs = other.coeffs[::-1]

        for n in range(len(rev_self_coeffs)):
            if n == 0:
                rev_poly_term = [r * rev_self_coeffs[n] for r in rev_other_coeffs]
            else:
                rev_poly_term = [0 for m in range(n)] + \
                                [r * rev_self_coeffs[n] for r in rev_other_coeffs]
            poly_term = rev_poly_term[::-1]
            new_poly = new_poly + Polynom(poly_term)
        return new_poly


    def getDegree(self):
        return len(self.coeffs) - 1

    def getCoeff(self, i: int):
        if 0 <= i < len(self.coeffs):
            return self.coeffs[-1 - i]
        else:
            return 0.0

    def setCoeff(self, i: int, a: float):
        self.coeffs[-1 - i] = a

    def add(self, other):
        rev_poly = []

        rev_self_coeffs = self.coeffs[::-1]
        rev_other_coeffs = other.coeffs[::-1]

        for n in range(len(rev_self_coeffs)):
            if n <= len(rev_other_coeffs) - 1:
                rev_poly.append(rev_self_coeffs[n] + rev_other_coeffs[n])
            else:
                rev_poly.append(rev_self_coeffs[n])

        if len(rev_other_coeffs) > len(rev_self_coeffs):
            for n in range(len(rev_self_coeffs), len(rev_other_coeffs)):
                rev_poly.append(rev_other_coeffs[n])

        new_poly = rev_poly[::-1]
        return Polynom(new_poly)

    def __add__(self, other):
        return self.add(other)

    def __call__(self, x):
        return self.eval(x)

    def __mul__(self, other):
        return self.mul(other)

    def __str__(self):
        string = ""
        add_str = " + "
        for n in range(len(self.coeffs)):
            n_coeff = str(self.coeffs[n])
            if n < len(self.coeffs) - 2:
                string = string + n_coeff + "z**" + \
                         str(len(self.coeffs) - n - 1) + add_str
            elif n < len(self.coeffs) - 1:
                string = string + n_coeff + "z" + add_str
            else:
                string = string + n_coeff
        return string

    def __repr__(self):
        return str(self)
