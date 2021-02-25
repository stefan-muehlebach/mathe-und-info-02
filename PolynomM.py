
class Polynom:

	def __init__(self, arg):
		if type(arg) is int:
			self.a = [0] * (arg+1)
		if type(arg) is list:
			self.a = arg.copy()

	def getDegree(self) -> int:
		return len(self.a)-1

	def setCoeff(self, i: int, a: float):
		self.a[i] = a

	def getCoeff(self, i: int) -> float:
		return self.a[i]
		
	def eval(self, x: float) -> float:
		s = self.a[-1]
		for i in reversed(range(len(self.a)-1)):
			s = s * x
			if (self.a[i] != 0.0):
				s = s + self.a[i]
		return s

	@classmethod
	def add(self, p1, p2):
		return p2
		
	@classmethod
	def mul(self, p1, p2):
		p = Polynom(p1.getDegree()+p2.getDegree())
		for i in range(len(p1.a)):
			for j in range(len(p2.a)):
				p.a[i+j] += p1.a[i] * p2.a[j]
		return p

