class Polynom:

    # constructor. initialized with a list of coefficients
    def __init__(self, coefficients):
        self.coeffs = []

        # check if any coefficients given
        if coefficients is None:
            coefficients = []

        # convert int to list
        if isinstance(coefficients, int):
            coefficients = list(map(int, str(coefficients)))

        # iterate over the given coefficients. (integers are converted to floats)
        for r in coefficients:
            self.coeffs.append(float(r))
        self.coeffs = self.coeffs

    # evaluate a polynom by setting the variable z equal to the input value x
    def eval(self, x: float):
        # reverse the coeff list
        rev_self_coeffs = self.coeffs[::-1]
        value = 0

        # loops over reversed list of coeffs, adding the corresponding term with z = v to the variable 'value', which is
        # then returned
        # horner's method.
        for n in range(len(rev_self_coeffs)):
            value = value + rev_self_coeffs[n] * (x ** n)
        return value

    def __call__(self, x):
        return self.eval(x)

    # get the degree of polynom
    def getDegree(self):
        return len(self.coeffs) - 1

    # get coefficient of polynom by the given i
    def getCoeff(self, i: int):
        if 0 <= i < len(self.coeffs):
            return self.coeffs[-1 - i]
        else:
            return 0.0

    # set the given cofficient i to given value a
    def setCoeff(self, i: int, a: float):
        self.coeffs[-1 - i] = a

    # adding two polynoms and returning their sum
    def add(self, other):
        rev_poly = []

        # reverse the given polynoms
        rev_self_coeffs = self.coeffs[::-1]
        rev_other_coeffs = other.coeffs[::-1]

        # loops over reversed lists of coeffs, adding together coeffs at the same position in the list (corresponding
        # to the variable raised to the same power), followed by adding extra terms in case the initial polynomial
        # has higher degree
        for n in range(len(rev_self_coeffs)):
            if n <= len(rev_other_coeffs) - 1:
                rev_poly.append(rev_self_coeffs[n] + rev_other_coeffs[n])
            else:
                rev_poly.append(rev_self_coeffs[n])

        # if condition catches extra terms in case the second polynomial has higher degree
        if len(rev_other_coeffs) > len(rev_self_coeffs):
            for n in range(len(rev_self_coeffs), len(rev_other_coeffs)):
                rev_poly.append(rev_other_coeffs[n])

        # the new list of coeffs is created by reversing the constructed list rev_poly, followed by returning the new
        # initialized polynomial
        new_poly = rev_poly[::-1]
        return Polynom(new_poly)

    def __add__(self, other):
        return self.add(other)

    # method for multiplying two polynomials and returning the resulting polynomial
    def mul(self, other):
        # begins by constructing a new polynomial new_poly (corresponding to '0.0') that will be added to
        # and reversing both lists of coeffs
        new_poly = Polynom(0)
        rev_self_coeffs = self.coeffs[::-1]
        rev_other_coeffs = other.coeffs[::-1]

        # loops over reversed list of coeffs, constructing the new polynomial term by term, first multiplying the
        # z**0-term by each term of the second polynomial and then adding the resulting polynomial to new_poly,
        # then multiplying the z**1-term by each term of the second polynomial and then adding the result to new_poly
        for n in range(len(rev_self_coeffs)):
            if n == 0:
                rev_poly_term = [r * rev_self_coeffs[n] for r in rev_other_coeffs]
            else:
                rev_poly_term = [0 for m in range(n)] + \
                                [r * rev_self_coeffs[n] for r in rev_other_coeffs]
            poly_term = rev_poly_term[::-1]
            new_poly = new_poly + Polynom(poly_term)
        return new_poly

    def __mul__(self, other):
        return self.mul(other)

    # string representation of polynomials of the form 'x(0)z**n +...+ x(n-1)z + x(n)', where x(i) are the coeffs
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
