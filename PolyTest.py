import math
import random
import time
import timeit
import datetime
from PolynomB import *

def Title(str):
	input()
	print()
	print(str)
	print('-'*79)
	input()


x = [-7.0, -5.0, -1.0, 1.0, 2.0, 4.0, 8.0]

sinFact = [0.0, 1.0, 0.0, -(1.0/6.0), 0.0, (1.0/120.0), 0.0,
        -(1.0/5040.0), 0.0, (1.0/362880.0), 0.0, -(1.0/39916800.0), 0.0,
        (1.0/6227020800), 0.0, -(1.0/1307674368000.0), 0.0,
        (1.0/355687428096000.0), 0.0, -(1.0/121645100408832000.0)]

nCalls = 1000000

Title("Coefficient Test (getDegree(), getCoeff(), setCoeff())")

p = Polynom(x)
print("degree(p):", p.getDegree())
for i in range(0, p.getDegree()+1):
	print("a_%i: %f" % (i, p.getCoeff(i)))
print()
for i in range(0, p.getDegree()+1):
	p.setCoeff(i, i)
for i in range(0, p.getDegree()+1):
	print("a_%i: %f" % (i, p.getCoeff(i)))
	
Title("OutOfBound Test (Exception expected)")

p = Polynom([1.0])
try:
	print(p.getCoeff(-2))
except Exception as err:
	print("Exception caught:", err)
try:
	print(p.getCoeff(2))
except Exception as err:
	print("Exception caught:", err)

Title("Addition Test (add())")

print("(1.0, 2.0, 3.0) + (3.0, 2.0, 1.0)")
p1 = Polynom([1.0, 2.0, 3.0])
p2 = Polynom([3.0, 2.0, 1.0])
p3 = Polynom.add(p1, p2)
print("degree:", p3.getDegree())
for i in range(0, p3.getDegree()+1):
	print("a_%i: %f" % (i, p3.getCoeff(i)))
print()

print("(1.0, 2.0, 3.0) + (1.0)")
p1 = Polynom([1.0, 2.0, 3.0])
p2 = Polynom([1.0])
p3 = Polynom.add(p1, p2)
print("degree:", p3.getDegree())
for i in range(0, p3.getDegree()+1):
	print("a_%i: %f" % (i, p3.getCoeff(i)))
print()

print("(1.0) + (-1.0, 2.0, 3.0)")
p1 = Polynom([1.0])
p2 = Polynom([-1.0, 2.0, 3.0])
p3 = Polynom.add(p1, p2)
print("degree:", p3.getDegree())
for i in range(0, p3.getDegree()+1):
	print("a_%i: %f" % (i, p3.getCoeff(i)))
print()

print("(1.0, 2.0, 3.0) + (1.0, -2.0, -3.0)")
p1 = Polynom([1.0,  2.0,  3.0])
p2 = Polynom([1.0, -2.0, -3.0])
p3 = Polynom.add(p1, p2)
print("degree:", p3.getDegree())
for i in range(0, p3.getDegree()+1):
	print("a_%i: %f" % (i, p3.getCoeff(i)))
print()

print("(1.0, 2.0, 3.0) + (-1.0, -2.0, -3.0)")
p1 = Polynom([ 1.0,  2.0,  3.0])
p2 = Polynom([-1.0, -2.0, -3.0])
p3 = Polynom.add(p1, p2)
print("degree:", p3.getDegree())
for i in range(0, p3.getDegree()+1):
	print("a_%i: %f" % (i, p3.getCoeff(i)))
print()

Title("Lagrange Test (mul(), eval())")

pr = Polynom([1.0])
p  = [0] * len(x)
for i in range(len(x)):
	p[i] = Polynom([-x[i], 1.0])
for i in range(len(p)):
	pr = Polynom.mul(pr, p[i])
print("degree:", pr.getDegree ())
for i in range(0, pr.getDegree()+1):
	print("a_%i: %f" % (i, pr.getCoeff(i)))
for val in x:
	print("pr(%f): %f" % (val, pr.eval(val)))
	
Title("sin(x) Tests")

pr = Polynom(sinFact)
print("degree   : %d" % (pr.getDegree()))
print("sin(0)   : %f" % (pr.eval(0.0)))
print("sin(pi/2): %f" % (pr.eval(math.pi/2.0)))
print("sin(pi)  : %f" % (pr.eval(math.pi)))

Title("Performance (%d calls of eval())" % (nCalls))

r = random.random()
t1 = timeit.default_timer()
for i in range(nCalls):
	d = pr.eval(r)
t2 = timeit.default_timer()
print("duration for %d calls of eval(): %s" % (nCalls, datetime.timedelta(seconds=t2-t1)))

