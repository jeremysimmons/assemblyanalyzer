using System;

class Metrics {
	IAssembly assembly;
	
	public Metrics(IAssembly assembly) {
		this.assembly = assembly;
	}
	
	public Decimal calculateAbstractness() {
		float abstractness = this.getAbstractness();
		return Decimal.Round(new Decimal(abstractness), 2); 
	}
	
	public Decimal calculateInstability() { 
		float instability = this.getInstability();
		return Decimal.Round(new Decimal(instability), 2); 
	}
	
	public int calculateAfferentCoupling() { 
		return assembly.getAnalyzedIncomingDependencies().Count; 
		//return 0;
	}
	
	public int calculateEfferentCoupling() { 
		return assembly.getAnalyzedOutgoingDependencies().Count;
		//return 0; 
	}
	
	public Decimal calculateDistance() { 
		if (this.getInstability() != -1) {
			float distance = this.getAbstractness() + this.getInstability() - 1;
			return Decimal.Round(new Decimal(Math.Abs(distance)), 2);
		} else {
			return -1;
		}
		//return Decimal.Add(this.calculateAbstractness(), this.calculateInstability()); 
	}
	
	private float getInstability() {
		float denominator = (float)this.calculateAfferentCoupling() + (float)this.calculateEfferentCoupling();
		float instability = 0;
		if (denominator != 0) {
			instability = (float)this.calculateEfferentCoupling() / (float)(this.calculateAfferentCoupling() + (float)this.calculateEfferentCoupling());
		} else {
			instability = -1;
		}
		
		return instability;
	}
	
	private float getAbstractness() {
		int totalClasses = assembly.getClassCount();
		int abstractClasses = assembly.getAbstractClassCount();
		float abstractness = (float)abstractClasses / (float)totalClasses;
		return abstractness;
	}


}